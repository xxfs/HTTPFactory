using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using DG.Tweening;
using Magicant.Util;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

///@brief 
///文件名称:ClientLogic
///功能描述:
///数据表:
///作者:梅超
///日期:2017-4-11 11:1:0
///R1:
///修改作者:
///修改日期:
///修改理由:
public class ClientLogic : Singleton<ClientLogic>
{
    ///// <summary>
    ///// @brief 该场玩家信息
    ///// </summary>
    //public Users curUsers;

    ///// <summary>
    ///// @brief 枪的信息
    ///// </summary>
    //private GunManager gunManager;

    //   /// <summary>
    //   /// @brief 上传下载工具
    //   /// </summary>
    //   private HTTP_ToolsForManWei httpTools;

    //   private StartAvatar startAvatar;

    //public float BGMVolume = 1f;
    //public float startVolume = 1f;

    //   /// <summary>
    //   /// @brief 游戏开始时间
    //   /// </summary>
    //   [HideInInspector]
    //   public string startTime;

    //   /// <summary>
    //   /// @brief 玩家头像
    //   /// </summary>
    //   public Texture2D[] avatars;

    //// Use this for initialization
    //void Start () 
    //{
    //	SocketEventHandle.Instance.NFCReader += OnNFCReader;
    //	Init();
    //}

    ///// <summary>
    ///// @brief 初始化(整个游戏中只有一次)
    ///// </summary>
    //private void Init()
    //{        
    //	//TODO 这样写GunManager必须放在子物体
    //	gunManager = transform.GetComponentInChildren<GunManager>();
    //    httpTools = transform.GetComponent<HTTP_ToolsForManWei>();

    //       avatars = new Texture2D[SystemParams.Instance().playersNum];

    //       curUsers = new Users();
    //	curUsers.uid = new string[gunManager.playersNum];
    //	curUsers.score = new int[gunManager.playersNum];
    //}

    ///// <summary>
    ///// @brief NFC读卡功能
    ///// </summary>
    ///// <param name="responseMessage"></param>
    //private void OnNFCReader(string responseMessage)
    //{
    //	////Debug.Log("NFCReader");
    //	//var wi = JsonUtility.FromJson<WristInfo>(responseMessage);
    //	//var uidIndex = GetSeatIndex(wi.readerID);
    //	//curUsers.uid[uidIndex] = wi.cardID;

    // //      //如果一个玩家重复刷不同座位，则以最后一次刷卡为准
    // //   if (cardID2SeatID.ContainsKey(wi.cardID))
    // //   {
    // //          //Debug.Log("该卡已经刷过，上次的座位ID为:"+ cardID2SeatID[wi.cardID]);
    // //       var preSeatID = cardID2SeatID[wi.cardID]- 1;
    // //          this.startAvatar.ClearAvatar(preSeatID);
    // //          SetSingleAvatarToDefault(preSeatID);
    // //      }

    // //   if (!cardID2SeatID.ContainsKey(wi.cardID))
    // //       cardID2SeatID.Add(wi.cardID, uidIndex + 1);
    // //   else
    // //       cardID2SeatID[wi.cardID] = uidIndex+1;

    // //      //Debug.Log(wi.readerID);
    // //      //Debug.Log(wi.cardID);

    // //          //TODO 下载玩家头像
    // //          //httpTools.DownloadAvatar();
    // //      httpTools.DownloadAvatar(wi.cardID, wi.readerID);
    //   }

    //   /// <summary>
    //   /// @brief 将玩家头像修改为默认图像
    //   /// </summary>
    //   public void SetSingleAvatarToDefault(int index)
    //   {
    //       //Texture2D image = Resources.Load<Texture2D>("DefaultAvatar/Avatar" + index);
    //       //ClientLogic.Instance.avatars[index] = image;
    //   }

    //   public void OnUploadScore(int playerOrder)
    //   {
    //       //httpTools.UploadScores(playerOrder);
    //   }

    //   public void SetStartAvatar(StartAvatar startAvatar)
    //   {
    //       this.startAvatar = startAvatar;
    //   }

    //   public void SetStartAvatarTexture(Texture2D t2d,int index,bool hasPlayed)
    //   {
    //       //var s = Sprite.Create(t2d,new Rect(0,0,t2d.width,t2d.height),new Vector2(0.5f,0.5f));
    //       //this.startAvatar.SetAvatart(s,index, hasPlayed);

    //       ////玩过就不能再玩了
    //       //gunManager.playerReticles[index].SetGunValid(!hasPlayed);
    //   }

    //   public void InitGunReticleUI()
    //   {
    //       //gunManager.InitReticlUI();
    //   }

    //   public Dictionary<string, int> cardID2SeatID = new Dictionary<string, int>();

