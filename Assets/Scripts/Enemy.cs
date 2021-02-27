using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : Unit
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
    public float lifetime = 4f;

    public event DeathNotify OnDeath;//事件，死亡


    public ENEMY_TYPE enemyType;

    public Vector2 range;

    float initY = 0;

    //private bool isFlying = false;


    // Use this for initialization
    void Start()
    {
        this.ani = this.GetComponent<Animator>();
        this.Fly();
        initPos = this.transform.position;
        Destroy(this.gameObject, lifetime );
        initY = Random.Range(range.x, range.y);
        this.transform.position = new Vector3(8f, initY, 0);
    }


    // Update is called once per frame
    void Update()
    {
        if (this.death)
            return;
        fireTimer += Time.deltaTime;
        float y = 0;
        if (this.enemyType == ENEMY_TYPE.SWING_ENEMY)
        {
            y = Mathf.Sin(Time.timeSinceLevelLoad)*3f;
        }
        this.transform.position = new Vector3(this.transform.position.x-Time.deltaTime * speed, initY+y);
        this.Fire();
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
        this.ani.SetTrigger("Die");
        if (this.OnDeath != null)
        {
            this.OnDeath();
        }
        Destroy(this.gameObject,0.2f);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Enemy:OnCollisionEnter2D:" + col.gameObject.name + ":" + gameObject.name + ":" + Time.time);
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
        if (bullet.side == SIDE.PLAYER)
        {
            this.Die();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("Enemy:OnTriggerExit2D:" + col.gameObject.name + ":" + gameObject.name + ":" + Time.time);
        if (col.gameObject.name.Equals("ScoreArea"))
        {
            if (this.OnScore != null)
                this.OnScore(1);
        }
    }
}
