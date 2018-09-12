using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///@brief 
///文件名称:Bullet
///功能描述:
///数据表:
///作者:梅超
///日期:2017-12-6 17:11:53
///R1:
///修改作者:
///修改日期:
///修改理由:
public class Bullet : MonoBehaviour
{
    /// <summary>
    /// @brief 子弹上记录玩家信息
    /// </summary>
    [HideInInspector]
    public Player player;

    void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        //{
            var hitArgs = new HitArgs(1, collision.contacts[0].point);
            if (collision.transform.GetComponent<InteractiveHit>())
            {
                collision.transform.GetComponent<InteractiveHit>().OnHit(this, hitArgs);
                Instantiate(Resources.Load<GameObject>("Bullet/DefalutBulletParticle"), transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
        //}
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