    //   public void InitCardID2SeatID()
    //   {
    //       cardID2SeatID.Clear();
    //       var count = SystemParams.Instance().playersNum;
    //       for (int i = 0; i < count; i++)
    //       {
    //           cardID2SeatID.Add(i.ToString(),i+1);
    //       }
    //   }

    //   public void InitScores()
    //   {
    //       //var count = gunManager.playersNum;
    //       //for (int i = 0; i < count; i++)
    //       //{
    //       //    curUsers.uid[i] = i.ToString();
    //       //    curUsers.score[i] = 0;
    //       //    if (!SystemParams.Instance().leftToRight)
    //       //        gunManager.playerReticles[i].score = i;
    //       //    else
    //       //        gunManager.playerReticles[i].score = count - i;
    //       //}
    //   }

    //   public int GetGunIDByCardID(string cardID)
    //   {
    //       if(cardID2SeatID.ContainsKey(cardID))
    //           return cardID2SeatID[cardID];
    //       else
    //       {
    //           return 1;
    //       }
    //   }

    //   /// <summary>
    //   /// @brief 通过读卡器ID获取对应的座位号
    //   /// </summary>
    //   /// <param name="readID"></param>
    //   /// <returns></returns>
    //   public int GetSeatIndex(string readID)
    //   {
    //       if (readID == SystemParams.Instance().reader1) return 0;
    //       if (readID == SystemParams.Instance().reader2) return 1;
    //       if (readID == SystemParams.Instance().reader3) return 2;
    //       if (readID == SystemParams.Instance().reader4) return 3;
    //       if (readID == SystemParams.Instance().reader5) return 4;
    //       if (readID == SystemParams.Instance().reader6) return 5;
    //       if (readID == SystemParams.Instance().reader7) return 6;
    //       if (readID == SystemParams.Instance().reader8) return 7;
    //       return 0;
    //   }

    //   private Coroutine startCoroutine;
    //   private Coroutine audioFadeCoroutine;
    ///// <summary>
    ///// @brief 当玩家刷卡完毕，则开始游戏
    ///// </summary>
    //public void StartGame()
    //{
    //    //if (PlayerPrefs.GetInt("isFirst") == 0)
    //    //{
    //    //       //Debug.Log("StartGame");
    //    //       this.startAvatar.CloseAllAvatar();

    //    //    //连续按下开始按键也只开启一次
    //    //    PlayerPrefs.SetInt("isFirst", 1);

    //    //    //TODO 播放声音
    //    //    AudioSource audioSource = Camera.main.gameObject.AddComponent<AudioSource>();
    //    //    AudioClip audioClip = Resources.Load<AudioClip>("Sound/start");
    //    //    audioSource.clip = audioClip;
    //    //    audioSource.volume = Mathf.Clamp(startVolume, 0, 1);
    //    //    audioSource.Play();

    //    //    //TODO 字幕开启
    //    //    var subtitle = GameObject.FindGameObjectWithTag("ConfigExample");
    //    //    var configStrings = subtitle.GetComponent<ConfigExample>();
    //    //    configStrings.OnStart();

    //    //    //TODO 背景音减小
    //    //    var bgmAudioSource = Camera.main.transform.GetComponent<AudioSource>();
    //    //    bgmAudioSource.DOFade(0.22f, 0.5f);


    //    //    //TODO 声音播放完毕，黑屏，场景跳转
    //    //    startCoroutine = StartCoroutine(WaitForDarkScreen(audioClip.length + 0.5f));

    //    //    //TODO 切换场景时候声音淡出
    //    //    audioFadeCoroutine = StartCoroutine(BGMFadeOut(audioClip));


    //    //    //TODO 关闭厅灯
    //    //    CustomSocket.Instance().Send("<DO," + SystemParams.Instance().cinemaLight.ToString() + ",1>"
    //    //        , SystemParams.Instance().effactServerIP
    //    //        , SystemParams.Instance().effactServerPort);

    //    //    CustomSocket.Instance().Send("<DO," + SystemParams.Instance().cinemaLight.ToString() + ",1>"
    //    //        , SystemParams.Instance().effactServerIP
    //    //        , SystemParams.Instance().effactServerPort);

    //    //    //TODO 发送开始消息到预演区
    //    //    string mes = APIS.START_RESPONSE;
    //    //    CustomSocket.Instance().Send(mes, SystemParams.Instance().attractPCIP, SystemParams.Instance().attractPCPort);

    //    //    //TODO 开启协程每5S发送一次分数信息到预演区
    //    //    simpleCoroutine = StartCoroutine(SimpleSocket());
    //    //}
    //}

    //   /// <summary>
    //   /// @brief 结束的时候声音淡出
    //   /// </summary>
    //   /// <param name="audiClip"></param>
    //   /// <returns></returns>
    //   private IEnumerator BGMFadeOut(AudioClip audiClip)
    //   {
    //       float audioTime = audiClip.length;

    //       yield return new WaitForSeconds(audioTime);

