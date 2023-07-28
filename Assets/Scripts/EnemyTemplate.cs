using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTemplate : MonoBehaviour
{
    public Enemy Enemy;
    public GameObject RouteCube;
    public GameObject Player;
    public Player player;
    private GameObject[] routePoints;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 4;i++)
        {
            routePoints[i] = Instantiate(RouteCube);
            Enemy.RoutePoints[i] = routePoints[i];
        }
        Enemy.PlayerPos = Player;
        Enemy.player = player;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
