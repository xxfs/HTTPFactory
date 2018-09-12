using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

///@brief 
///文件名称:ScreenReticle
///功能描述:
///作者:梅超
///日期:2017-8-23 13:41:9
///R1:
///修改作者:
///修改日期:
///修改理由:
public class ScreenReticle : Reticle
{
    /// <summary>
    /// @brief 用于记录屏幕坐标
    /// </summary>
    private Vector3 screenPos = new Vector3(0,0,0.3f);

    /// <summary>
    /// @brief 虚拟屏幕宽度
    /// </summary>
    private float virtualWidth = 5760f;

    /// <summary>
    /// @brief 虚拟屏幕高度
    /// </summary>
    private float virtualHeigth = 1200f;

    /// <summary>
    /// @brief 准心枪的初始化
    /// </summary>
    public override void Awake()
    {
        base.Awake();
        //virtualWidth = SystemParams.Instance().imageWidth;
        //virtualHeigth = SystemParams.Instance().imageHeight; 
    }

    /// <summary>
    /// @brief 接收来自硬件陀螺仪的坐标数据以及手枪的状态
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="status"></param>
    public override void NetWorkControl(float x,float y,int status)
    {
        if (!valid || !enableExternalGun) return;
        screenPos.x = virtualWidth * x / 320.00f;
        screenPos.y = virtualHeigth * y / 240.00f;
        if (cursor) cursor.anchoredPosition3D = screenPos;//@brief 移动光标
        Shoot(status);
    }

    /// <summary>
    /// @brief 本地电脑模拟的控制数据
    /// </summary>
    public override void LocalControl()
    {
        if (!valid) return;
        if (Input.GetMouseButtonDown(0))
        {
            Shoot(1);
        }
        screenPos = Input.mousePosition;
        if(cursor)cursor.anchoredPosition3D = screenPos;
    }

    /// <summary>
    /// @brief 射击
    /// </summary>
    private void Shoot(int status)
    {
        if(status!=1) return;
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var hitArgs = new HitArgs(1, hit.point);
            if (hit.transform.GetComponent<InteractiveHit>())
            {
                hit.transform.GetComponent<InteractiveHit>().OnHit(this, hitArgs);
                Instantiate(Resources.Load<GameObject>(particlePath), hit.point, Quaternion.Euler(hit.normal));
            }
        }
        PlayShootVoice();        
        PlayAnimation();
    }

    /// <summary>
    /// 没击中时播放声音
    /// </summary>
    void PlayShootVoice()
    {
        AudioClip audioClip = Resources.Load<AudioClip>(audioPath);
        AudioSource audioSource = gameObject.GetComponent<AudioSource>() ? GetComponent<AudioSource>() : gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    /// <summary>
    /// @brief 按下扳机时候播放准心动画
    /// </summary>
    private Tweener tweener;
    private void PlayAnimation()
    {
        if (tweener != null) tweener.Complete();
        tweener = cursor.DOScale(0.8f * Vector2.one, 0.2f)
            .OnComplete(() => cursor.DOScale(1.2f * Vector2.one, 0.1f)
            .OnComplete(() => cursor.DOScale(1f * Vector2.one, 0.08f)));
    }

}
