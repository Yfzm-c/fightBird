using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour {

    public Rigidbody2D rigidbodyBird;//刚体
    public float speed = 100f;//速度
    public float fireRate = 10f;
    public float lifetime = 4f;
    public Animator ani;//动画控制器

    private bool death = false;
    public delegate void DeathNotify();//委托，死亡通知

    public event DeathNotify OnDeath;//事件，死亡

    public UnityAction<int> OnScore;

    private Vector3 initPos;//小鸟初始化位置

    public GameObject bulletTemplate;

    //private bool isFlying = false;


    // Use this for initialization
    void Start()
    {
        this.ani = this.GetComponent<Animator>();
        this.Fly();
        initPos = this.transform.position;
        Destroy(this.gameObject, lifetime );
    }

    public void Init()
    {
        this.transform.position = initPos;
        this.Idle();
        this.death = false;
    }

    float fireTimer = 0;

    // Update is called once per frame
    void Update()
    {

        //如果死亡就停止所有行为
        if (this.death)
            return;

        //if (!this.isFlying)
        //    return;

        fireTimer += Time.deltaTime;
        this.transform.position += new Vector3(-Time.deltaTime * speed, 0,0);
        this.Fire();
    }

    public void Fire()
    {
        if (fireTimer > 1 / fireRate)
        {
            GameObject go = Instantiate(bulletTemplate);
            go.transform.position = this.transform.position;
            go.GetComponent<Element>().direction = -1;
            fireTimer = 0f;
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
        if (this.OnDeath != null)
        {
            this.OnDeath();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //this.Die();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("OnTriggerEnter2D:" + col.gameObject.name + ":" + gameObject.name + ":" + Time.time);
        if (col.gameObject.name.Equals("ScoreArea"))
        {

        }
        //else
        //this.Die();
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
