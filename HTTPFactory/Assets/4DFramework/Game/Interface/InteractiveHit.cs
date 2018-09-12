using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///@brief 
///文件名称:InteractiveHit
///功能描述:受击接口
///作者:梅超
///日期:2017-8-23 13:41:9
///R1:
///修改作者:
///修改日期:
///修改理由:
public class InteractiveHit : MonoBehaviour {

    public EventHandler<HitArgs> Hit;

    public void OnHit(object sender, HitArgs e)
    {
        if (Hit != null) Hit.Invoke(sender, e);
    }
}

public class HitArgs : EventArgs
{
    public int damage;
    public Vector3 hitPoint;
    public Vector3 hitVelocity;

    public HitArgs()
    {
        
    }

    public HitArgs(int damage,Vector3 hitPoint)
    {
        this.damage = damage;
        this.hitPoint = hitPoint;
    }

    public HitArgs(int damage,Vector3 hitPoint,Vector3 hitVelocity)
    {
        this.damage = damage;
        this.hitPoint = hitPoint;
        this.hitVelocity = hitVelocity;
    }
}
