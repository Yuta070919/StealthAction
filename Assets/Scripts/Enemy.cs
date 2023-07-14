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
    public GameObject[] RoutePoints;
    public GameObject EnemyObject;
    public GameObject PlayerPos;
    private int pointNumber = 0;
    public EnemyPhase Phase;
    public float MoveSpeed;
    private int enemyHealth;
    public int MaxEnemyHealth = 20;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = MaxEnemyHealth;
        enemyHealth = MaxEnemyHealth;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = (float)enemyHealth;
        RouteMove(RoutePoints, pointNumber,EnemyObject);
        pointNumber = PointCheck(RoutePoints, pointNumber, EnemyObject);
    }
    //敵の巡回移動の追加
    //敵の巡回ポイントへ移動
    public void RouteMove(GameObject[] RoutePoints, int pointNumber, GameObject EnemyObject)
    {
        switch (Phase)
        {
            case EnemyPhase.Patroll:
                EnemyObject.transform.LookAt(RoutePoints[pointNumber].transform.position);
                EnemyObject.transform.Translate(0, 0, MoveSpeed * Time.deltaTime);
                break;
            case EnemyPhase.Tracking:
                EnemyObject.transform.LookAt(PlayerPos.transform.position);
                if (Vector3.Distance(EnemyObject.transform.position, PlayerPos.transform.position) >= 2)
                {
                    EnemyObject.transform.Translate(0, 0, MoveSpeed * Time.deltaTime);
                }
                break;
                    
        }




    }
    //敵の巡回ポイントに到達したか確認(到達したら＋１)
    int PointCheck(GameObject[] RoutePoints, int pointNumber, GameObject EnemyObject)
    {
        if (Vector3.Distance(EnemyObject.transform.position,
            RoutePoints[pointNumber].transform.position) <= 1)
        {
            pointNumber++;
            if (pointNumber >= RoutePoints.Length)
            {
                pointNumber = 0;
                return pointNumber;
            }
            else
            {
                return pointNumber;
            }
        }
        else
        {
            return pointNumber;
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
        enemyHealth = enemyHealth - value;
        if (enemyHealth <= 0)
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
