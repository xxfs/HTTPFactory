using System.Collections;
using System.Collections.Generic;
using Magicant.Util;
using UnityEngine;

///@brief 
///文件名称:GameController
///功能描述:
///数据表:
///作者:梅超
///日期:2018-1-2 14:23:35
///R1:
///修改作者:
///修改日期:
///修改理由:
public class GameController : Singleton<GameController>
{
    /// <summary>
    /// @brief 时间加速开关
    /// </summary>
    private bool gameSpeedFlag = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            //Time.timeScale = gameSpeedFlag ? SystemParams.Instance().timeScale : 1;
            //gameSpeedFlag = !gameSpeedFlag;
        }
    }
}
