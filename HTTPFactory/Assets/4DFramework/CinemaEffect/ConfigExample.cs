using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

///@brief 
///文件名称:ConfigExample
///功能描述:
///数据表:
///作者:梅超
///日期:2017-4-12 13:42:6
///R1:
///修改作者:
///修改日期:
///修改理由:
public class ConfigExample : MonoBehaviour, IConfigString
{
    public bool playOnAwake;
    //public float delayTime;

    /// <summary>
    /// @brief 读取到的字幕信息
    /// </summary>
    private List<string> txtStrings = new List<string>();

    /// <summary>
    /// @brief 当前显示的文字
    /// </summary>
    private string curContent;

    /// <summary>
    /// @brief 上一次字幕的持续时间
    /// </summary>
    private float preDurTime = 0;

    /// <summary>
    /// @brief 字幕持续时间
    /// </summary>
    private float durTime;

    /// <summary>
    /// @brief 时间
    /// </summary>
    private float startLine = 0;

    /// <summary>
    /// @brief 上一次的时间节点
    /// </summary>
    private float preTimeLine = 0;

    /// <summary>
    /// @brief 当前的时间节点
    /// </summary>
    private float curTimeLine = 0;

    /// <summary>
    /// @brief 读取到的字幕信息
    /// </summary>
    public List<string> TxtStrings
    {
        get { return txtStrings; }
        set { txtStrings = value; }
    }

    void Start()
    {
        if (playOnAwake) StartCoroutine(StartConfiger());
    }
    
    IEnumerator StartConfiger()
    {
        yield return new WaitForEndOfFrame();

        foreach (var s in txtStrings)
        {
            var t = s.Split('|');
            curContent = t[0];
            var readTime = t[1];

            var timevalues = readTime.Split(':');
            //计算秒数的绝对值
            var minuteValue = float.Parse(timevalues[0]) * 60;
            var secondsValue = float.Parse(timevalues[1]);
            curTimeLine = minuteValue + secondsValue;
                    
            yield return new WaitForSeconds(Mathf.Clamp(curTimeLine - preTimeLine, 0, 99999f));
            Debug.Log("Test" + Time.time);
            preTimeLine = curTimeLine;

        }

    }

}
