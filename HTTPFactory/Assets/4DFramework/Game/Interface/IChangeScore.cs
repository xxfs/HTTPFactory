using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///@brief 
///文件名称:IChangeScore
///功能描述:获得分数接口
///数据表:
///作者:梅超
///日期:2017-12-6 15:27:9
///R1:
///修改作者:
///修改日期:
///修改理由:
public interface IChangeScore
{
    void ChangeScore(int score);
    int GetScore();
}
