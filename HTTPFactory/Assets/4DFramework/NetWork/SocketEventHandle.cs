using System;
using UnityEngine;
using System.Collections;
using Magicant.Util;

///@brief 
///文件名称:SocketEventHandle
///功能描述:
///数据表:
///作者:梅超
///日期:2017-4-10 9:30:54
///R1:
///修改作者:
///修改日期:
///修改理由:
public class SocketEventHandle : Singleton<SocketEventHandle>
{

    public delegate void ServerCallBackEvent(string responseMessage);

    public event ServerCallBackEvent NFCReader;
    
    void Start()
    {
        //CustomSocket.Instance().Send("Start", SystemParams.Instance().gamePCIP, SystemParams.Instance().gamePCPort);
    }

    void FixedUpdate()
    {
        CustomSocket.Instance().FixedUpdate();
    }

    public void AddResponse(string response)
    {
        DispatchHandle(response);        
    }

    //@brief 这里有个消息头处理起来会很容易
    private void DispatchHandle(string response)
    {
        if (response.StartsWith(APIS.START_RESPONSE))
        {
            NFCReader(response);
            return;
        }
    }

    public void OnApplicationQuit()
    {
        CustomSocket.Instance().OnApplicationQuit();
        RemoveAllAction();
    }

    private void RemoveAllAction()
    {
        if (NFCReader != null) NFCReader = null;
    }

}
