using System;
using System.Collections;
using System.IO;
using System.Reflection;
using UnityEngine;

/// <summary>
/// 配置系统参数
/// </summary>
public class SystemConfiger : MonoBehaviour
{
    void Awake()
    {
//#if !UNITY_EDITOR
        FileTools.ReadOnce();
        Destroy(this);
//#endif
    }
}
