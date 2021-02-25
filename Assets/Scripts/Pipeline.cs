using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipeline : MonoBehaviour {
    /*
     * 控制单根管道预制物的初始化位置和移动方向和速度
     * 
     * 
     * */

    public float speed;
    public float minRange;
    public float maxRange;
	// Use this for initialization
	void Start () {
        this.Init();
	}

    float t = 0;
    
    //初始化位置
    public void Init()
    {
        float y = Random.Range(minRange, maxRange);
        this.transform.localPosition = new Vector3(0, y, 0);
    }
	
	//控制移动
	void Update () {
        this.transform.position += new Vector3(-speed, 0) * Time.deltaTime;
        t += Time.deltaTime;
        if(t>6f)
        {
            t = 0;
            this.Init();
        }
	}
}
