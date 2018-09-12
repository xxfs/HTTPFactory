using System;
using System.Reflection;
using UnityEngine;

public class SystemParams
{
    private static SystemParams instance;

    protected SystemParams() { }

    public static SystemParams Instance()
    {
        if (instance == null)
            instance = new SystemParams();

        return instance;
    }


    public string serverAddress = "http://59.110.19.162:7901/";
    public string serverAddress_tip = "服务器地址";

    public string robotStatusAddress = "http://59.110.19.162:7901/api/v1/d3/robot/Robot/status/";
    public string robotStatusAddress_tip = "机械臂状态地址";

    public string modelInfoAddress = "http://59.110.19.162:7901/api/v1/#/3D";
    public string modelInfoAddress_tip = "模型相关信息";


    public void CopyFrom(SystemParams target)
    {
        Type targetType = target.GetType();
        var targetInfos = targetType.GetFields();

        Type thisType = this.GetType();
        var thisInfos = thisType.GetFields();

        for (int i = 0; i < thisInfos.Length; i++)
        {
            object v = Convert.ChangeType(targetInfos[i].GetValue(target), thisInfos[i].FieldType);
            thisInfos[i].SetValue(this, v);
        }
    }
}