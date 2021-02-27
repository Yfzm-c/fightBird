using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Unit
{
    public Rigidbody2D rigidbodyBird;
    public Animator ani;
    public float speed = 100f;
    public float fireRate = 10f;
    protected bool death = false;
    public delegate void DeathNotify();


    public UnityAction<int> OnScore;

    protected Vector3 initPos;

    public GameObject bulletTemplate;

    public float HP = 100f;
    public float MaxHP = 100f;

    float fireTimer = 0;
    public event DeathNotify OnDeath;


    // Use this for initialization
    void Start () {
        this.ani = this.GetComponent<Animator>();
        this.Idle();
        initPos = this.transform.position;
	}


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

    public void Init()
    {
        this.transform.position = initPos;
        this.Idle();
        this.death = false;
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
        Enemy enemy = col.gameObject.GetComponent<Enemy>();
        if (bullet == null && enemy==null)
        {
            return;
        }
        Debug.Log("Player:OnTriggerEnter2D:" + col.gameObject.name + ":" + gameObject.name + ":" + Time.time);
        if (bullet!=null && bullet.side == SIDE.ENEMY)
        {
            this.HP=this.HP-bullet.power;
            if (this.HP <= 0)
            {
                this.Die();
            }
        }

        if (enemy != null)
        {
            this.HP = 0;
            if (this.HP <= 0)
                this.Die();
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
