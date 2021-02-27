using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Unit
{

    public event DeathNotify OnDeath;


    // Use this for initialization
    void Start () {
        this.ani = this.GetComponent<Animator>();
        this.Idle();
        initPos = this.transform.position;
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
