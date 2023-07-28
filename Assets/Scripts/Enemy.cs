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
    public int EnemyTaskDivider;
    public GameObject[] RoutePoints;
    public GameObject EnemyObject;
    public GameObject PlayerPos;
    private int pointNumber = 0;
    public EnemyPhase Phase;
    public float MoveSpeed;
    private int enemyHealth;
    public int MaxEnemyHealth = 20;
    public Slider slider;
    public float EnemyAttackCooldown = 1.0f;
    public float EnemyAttackCoolingTime = 0.0f;
    public Player player;
    float timer;
    float timer1;
    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = MaxEnemyHealth;
        enemyHealth = MaxEnemyHealth;
        timer = (float)-0.05 * EnemyTaskDivider;
        timer1 = (float)-0.05 * EnemyTaskDivider;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = (float)enemyHealth;
        timer += Time.deltaTime;
        timer1 = timer;
        RouteMove(RoutePoints, pointNumber,EnemyObject);
        if (timer >= 1)
        {
            pointNumber = PointCheck(RoutePoints, pointNumber, EnemyObject);
            timer = 0;
        }
    }
    //敵の巡回移動の追加
    //敵の巡回ポイントへ移動
    public void RouteMove(GameObject[] RoutePoints, int pointNumber, GameObject EnemyObject)
    {
        switch (Phase)
        {
            case EnemyPhase.Patroll:
                if (timer1 <= 1) EnemyObject.transform.LookAt(RoutePoints[pointNumber].transform.position);
                EnemyObject.transform.Translate(0, 0, MoveSpeed * Time.deltaTime);
                break;
            case EnemyPhase.Tracking:
                if (timer1 <= 1) EnemyObject.transform.LookAt(PlayerPos.transform.position);
                if (Vector3.Distance(EnemyObject.transform.position, PlayerPos.transform.position) >= 2)
                {
                    EnemyObject.transform.Translate(0, 0, MoveSpeed * Time.deltaTime);
                }
                EnemyAttackCoolingTime += Time.deltaTime;
                if (EnemyAttackCoolingTime >= EnemyAttackCooldown)
                {
                    EnemyAttackCoolingTime = 0;
                    player.PlayerHpReduce(1);
                }
                break;
                    
        }




    }
    //敵の巡回ポイントに到達したか確認(到達したら＋１)
    int PointCheck(GameObject[] RoutePoints, int pointNumber, GameObject EnemyObject)
    {
        Vector3 vector = EnemyObject.transform.position - RoutePoints[pointNumber].transform.position;
        if (Vector3.SqrMagnitude(vector) <= 1)
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
            Phase = EnemyPhase.Patroll;
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
