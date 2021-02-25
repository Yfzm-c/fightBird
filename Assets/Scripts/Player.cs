using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour {

    public Rigidbody2D rigidbodyBird;//刚体
    public float force=100f;//力
    public Animator ani;//动画控制器

    private bool death = false;
    public delegate void DeathNotify();//委托，死亡通知

    public event DeathNotify OnDeath;//事件，死亡

    public UnityAction<int> OnScore;

    private Vector3 initPos;//小鸟初始化位置
    

	// Use this for initialization
	void Start () {
        this.ani = this.GetComponent<Animator>();
        this.Idle();
        initPos = this.transform.position;
	}

    public void Init()
    {
        this.transform.position = initPos;
        this.Idle();
        this.death = false;
    }
	
	// Update is called once per frame
	void Update () {

        //如果死亡就停止所有行为
        if (this.death)
            return;

		if(Input.GetMouseButtonDown(0))
        {
            //每次都从速度为0开始
            rigidbodyBird.velocity = Vector2.zero;
            //添加力
            rigidbodyBird.AddForce(new Vector2(0, force),ForceMode2D.Force);
        }
	}

    public void Idle()
    {

        this.rigidbodyBird.simulated = false;
        this.ani.SetTrigger("Idle");
    }
    public void Fly()
    {
        this.rigidbodyBird.simulated = true;
        this.ani.SetTrigger("Fly");
    }

    public void Die()
    {
        this.death = true;
        if(this.OnDeath!=null)
        {
            this.OnDeath();
        }
    }

     void OnCollisionEnter2D(Collision2D col)
    {
        this.Die();
    }

     void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("OnTriggerEnter2D:" + col.gameObject.name + ":" + gameObject.name + ":" + Time.time);
        if(col.gameObject.name.Equals("ScoreArea"))
        {

        }
        else
            this.Die();
    }

    void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("OnTriggerExit2D:" + col.gameObject.name + ":" + gameObject.name + ":" + Time.time);
        if (col.gameObject.name.Equals("ScoreArea"))
        {
            if (this.OnScore != null)
                this.OnScore(1);
        }
    }
}
