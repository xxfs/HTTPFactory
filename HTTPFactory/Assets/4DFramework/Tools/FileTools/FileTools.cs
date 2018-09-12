using System;
using UnityEngine;
using System.IO;

///@brief 
///文件名称:FileTools
///功能描述:
///数据表:
///作者:梅超
///日期:2017-4-12 13:42:6
///R1:
///修改作者:
///修改日期:
///修改理由:

//TODO 项目完成后将其改为通用类，添加三个方法，一个直接读取所有文本文件返回一串字符，一个逐行读取返回一个字符数组，一个写入一串字符
public class FileTools
{
    //@brief 配置文件的路径
    private static string configPath = "/../config/config.txt";

    //程序初始化的时候读取一次
    public static void ReadOnce()
    {
        configPath = "/../config/config.txt";

        //获取文件路径
        configPath = string.Format("{0}{1}", Application.dataPath, configPath);

        //如果没有目录结构，尝试创建它们
        if (!Directory.Exists(configPath)) Directory.CreateDirectory(Application.dataPath + "/../config");

        //读取config文件
        if (File.Exists(configPath)) ReadConfig();
        else UpdateConfig();
    }

    //如果文件存在则读取文件
    private static void ReadConfig()
    {
        StreamReader sr = new StreamReader(configPath);
        var sjson = sr.ReadToEnd();
        SystemParams tps;
        try
        {
            tps = JsonUtility.FromJson<SystemParams>(sjson);
            SystemParams.Instance().CopyFrom(tps);
            sr.Close();
        }
        catch (Exception)
        {
            sr.Close();
            UpdateConfig();
        }
    }

    //@brief 此处可重新创建config文件并将这些预设写入
    public static void UpdateConfig()
    {
        try
        {
            configPath = "/../config/config.txt";
            //获取文件路径
            configPath = string.Format("{0}{1}", Application.dataPath, configPath);

            if (File.Exists(configPath))
            {
                File.Delete(configPath);
            }

            FileStream fs = new FileStream(configPath, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);

            var sjson = JsonUtility.ToJson(SystemParams.Instance());

            //将Json逐行写入配置文件
            var jsontowrite = sjson.Split(',');

            for (int i = 0; i < jsontowrite.Length; i++)
            {
                if (i != jsontowrite.Length - 1)
                    sw.WriteLine(jsontowrite[i] + ",");
                else
                    sw.WriteLine(jsontowrite[i]);
            }

            sw.Flush();
            sw.Close();
            fs.Close();
        }
        catch (IOException e)
        {
            Debug.Log(e);
        }
    }
}