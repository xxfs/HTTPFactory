using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveAnimation : MonoBehaviour {

    public float delayTime = 2;
    public float durTime = 10;
    public float dis = 1.5f;


	// Use this for initialization
	void Start () {
        var endValue = this.transform.localPosition.x - dis;
        this.transform.DOLocalMoveX(endValue, durTime).SetLoops(-1,LoopType.Yoyo).SetDelay(delayTime);
	}
	
}
