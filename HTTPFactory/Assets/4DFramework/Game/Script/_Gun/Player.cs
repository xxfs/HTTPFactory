using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///@brief 
///文件名称:Player
///功能描述:
/// 记录玩家分数
///数据表:
///作者:梅超
///日期:2017-12-6 13:25:36
///R1:
///修改作者:
///修改日期:
///修改理由:
public class Player : MonoBehaviour,IChangeScore
{
    /// <summary>
    /// @brief 玩家分数
    /// </summary>
    public int score;

    /// <summary>
    /// @brief 射击类型
    /// </summary>
    public ShootType shootType;

    /// <summary>
    /// @brief 准星
    /// </summary>
    public RectTransform cursor;

    /// <summary>
    /// @brief 准星基类
    /// </summary>
    private Reticle reticle;

    void Start()
    {
        switch (shootType)
        {
            case ShootType.ScreenReticle:
                reticle = this.gameObject.AddComponent<ScreenReticle>();
                reticle.cursor = cursor;
                reticle.SetGunValid(true);
                break;
            case ShootType.CastReticle:
                reticle = this.gameObject.AddComponent<CastReticle>();
                reticle.cursor = cursor;
                reticle.SetGunValid(true);
                break;
            case ShootType.JoystickReticle:

                break;
        }
    }

    void Update()
    {
        reticle.LocalControl();
    }

    /// <summary>
    /// @brief 分数变化
    /// </summary>
    /// <param name="score"></param>
    public void ChangeScore(int score)
    {
        this.score += score;
    }

    /// <summary>
    /// @brief 得到分数
    /// </summary>
    /// <returns></returns>
    public int GetScore()
    {
        return this.score;
    }
}

/// <summary>
/// @brief 射击类型，常见的影院射击有三种，屏幕坐标，投掷，摇杆
/// </summary>
public enum ShootType
{
    ScreenReticle,
    CastReticle,
    JoystickReticle
}

