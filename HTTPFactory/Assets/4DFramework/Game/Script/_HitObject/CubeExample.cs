using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

///@brief 
///文件名称:CubeExample
///功能描述:受击物体脚本编写示意
///数据表:
///作者:梅超
///日期:2017-12-6 16:41:7
///R1:
///修改作者:
///修改日期:
///修改理由:
[RequireComponent(typeof (InteractiveHit))]
public class CubeExample : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        //@brief 编写的脚本需要将受击行为挂载在InteractiveHit的受击事件上
        GetComponent<InteractiveHit>().Hit += Hit;
    }

    private void Hit(object sender, HitArgs hitArgs)
    {
        GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f),
            Random.Range(0f, 1f));
    }

}
