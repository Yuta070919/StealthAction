using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerater : MonoBehaviour
{
    public GameObject EnemyTemplate;
    public GameObject RoutePoint;
    public Player player;
    public GameObject Player;
    public int Interval;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= Interval) {
            GameObject enemy = Instantiate(EnemyTemplate);
            EnemyTemplate enemytemplate = enemy.GetComponent<EnemyTemplate>();
            enemytemplate.player = player;
            enemytemplate.Player = Player;
            enemytemplate.RouteCube = RoutePoint;
            timer = 0;
        }
    }
}
