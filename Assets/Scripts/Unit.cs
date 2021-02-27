using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour {

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

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (this.death)
            return;
        fireTimer += Time.deltaTime;
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
    

}
