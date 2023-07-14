using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public Enemy enemy;
    public void OnTriggerEnter(Collider other)
    {
        //プレイヤーが索敵範囲に入ったらEnemy.csにPlayerを追いかけるように関数引き出し
        if (other.gameObject.name == "Player")
        {
            enemy.Phase = EnemyPhase.Tracking;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        //プレイヤーが索敵範囲に入ったらEnemy.csに巡回するように関数引き出し
        if (other.gameObject.name == "Player")
        {
            enemy.Phase = EnemyPhase.Patroll;
        }
    }
}
