using System;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

///@brief 
///文件名称:CustomSocket
///功能描述:
///数据表:
///作者:梅超
///日期:2017-4-10 9:30:50
///R1:
///修改作者:
///修改日期:
///修改理由:
public class GunSocket : MonoBehaviour
{
    /// <summary>
    /// @brief 枪的ID
    /// </summary>
    public int gunStartPort = 0;

    /// <summary>
    /// @brief 玩家准心
    /// </summary>
    public ScreenReticle ScreenReticle;

    /// <summary>
    /// @brief 接收信息的UDP
    /// </summary>
    private UdpClient systemReceiverUdp;

    /// <summary>
    /// @brief 接收信息的线程
    /// </summary>
    private Thread udpThreadReceiver;

    /// <summary>
    /// @brief 接收来自其他客户端的信息
    /// </summary>
    private string heartDatas = "";

    //private StringBuilder stringTools = new StringBuilder();

    void Start()
    {
        if (ScreenReticle == null)
        {
            Debug.LogWarning("请关联玩家准心组件！");
            Destroy(this);
            return;            
        }

        GunSocketInit(gunStartPort);
    }

    /// <summary>
    /// @brief 枪的Socket初始化
    /// </summary>
    /// <param name="gunPort"></param>
    public void GunSocketInit(int gunPort)
    {
        systemReceiverUdp = new UdpClient(gunPort);
        udpThreadReceiver = new Thread(new ThreadStart(ReceiveUdpThread));
        udpThreadReceiver.IsBackground = true;
        udpThreadReceiver.Start();
    }

    public bool isSceneHeartRuning = true;
    /// <summary>
    /// @brief 接收来自枪的数据
    /// </summary>
    private void ReceiveUdpThread()
    {
        IPEndPoint remoteHost = null;
        while (systemReceiverUdp != null && Thread.CurrentThread.ThreadState.Equals(ThreadState.Background) &&
               isSceneHeartRuning)
        {
            try
            {
                byte[] buf = systemReceiverUdp.Receive(ref remoteHost);
                string s = Encoding.UTF8.GetString(buf);
                heartDatas += s;
                //stringTools.Append(s);
                Thread.Sleep(5);
            }
            catch (Exception e)
            {
                Debug.Log("Stop..." + e);
            }
        }
    }

    #region New

    ////using for gc
    //private float x = 0;
    //private float y = 0;
    //private int state = 0;
    //private int count = 0;

    ///// <summary>
    ///// @brief 解析枪的数据(新)
    ///// </summary>
    //private void PadMsg()
    //{
    //    heartDatas = stringTools.ToString();
    //    if (heartDatas.Length <= 0) return;//如果没有接收到枪信息，则什么也不做        
    //    var strs = heartDatas.Split(',');//解析枪发送来的数据<4001,320,240,0><4001,319,240,0><4001,319,240,1>
    //    x = 0; y = 0; state = 0;//重置枪状态
    //    count = strs.Length;//枪发送的数据如上，这里得到切割后字符串的长度
    //    for (int i = 0; i < count; i++)//遍历字符数组长度，解析是否有开枪信息
    //    {
    //        if (strs[i].Contains(">"))//坐标信息(听说有可能出现这种格式，所以这里进行处理<4001,320,240,0><4001,319,240,0><4001,319,)
    //        {
    //            x = float.Parse(strs[i - 2]);
    //            y = float.Parse(strs[i - 1]);
    //        }
    //        if (strs[i].Contains("1>"))//开枪信息
    //        {
    //            state = 1;
    //        }
    //    }
    //    ScreenReticle.NetWorkControl(x, y, state);//枪信息应用到准心上
    //}

    //private void FixedUpdate()
    //{
    //    PadMsg();
    //    stringTools.Remove(0, heartDatas.Length);
    //    heartDatas = "";
    //}

    #endregion

