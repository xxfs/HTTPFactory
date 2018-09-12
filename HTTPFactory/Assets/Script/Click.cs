using System;
using UnityEngine;

public class Click : MonoBehaviour
{
    public DevicePanel devicePanel;

    public DeviceCloud deviceCloud;

    public int deviceid;

    void OnMouseDown()
    {
        deviceCloud.UpdateDeviceInfo(EndGet);
    }

    /// <summary>
    /// code = 1 -----> 失败
    /// code = 2 -----> 成功
    /// </summary>
    /// <param name="stateCode"></param>
    private void EndGet(int stateCode)
    {
        if (stateCode == 2)
        {
            var device = deviceCloud.curDeviceListModelDataList.data[deviceid];
            var did = device.device_id;
            var dname = device.name;
            var dstate = device.state;
            devicePanel.Show(did, dname, dstate, stateCode);
        }
        else
        {
            devicePanel.ShowMessage();
        }
    }

}
