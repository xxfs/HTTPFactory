using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///@brief 
///文件名称:CastReticle
///功能描述:
///数据表:
///作者:梅超
///日期:2017-12-6 17:25:34
///R1:
///修改作者:
///修改日期:
///修改理由:
public class CastReticle : Reticle
{

    /// <summary>
    /// @brief 调整枪口旋转的速度
    /// </summary>
    private float value = 100;

    public override void Awake()
    {
        base.Awake();
        //enableExternalGun = SystemParams.Instance().enableUDPGun;
    }

    public override void NetWorkControl(float x, float y, int status)
    {
        
    }    

    public override void LocalControl()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Rotate(Vector3.left * Time.deltaTime * value, Space.Self);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Rotate(Vector3.down * Time.deltaTime * value, Space.Self);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Rotate(Vector3.right * Time.deltaTime * value, Space.Self);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Rotate(Vector3.up * Time.deltaTime * value, Space.Self);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot(1);
        }
    }

    /// <summary>
    /// @brief 射击
    /// </summary>
    private void Shoot(int status)
    {
        var go = Instantiate(Resources.Load<GameObject>(bulletPath),transform.position,transform.rotation);
        var rig = go.GetComponent<Rigidbody>();
        rig.AddForce(transform.forward * 500);
    }
}
