using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyPhase
{
    Patroll,
    Tracking
}

public class Enemy : MonoBehaviour
{
    public GameObject[] routepoints;
    public GameObject EnemyObject;
    public GameObject PlayerPos;
    public int pointnumber = 0;
    public EnemyPhase Phase;
    public float movespeed;
    public int EnemyHealth = 20;
    public int MaxEnemyHealth = 20;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = slider.GetComponent<Slider>();
        slider.maxValue = MaxEnemyHealth;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = (float)EnemyHealth;
        RouteMove(routepoints, pointnumber,EnemyObject);
        pointnumber = PointCheck(routepoints, pointnumber, EnemyObject);
    }
    //敵の巡回移動の追加
    //敵の巡回ポイントへ移動
    public void RouteMove(GameObject[] routepoints, int pointnumber, GameObject EnemyObject)
    {
        switch (Phase)
        {
            case EnemyPhase.Patroll:
                EnemyObject.transform.LookAt(routepoints[pointnumber].transform.position);
                EnemyObject.transform.Translate(0, 0, movespeed * Time.deltaTime);
                break;
            case EnemyPhase.Tracking:
                EnemyObject.transform.LookAt(PlayerPos.transform.position);
                if (Vector3.Distance(EnemyObject.transform.position, PlayerPos.transform.position) >= 2)
                {
                    EnemyObject.transform.Translate(0, 0, movespeed * Time.deltaTime);
                }
                break;
                    
        }




    }
    //敵の巡回ポイントに到達したか確認(到達したら＋１)
    int PointCheck(GameObject[] routepoints, int pointnumber, GameObject EnemyObject)
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
    //Playerを追いかける関数(巡回を停止)
    public void TargetPlayer()
    {
        Phase = EnemyPhase.Tracking;
    }
    //HPを減らす
    void ReduceHealth(int value)
    {
        EnemyHealth = EnemyHealth - value;
        if (EnemyHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (collision.gameObject.tag == "PlayerAttackArea")
            {
                ReduceHealth(1);
            }
        }
    }
}
