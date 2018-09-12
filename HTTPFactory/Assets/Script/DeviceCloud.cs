using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceCloud : MonoBehaviour
{
    private bool enableRequest = true;

    public DeviceListModelDataList curDeviceListModelDataList = new DeviceListModelDataList();    

    Dictionary<string, string> param = new Dictionary<string, string>();

    private void Start()
    {
        param.Add("start", "1");
        param.Add("limit", "1");
    }
    
    public void UpdateDeviceInfo(Action<int> callback)
    {
        if (enableRequest)
        {
            //SystemParams.Instance().robotStatusAddress;
            StartCoroutine(Get("http://huitong.jiawei.me:8900/api/v1/device/", param, callback));
            enableRequest = false;
        }
    }

    IEnumerator Get(string url, Dictionary<string, string> getData,Action<int> callback)
    {
        string param = null;

        //生成参数
        if (getData != null)
        {
            param = "?";
            int count = getData.Count;
            int index = 0;
            foreach (KeyValuePair<string, string> kvp in getData)
            {
                if (index < count - 1)
                {
                    param += kvp.Key + "=" + kvp.Value + "&";
                }
                else
                {
                    param += kvp.Key + "=" + kvp.Value;
                }
                index++;
            }
        }
        
        WWW www = new WWW(url + param);
        Debug.Log(url + param);

        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error);
            if (callback != null) callback.Invoke(1);
            enableRequest = true;
            yield return null;
        }
        else
        {
            Debug.Log(www.text);
            curDeviceListModelDataList = JsonMapper.ToObject<DeviceListModelDataList>(www.text);
            //Debug.Log(curDeviceListModelDataList.count);
            //Debug.Log(curDeviceListModelDataList.data[0].name);
            if (callback != null) callback.Invoke(2);
            enableRequest = true;
        }
    }

}

public class DeviceListModelDataList {

    public int code = 200;
    public string message = "OK";
    public int count;
    public int start;
    public int limit;
    public string next = "OK";
    public string previous = "OK";
    public DeviceListModel[] data;
    
}

public class DeviceListModel {
    public string id;
    public string device_id;
    public string name;
    public int state;
}
