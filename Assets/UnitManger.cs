using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManger : MonoBehaviour
{

    public GameObject enemyTemplate;
    public float speed = 3f;

    public List<Enemy> enemies = new List<Enemy>();

    public Vector2 range;

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


    IEnumerator GenetateEnemies()
    {
        while (true)
        {
            CreateEnemy();
            yield return new WaitForSeconds(speed);
        }
    }

    void CreateEnemy()
    {

        GameObject obj = Instantiate(enemyTemplate, this.transform);
        Enemy p = obj.GetComponent<Enemy>();
        this.enemies.Add(p);

        float y = Random.Range(range.x, range.y);
        obj.transform.localPosition = new Vector3(0, y, 0);
    }


}
