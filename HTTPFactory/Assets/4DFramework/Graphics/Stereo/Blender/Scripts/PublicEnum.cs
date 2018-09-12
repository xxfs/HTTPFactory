using UnityEngine;
using System.Collections;

///@brief 
///文件名称:TagManagerData
///功能描述:该类用于存储项目tags，sortingLayers以及layers，
///        在打包成Unitypackage时候会将这些信息保存下来
///数据表:
///作者:梅超
///日期:#CreateTime#
///R1:
///修改作者:
///修改日期:
///修改理由:
public class TagManagerData
{
    public static string[] tags = new string[]
    {
        "UICamera"
    };

    public static string[] sortingLayers = new string[]
    {

    };

    public static string[] layers = new string[]
    {
       "Blend"
       ,"Enemy"
    };
}

public enum BlendType
{
    NoBlend
    ,Blend1X2
    ,Blend1X3
    , Stereo1X2
    , Stereo1X3
    , Stereo2X2
}

