using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipelineManger : MonoBehaviour {

    // Use this for initialization

    /*
     * 此脚本大致理解如下
     * 1.写GeneratePipeline()方法，目的是控制单根管道的生成
     * 2. IEnumerator GeneratePipelines()协程，目的是让管道逐个显示
     * 3.StartRun()开启协程
     * 4.Stop()关闭协程
     * 5.Init()初始化，主要用在RestartGame上面
     * */

    public GameObject template;
    List<Pipeline> pipelines = new List<Pipeline>();

	void Start () {
        	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    Coroutine coroutine = null;

    //初始化
    public void Init()
    {
        for (int i = 0; i < pipelines.Count; i++)
        {
            Destroy(pipelines[i].gameObject);   //销毁所有的管道
        }
        pipelines.Clear();  //清空列表
    }


    public void StartRun()
    {
            coroutine=StartCoroutine(GeneratePipelines());  //开始协程，创建管道
    }
    public void Stop()
    {
        StopCoroutine(coroutine);   //关闭协程
        for (int i = 0; i < pipelines.Count; i++)
            pipelines[i].enabled = false;   //让管道全都不显示
    }


    //协程
    IEnumerator GeneratePipelines()
    {
        for (int i=0;i<3;i++)
        {
            if (pipelines.Count < 3)    //如果没有管道，就创建管道
                GeneratePipeline();
            else
            {   //让管道显现出来，并且初始化位置
                pipelines[i].enabled = true;
                pipelines[i].Init();
            }
            yield return new WaitForSeconds(2f);
        }
    }


    //创建单根管道，并且把Pipeline脚本放到我们定义的列表里面
    void GeneratePipeline()
    {
        if (pipelines.Count < 3)
        {
            GameObject obj = Instantiate(template, this.transform);
            Pipeline p = obj.GetComponent<Pipeline>();
            pipelines.Add(p);
        }
    }
}
