using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.PostProcessing;

//Blender读取用于融合的纹理图片和alpha图片，根据纹理图片生成最终的渲染图像
//目前仅支持单通道的立体左右眼融合，更多通道的融合持续开发中。。
public class Screen1X3 : MonoBehaviour {

    public Camera _viewer;//用来观察左右画布的摄像机
    public GameObject _canvas;//左画布
    public Texture2D _sampleTexture;//采样纹理图
    public Texture2D _alphaTexture;//融合纹理图

    private float cameraSize = 5.0f;

    //单例模式的实现
    private static object lockpad = new object();
    private static Screen1X3 _instance;
    public static Screen1X3 Instance
    {
        get
        {
            lock (lockpad)
            {
                if (!_instance)
                {
                    _instance = GameObject.FindObjectOfType<Screen1X3>();
                    if (!_instance)
                    {
                        GameObject g = new GameObject("MovieBlender");
                        _instance = g.AddComponent<Screen1X3>();
                        g.transform.position = Vector3.up * 1000f;
                    }
                }
                return _instance;
            }
        }
    }

    //初始化，读取采样纹理和融合纹理，设置摄像机和画布
    void Awake()
    {

        //声明不随着场景读取而Destroy
        DontDestroyOnLoad(gameObject);

        //开始读取网格采样纹理
        if (!ReadSampleTexture())
        {
            return;
        }

        //然后读取透明度融合纹理
        ReadAlphaTexture();

        //读取完成后，设置观察者摄像机
        CreateViewer();

    }

    //读取网格采样纹理,并分解成左右眼的采样信息
    bool ReadSampleTexture()
    {

        byte[] buf = new byte[] { };

        //FixedMe
        string sampleTexturePath = Application.dataPath + "/../Config/1X3/tex1.png";

        //如果采样纹理不存在，则确定无法形成立体模式，发送无法读取采样纹理的事件，并自杀
        if (!File.Exists(sampleTexturePath))
        {
            Debug.Log("无法读取网格采样纹理");
            return false;
        }

        //读取
        buf = File.ReadAllBytes(sampleTexturePath);
        _sampleTexture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
        _sampleTexture.filterMode = FilterMode.Point;
        _sampleTexture.wrapMode = TextureWrapMode.Clamp;
        _sampleTexture.LoadImage(buf);
        _sampleTexture.Apply();
        return true;
    }

    //读取融合纹理，并分解左右眼的融合信息
    bool ReadAlphaTexture()
    {
        byte[] buf = new byte[] { };

        //FixedMe
        string alphaTextureLeftPath = Application.dataPath + "/../Config/1X3/alpha1.png";
        //如果融合纹理存在，则读取，并根据融合纹理得到左融合和右融合纹理，如果不存在，则创建默认纹理，并发送无法读取的事件
        //即便无法读取融合纹理，立体也可以实现，一般情况下单通道是不需要融合的
        if (File.Exists(alphaTextureLeftPath))
        {
            buf = File.ReadAllBytes(alphaTextureLeftPath);
            _alphaTexture = new Texture2D(1, 1);
            _alphaTexture.filterMode = FilterMode.Point;
            _alphaTexture.wrapMode = TextureWrapMode.Clamp;
            _alphaTexture.LoadImage(buf);
            _alphaTexture.Apply();
            return true;
        }
        else
        {
            Debug.Log("无法读取融合纹理");
            _alphaTexture = new Texture2D(1, 1);
            _alphaTexture.SetPixel(0, 0, new Color(1.0f, 1.0f, 1.0f, 1.0f));
            _alphaTexture.Apply();
            return false;
        }
    }

    //设置观察者相机和左右画布
    void CreateViewer()
    {
        //如果观察者摄像机未初始化，则首先寻找子节点有没有观察者摄像机，如果不存在，则创建
        if (!_viewer)
        {
            Transform viewerTransform = transform.Find("Viewer");
            if (!viewerTransform)
            {
                GameObject viewerContainer = new GameObject("Viewer");
                _viewer = viewerContainer.AddComponent<Camera>();
            }
            else
            {
                _viewer = viewerTransform.GetComponent<Camera>();
            }
        }
        _viewer.clearFlags = CameraClearFlags.SolidColor;
        _viewer.backgroundColor = new Color(0.0f, 0.0f, 0.0f, 0.02f);//Color.black
        _viewer.orthographic = true;
        _viewer.orthographicSize = cameraSize;
        _viewer.nearClipPlane = 0.3f;
        _viewer.farClipPlane = 2.0f;
        _viewer.depth = 10;
        _viewer.renderingPath = RenderingPath.DeferredLighting;
        _viewer.cullingMask = 1 << LayerMask.NameToLayer(TagManagerData.layers[0]);
        //设置为Blender的子集
        _viewer.transform.parent = this.transform;
        _viewer.transform.localPosition = Vector3.zero;

        SetCanvas();
    }

    public void CopyEffect()
    {
        if (Camera.main.GetComponent<PostProcessingBehaviour>())
        {
            if (!_viewer.gameObject.GetComponent<PostProcessingBehaviour>())
            {
                var post = _viewer.gameObject.AddComponent<PostProcessingBehaviour>();
                post.profile = Camera.main.GetComponent<PostProcessingBehaviour>().profile;
            }
            else
            {
                _viewer.gameObject.GetComponent<PostProcessingBehaviour>().profile = Camera.main.GetComponent<PostProcessingBehaviour>().profile;
            }
        }
    }

    void SetCanvas()
    {
        if (!_canvas)
        {
            Transform canvas_Transform = _viewer.transform.Find("Canvas");
            if (!canvas_Transform)
            {
                _canvas = GameObject.CreatePrimitive(PrimitiveType.Quad);
                _canvas.name = "Canvas";
                _canvas.transform.parent = _viewer.transform;
                Destroy(_canvas.GetComponent<Collider>());
            }
        }
        _canvas.transform.localPosition = new Vector3(0.0f, 0.0f, 1.0f);
        _canvas.transform.localScale = new Vector3((float)Screen.width / (float)Screen.height * cameraSize * 2f, cameraSize * 2f, 1.0f);

        SetCanvasShader();
    }

    void SetCanvasShader()
    {
        //FixedMe
        Material mat = new Material(Shader.Find("HD/Blender1X3"));
        mat.SetTexture("_SamTex", _sampleTexture);
        mat.SetTexture("_AlphaTex", _alphaTexture);

        //mat.SetTextureScale("_SamTex", new Vector2(0.5f, 1));

        _canvas.GetComponent<Renderer>().material = mat;
        _canvas.layer = LayerMask.NameToLayer(TagManagerData.layers[0]);
    }
}
