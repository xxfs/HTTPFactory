using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Camera))]
public class Blend1X2 : MonoBehaviour
{
    private Camera _camera; //主相机
    private Camera _UIcamera; //UI相机

    private RenderTexture render;

    // Use this for initialization
    void Start()
    {
        _camera = GetComponent<Camera>();
        _camera.cullingMask = ~(1<<LayerMask.NameToLayer(TagManagerData.layers[0]));
        if (GameObject.FindWithTag(TagManagerData.tags[0]))
        {
            _UIcamera = GameObject.FindWithTag(TagManagerData.tags[0]).GetComponent<Camera>();
        }
        StartCoroutine(BlendRender());        
    }

    //获得Camera的图像
    RenderTexture GetRenderTexture(Camera cam)
    {
        try
        {
            RenderTexture rt = new RenderTexture(2048, 1020, 24, RenderTextureFormat.Default);
            rt.wrapMode = TextureWrapMode.Clamp;
            rt.filterMode = FilterMode.Point;
            rt.Create();
            cam.targetTexture = rt;
            cam.ResetAspect();
            return rt;
        }
        catch (Exception e)
        {
            Debug.LogError("无法获取摄像机图像，摄像机:" + cam.name + "\n" + e.ToString());
        }
        return null;
    }

    //渲染图像到左右Blender
    IEnumerator BlendRender()
    {
        yield return null;
        Screen1X2.Instance._canvas.GetComponent<Renderer>().material.mainTexture = GetRenderTexture(_camera);
        if (_UIcamera)
        {
            Screen1X2.Instance._canvas.GetComponent<Renderer>()
                .material.SetTexture("_UITex", GetRenderTexture(_UIcamera));
            Screen1X2.Instance._canvas.GetComponent<Renderer>()
                .material.SetColor("_UIColor", Color.white);
        }
    }
}
