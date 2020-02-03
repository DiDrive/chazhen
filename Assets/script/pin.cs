using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pin : MonoBehaviour {
    private bool isFly = false;
    private bool isReach = false;
    private Transform startPoint;
    private Transform circle;
    private Vector3 circlePos;
    public float speed = 15;
	// Use this for initialization
	void Start () {
        startPoint = GameObject.Find("StartPoint").transform;
        circle = GameObject.Find("Circle").transform;
        circlePos = circle.position;
        circlePos.y -= 1.48f;
	}
	
	
	void Update () {
        if (isFly == false) {

            if (isReach == false)
            {
                transform.position=Vector3.MoveTowards(transform.position,startPoint.position,speed*Time.deltaTime);//使小针运动到startPoint位置

            }
            if (Vector3.Distance(transform.position, startPoint.position) < 0.05f) {
                isReach = true;

            }

        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position,circlePos,speed * Time.deltaTime);//使小针运动到圆的位置
            if (Vector3.Distance(transform.position, circlePos) < 0.05f) {
                transform.position = circlePos;//使每个小针差上后距离圆心的距离一样
                transform.parent = circle;//插在圆上
                isFly = false;//小针停止自己运动，不写这个小针可能会自己继续运动或旋转
            }//判断小针距离圆中心的距离如果小于0.05执行使小针跟随圆一起运动即插在圆上

        }
	}
    public void StartFly() {
        isFly = true;
        isReach = true;

    }
}
