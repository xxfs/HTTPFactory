using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///@brief 
///文件名称:Reticle
///功能描述:准心的基类
///数据表:
///作者:梅超
///日期:2017-12-6 17:24:19
///R1:
///修改作者:
///修改日期:
///修改理由:
public class Reticle : MonoBehaviour
{  
    /// <summary>
    /// @brief 屏幕上显示的准心
    /// </summary>
    public RectTransform cursor;

    /// <summary>
    /// @brief 子弹预制体路径
    /// </summary>
    public string bulletPath = "Bullet/DefaultBullet";

    /// <summary>
    /// @brief 子弹爆炸特效的路径
    /// </summary>
    public string particlePath = "Bullet/DefalutBulletParticle";

    /// <summary>
    /// @brief 子弹发射时候的声音
    /// </summary>
    public string audioPath = "GunSound";

    /// <summary>
    /// @brief 枪是否可用
    /// </summary>
    protected bool valid;

    /// <summary>
    /// @brief 是否启用外部的枪，比如UDP等枪
    /// </summary>
    protected bool enableExternalGun;

    /// <summary>
    /// @brief 枪的初始化
    /// </summary>
    public virtual void Awake()
    {
        //enableExternalGun = SystemParams.Instance().enableUDPGun;
    }

    /// <summary>
    /// @brief 通过网络数据来控制射击姿态，一般为<0,105,102,1>
    /// 该协议为恒润集团使用多年的协议，故不再变更
    /// 0表示补位，无意义
    /// 105表示虚拟屏幕的X坐标值(0~320)
    /// 102表示虚拟屏幕的Y坐标值(0~240)
    /// 1表示枪的触发信号
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="status"></param>
    public virtual void NetWorkControl(float x, float y, int status)
    {
        //if (!valid) return;
    }

    /// <summary>
    /// @brief 本地电脑控制
    /// </summary>
    /// <param name="status">枪的触发信号</param>
    public virtual void LocalControl()
    {
        //if (!valid) return;
    }

    /// <summary>
    /// @brief 设置枪是否可用
    /// </summary>
    /// <param name="valid"></param>
    public void SetGunValid(bool valid)
    {
        this.valid = valid;
        if (cursor) cursor.gameObject.SetActive(this.valid);
    }
}
