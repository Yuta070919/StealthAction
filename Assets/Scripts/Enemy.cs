using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public GameObject[] routepoints;
    public GameObject EnemyObject;
    public int pointnumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RouteMove(routepoints, pointnumber,EnemyObject);
        pointnumber = PointCheck(routepoints, pointnumber, EnemyObject);
    }
    //敵の巡回移動の追加
    //敵の巡回ポイントへ移動
    static void RouteMove(GameObject[] routepoints,int pointnumber,GameObject EnemyObject)
    {
        EnemyObject.transform.LookAt(routepoints[pointnumber].transform.position);
        EnemyObject.transform.Translate(0, 0, 1 * Time.deltaTime);
    }
    //敵の巡回ポイントに到達したか確認(到達したら＋１)
    static int PointCheck(GameObject[] routepoints,int pointnumber,GameObject EnemyObject)
    {
        if (Vector3.Distance(EnemyObject.transform.position,
            routepoints[pointnumber].transform.position) <= 1)
        {
            pointnumber++;
            if (pointnumber >= routepoints.Length)
            {
                pointnumber = 0;
                return pointnumber;
            }
            else
            {
                return pointnumber;
            }
        }
        else
        {
            return pointnumber;
        }
    }
}
