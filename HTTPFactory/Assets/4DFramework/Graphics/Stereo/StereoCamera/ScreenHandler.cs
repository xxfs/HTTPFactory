using System;
using UnityEngine;
using System.Collections;

///@brief 
///文件名称:ScreenType
///功能描述:
///数据表:
///作者:梅超
///日期:#CreateTime#
///R1:
///修改作者:
///修改日期:
///修改理由:
public class ScreenHandler : MonoBehaviour
{
    public BlendType screenType;

    void Start()
    {
        //if (!SystemParams.Instance().enableStereo) return;

#if !UNITY_EDITOR
        screenType = (BlendType)Enum.Parse(typeof(BlendType),SystemParams.Instance().stereoType,true);
#endif
        
        switch (screenType)
        {
            case BlendType.Blend1X2:
                this.gameObject.AddComponent<Blend1X2>();
                break;
            case BlendType.Blend1X3:
                this.gameObject.AddComponent<Blend1X3>();
                break;
            case BlendType.Stereo1X2:
                break;
            case BlendType.Stereo1X3:
                break;
            case BlendType.Stereo2X2:
                break;
        }
    }
}

