using System;
using UnityEngine;
using System.Collections;
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
public class CustomSocket 
{
    //@brief 接收信息的UDP
    private UdpClient systemReceiverUdp;

    //@brief 接收信息的线程
    private Thread udpThreadReceiver;

    //@brief 接收来自其他客户端的信息
    private string heartDatas = "";

    private static CustomSocket _instance;

    public string HeartDatas
    {
        get { return heartDatas; }
        set { heartDatas = value; }
    }

    public static CustomSocket Instance()
    {
        if (_instance == null)
        {
            _instance = new CustomSocket();
        }
        return _instance;
    }

    public CustomSocket()
    {
        //@brief 根据系统参数实例化接收信息的UDP
        systemReceiverUdp = new UdpClient(5000);
        udpThreadReceiver = new Thread(new ThreadStart(ReceiveUdpThread));
        udpThreadReceiver.IsBackground = true;
        udpThreadReceiver.Start();
    }

    private bool isSceneHeartRuning = true;
    public void ReceiveUdpThread()
    {
        IPEndPoint remoteHost = null;
        while (systemReceiverUdp != null && Thread.CurrentThread.ThreadState.Equals(ThreadState.Background)&& isSceneHeartRuning)
        {
            try
            {
                byte[] buf = systemReceiverUdp.Receive(ref remoteHost);
                string s = Encoding.UTF8.GetString(buf);
                heartDatas = s;
            }
            catch (Exception e)
            {
                Debug.Log("Stop..." + e);
            }
        }
    }

    public void FixedUpdate()
    {
        SocketEventHandle.Instance.AddResponse(heartDatas);
        heartDatas = "";
    }

    /// <summary>
    /// 朝指定IP发送消息
    /// </summary>
    /// <param name="s">消息内容</param>
    /// <param name="ip">指定主机IP</param>
    /// <param name="port">指定主机端口</param>
    public void Send(string s, string ip, int port)
    {
        byte[] mes = Encoding.UTF8.GetBytes(s);
        systemReceiverUdp.Send(mes, mes.Length, ip, port);
    }

    public void OnApplicationQuit()
    {
        isSceneHeartRuning = false; 
        udpThreadReceiver.Abort();
        systemReceiverUdp.Close();
        Thread.Sleep(100);
    }

}
