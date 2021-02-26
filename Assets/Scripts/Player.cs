using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour {

    public Rigidbody2D rigidbodyBird;//刚体
    public float speed=100f;//速度
    public float fireRate = 10f;
    public Animator ani;//动画控制器

    private bool death = false;
    public delegate void DeathNotify();//委托，死亡通知

    public event DeathNotify OnDeath;//事件，死亡

    public UnityAction<int> OnScore;

    private Vector3 initPos;//小鸟初始化位置

    public GameObject bulletTemplate;

    public float HP = 100f;

    //private bool isFlying = false;
    

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

    float fireTimer = 0;

	// Update is called once per frame
	void Update () {

        //如果死亡就停止所有行为
        if (this.death)
            return;

        //if (!this.isFlying)
           // return;
        
        fireTimer += Time.deltaTime;

        Vector2 pos = this.transform.position;

        pos.x += Input.GetAxis("Horizontal") * Time.deltaTime*speed;
        pos.y += Input.GetAxis("Vertical") * Time.deltaTime*speed;
        this.transform.position = pos;
        if (Input.GetButton("Fire1"))
        {
            this.Fire();
        }
	}

    public void Fire()
    {
        if (fireTimer > 1 / fireRate)
        {
            GameObject go = Instantiate(bulletTemplate);
            go.transform.position = this.transform.position;

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
        if(this.OnDeath!=null)
        {
            this.OnDeath();
        }
    }

     void OnCollisionEnter2D(Collision2D col)
    {
        
        Debug.Log("Player:OnCollisionEnter2D:" + col.gameObject.name + ":" + gameObject.name + ":" + Time.time);
        //this.Die();
    }

     void OnTriggerEnter2D(Collider2D col)
    {
        Element bullet = col.gameObject.GetComponent<Element>();
        if (bullet == null)
        {
            return;
        }
        Debug.Log("Player:OnTriggerEnter2D:" + col.gameObject.name + ":" + gameObject.name + ":" + Time.time);
        if (bullet.side == SIDE.ENEMY)
        {
            this.HP=this.HP-bullet.power;
            if (this.HP <= 0)
            {
                this.Die();
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("Player:OnTriggerExit2D:" + col.gameObject.name + ":" + gameObject.name + ":" + Time.time);
        if (col.gameObject.name.Equals("ScoreArea"))
        {
            if (this.OnScore != null)
                this.OnScore(1);
        }
    }
}
