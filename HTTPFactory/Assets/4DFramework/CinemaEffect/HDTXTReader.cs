using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

///@brief 
///文件名称:HDTXTReader
///功能描述:
///数据表:
///作者:梅超
///日期:2017-4-12 13:42:6
///R1:
///修改作者:
///修改日期:
///修改理由:
public class HDTXTReader : MonoBehaviour
{
    /// <summary>
    /// @brief 读取配置文件，以字符数组的方式存储
    /// </summary>
    public IConfigString configStrings;

    /// <summary>
    /// @brief 配置文件的路径
    /// </summary>
    [Tooltip("文件存放在Assets同级目录下的config文件夹中")]
    private string configPath;

    /// <summary>
    /// @brief 配置文件名称
    /// </summary>
    [Tooltip("如果文件名为空，则为场景名字")]
    public string fileName = "";

    // Use this for initialization
    void Start()
    {
        if (fileName == "") fileName = SceneManager.GetActiveScene().name;
        
        //获取文件路径
        configPath = string.Format("{0}{1}{2}", Application.dataPath, "/../config/", SceneManager.GetActiveScene().name + ".txt");

        //如果没有目录结构，尝试创建它们
        if (!Directory.Exists(configPath)) Directory.CreateDirectory(Application.dataPath + "/../config");
        
        //获取配置字符数组
        var mono = GetComponents<MonoBehaviour>();
        foreach (var m in mono)
        {
            configStrings = m as IConfigString;
            if(configStrings!=null) break;
        }

        //读取config文件
        if (File.Exists(configPath)) ReadConfig();
    }
    
    void ReadConfig()
    {
        StreamReader sr = new StreamReader(configPath);

        string strReadline;
        while ((strReadline = sr.ReadLine()) != null)
        {
            configStrings.TxtStrings.Add(strReadline);
        }
        sr.Close();
    }

}

public interface IConfigString
{
    List<string> TxtStrings { get; set; }
}
