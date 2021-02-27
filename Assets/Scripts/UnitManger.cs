using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManger : MonoBehaviour
{

    public GameObject enemyTemplate;
    public GameObject enemy2Template;
    public GameObject enemy3Template;
    public float speed1 = 1f;
    public float speed2 = 2f;
    public float speed3 = 3f;

    public List<Enemy> enemies = new List<Enemy>();


    // Use this for initialization
    void Start()
    {

    }

    Coroutine runner = null;
    // Update is called once per frame
    void Update()
    {

    }

    public void Begin()
    {
        runner = StartCoroutine(GenetateEnemies());
    }

    public void Stop()
    {
        StopCoroutine(runner);
        this.enemies.Clear();
    }

    int timer1 = 0;
    int timer2 = 0;
    int timer3 = 0;

    IEnumerator GenetateEnemies()
    {
        while (true)
        {
            if (timer1 > speed1)
            {
                CreateEnemy(enemyTemplate);
                timer1 = 0;
            }
            if (timer2 > speed2)
            {
                CreateEnemy(enemy2Template);
                timer2 = 0;
            }
            if (timer3 > speed3)
            {
                CreateEnemy(enemy3Template);
                timer3 = 0;
            }
            timer1++;
            timer2++;
            timer3++;
            yield return new WaitForSeconds(1f);
        }
    }

    void CreateEnemy(GameObject templates)
    {
        if (templates == null)
        {
            return;
        }
        GameObject obj = Instantiate(templates, this.transform);
        Enemy p = obj.GetComponent<Enemy>();
        this.enemies.Add(p);

    }


}