    //       var bgmAudioSource = Camera.main.transform.GetComponent<AudioSource>();

    //       float time = 0;
    //       while (time < 1)
    //       {
    //           time+= Time.deltaTime;
    //           bgmAudioSource.volume -= Time.deltaTime;
    //           yield return new WaitForEndOfFrame();
    //       }

    //   }

    //   private Coroutine simpleCoroutine;
    ///// <summary>
    ///// @brief 每隔5S发送一次消息
    ///// </summary>
    ///// <returns></returns>
    //private IEnumerator SimpleSocket()
    //{        
    //	var time = SystemParams.Instance().sendInterval;
    //	while (true)
    //	{
    //		SendScores();
    //		yield return new WaitForSeconds(time);
    //	}
    //}

    ///// <summary>
    ///// @brief 朝预演区发送分数信息
    ///// </summary>
    //private void SendScores()
    //{
    //	//curUsers.score = gunManager.GetScores();
    //	//var jsonSend = APIS.UPDATE_RESPONSE + "|" + JsonUtility.ToJson(curUsers);
    //	//CustomSocket.Instance().Send(jsonSend,SystemParams.Instance().attractPCIP,SystemParams.Instance().attractPCPort);
    //}

    ///// <summary>
    ///// @brief 游戏结束或者意外停止
    ///// </summary>
    //public void StopGame()
    //{
    //    ////if (PlayerPrefs.GetInt("isFirst") == 1)
    //    ////{
    //    //    StopAllCoroutines();
    //    //    GameController.instance.hdSeatSender.OnGameReset();
    //    //    GameController.instance.hdSeatEffactSender.OnGameReset();
    //    //    SceneManager.LoadScene("Start");
    //    //    PlayerPrefs.SetInt("isFirst", 0);
    //    ////}
    //}

    ///// <summary>
    ///// @brief 得到user[]
    ///// </summary>
    ///// <returns></returns>
    //public User[] GetUsers()
    //{
    //	curUsers.score = gunManager.GetScores();
    //	return curUsers.ConvertUser();
    //}

    //   private IEnumerator WaitForDarkScreen(float timer)
    //{
    //	yield return new WaitForSeconds(timer);
    //	//黑屏
    //	GameObject darkCav = Resources.Load<GameObject>("Prefab/TimeLineTools/GraduallyDark");
    //	darkCav.GetComponent<GraduallyDark>().gradually = Gradually.Dark;
    //	GameObject go = Instantiate(darkCav);
    //       yield return new WaitForSeconds(go.GetComponent<GraduallyDark>().duration + 0.1f);
    //	SceneManager.LoadScene("Main");
    //}

    //public AudioSource PlayMusic(string path, bool isLoop)
    //{
    //    var audioSource = Camera.main.gameObject.AddComponent<AudioSource>();
    //       AudioClip audioClip = Resources.Load<AudioClip>(path);
    //       audioSource.clip = audioClip;
    //       audioSource.loop = isLoop;
    //       audioSource.Play();
    //	return audioSource;
    //}

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space) ||
    //        Input.GetKeyDown(KeyCode.PageUp) ||
    //        Input.GetKeyDown(KeyCode.UpArrow) ||
    //        Input.GetKeyDown(KeyCode.LeftArrow))
    //    {
    //        if (PlayerPrefs.GetInt("isFirst") == 0)
    //        {
    //            StartGame();
    //        }
    //    }

    //    if (Input.GetKeyDown(KeyCode.T) ||
    //        Input.GetKeyDown(KeyCode.D) ||
    //        Input.GetKeyDown(KeyCode.PageDown) ||
    //        Input.GetKeyDown(KeyCode.DownArrow) ||
    //        Input.GetKeyDown(KeyCode.RightArrow))
    //    {
    //        StopAllCoroutines();
    //        GameController.instance.hdSeatSender.OnGameReset();
    //        GameController.Instance.StopEffact();
    //        SceneManager.LoadScene("Start");
    //    }



    //    //UnitTest
    //    if (Input.GetKeyDown(KeyCode.B))
    //    {
    //        SceneManager.LoadScene("UIScene");
    //    }

    //    //GunTest
    //    if (Input.GetKeyDown(KeyCode.H))
    //    {
    //        SceneManager.LoadScene("GunTest");
    //    }

    //    if (Input.GetKeyDown(KeyCode.Q))
    //    {
    //        Debug.Log("Down");
    //        httpTools.DownloadAvatar("47FEB4D6", "2");
    //    }

    //    if (Input.GetKeyDown(KeyCode.F1))
    //    {
    //        scaleFlag = !scaleFlag;
    //        if (scaleFlag)
    //        {
    //            Time.timeScale = 0;
    //        }
    //        else
    //        {
    //            Time.timeScale = 1;
    //        }
    //    }

    //}

    //bool scaleFlag = false;
}
