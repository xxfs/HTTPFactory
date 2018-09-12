using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DevicePanel : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public Text deviceIDText;
    public Text deviceNameText;
    public Text deviceStateText;

    private string device_id;
    private string device_name;
    private int state;

    public void Show(string device_id, string device_name, int state,int stateCode)
    {
        canvasGroup.alpha = 1;
        deviceIDText.text = device_id;
        deviceNameText.text = device_name;        
        deviceStateText.text = state == 1 ? "正常" : "异常";
    }

    public void ShowMessage()
    {

    }

    public void Hide()
    {
        canvasGroup.alpha = 0;

        deviceIDText.text = "";
        deviceNameText.text = "";
        deviceStateText.text = "";
    }
}
