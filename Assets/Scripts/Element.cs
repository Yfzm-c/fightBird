using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour {

    public float speed;

    public int direction = 1;

    public SIDE side;
    public float power=1;
	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, 3f);
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.position += new Vector3(speed * Time.deltaTime*direction,0,0);
        if (Screen.safeArea.Contains(Camera.main.WorldToScreenPoint(this.transform.position)) == false)
        {
            Destroy(this.gameObject,1f);
        }
	}
}