    #region Old
    /// <summary>
    /// @brief 解析枪的信号，其中信号格式为 #id，X，Y，State#
    /// </summary>
    /// @param msg 二进制流
    /// @param size byte数组长度
    /// @param s 枪的状态
    /// @param x X坐标
    /// @param y Y坐标
    private void PadMsg(byte[] msg, int size, ref int s, ref float x, ref float y)
    {

        shootstate = 0;
        if (size == 0)
            return;
        for (int i = 0; i < size; i++)
        {
            if (!bReceiveOK) // 是否正确接收到指令
            {
                if ('<' == msg[i]) // package head
                {
                    bFoundHead = true;
                    iReceiveCounter = 0;
                    dotcount = 0;
                }
                else if (dotcount == 3 && bFoundHead)		//package end
                {
                    cReceiveBuffer[iReceiveCounter++] = msg[i];
                    bFoundHead = false;
                    if ((iReceiveCounter < MAXBUFSIZE + 1))
                    {
                        bReceiveOK = true;
                        if (bReceiveOK) // 找到一个完整的数据包;
                        {
                            string msgStr = System.Text.Encoding.ASCII.GetString(cReceiveBuffer, 0, iReceiveCounter);

                            string[] sArray = msgStr.Split(',');

                            if (sArray.Length == 4)
                            {
                                int ss = int.Parse(sArray[3]);
                                if (ss != 0)
                                {
                                    s = ss;
                                }
                                x = float.Parse(sArray[1]);
                                y = float.Parse(sArray[2]);
                            }


                            bReceiveOK = false;
                            bFoundHead = false;
                            iReceiveCounter = 0;
                            dotcount = 0;
                        }
                    }
                    else
                    {
                        bFoundHead = false;
                        iReceiveCounter = 0;
                        dotcount = 0;
                    }
                }
                else if (bFoundHead)
                {
                    if (iReceiveCounter < MAXBUFSIZE)
                    {
                        if (',' == msg[i])
                        {
                            dotcount++;
                        }
                        cReceiveBuffer[iReceiveCounter++] = msg[i];
                    }
                    else
                    {
                        bFoundHead = false;		//标准接收包起始标志
                        iReceiveCounter = 0;
                        dotcount = 0;
                    }
                }
            }
        }
    }


    void FixedUpdate()
    {
        if (true)//(SystemParams.Instance().enableUDPGun)
        {
            try
            {
                //不移动枪时,其发送的数据会变慢，3s接受不到信息就隐藏准心
                if (heartDatas.Length == 0)
                {
                    shoottime += Time.deltaTime;
                    if (shoottime > 3)
                    {
                        shootx = shooty = -300f;
                        shoottime = 0.0f;
                    }
                }
                else
                {
                    shoottime = 0.0f;
                }

                byte[] bs = Encoding.ASCII.GetBytes(heartDatas);
                PadMsg(bs, bs.Length, ref shootstate, ref shootx, ref shooty);
                heartDatas = "";

                ////计算准星在当前屏幕的位置2014/4/29  Fresh2017/10/17
                ScreenReticle.NetWorkControl(shootx, shooty, shootstate);

            }
            catch (Exception)
            {
                throw;
            }
        }
    }


    private bool bReceiveOK { get; set; }

    private int shootstate = 0;

    private float shootx = 0.0f;

    private float shooty = 0.0f;

    private bool bFoundHead { get; set; }

    private int iReceiveCounter { get; set; }

    private int dotcount { get; set; }

    const int MAXBUFSIZE = 64;
    byte[] cReceiveBuffer = new byte[MAXBUFSIZE];       //和上位机通讯时的缓冲区

    private float DistanceX { get; set; }
    private float DistanceY { get; set; }

    float shoottime = 0.0f;

    #endregion

    /// <summary>
    /// @brief 退出
    /// </summary>
    private void OnApplicationQuit()
    {
        isSceneHeartRuning = false;
        udpThreadReceiver.Abort();
        systemReceiverUdp.Close();
        Thread.Sleep(100);
    }

}