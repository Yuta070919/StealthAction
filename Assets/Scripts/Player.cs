using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int PlayerHP = 20;
    public Slider PlayerHPSlider;
    public float EnemyAttackCooldown = 1.0f;
    public float EnemyAttackCoolingTime = 0.0f;
    public int EnemyStrength = 1;
    public Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        PlayerHPSlider = PlayerHPSlider.GetComponent<Slider>();
        PlayerHPSlider.maxValue = PlayerHP;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerHPSlider.value = PlayerHP;
        KeyControll();
        ErrorCheck();
        if (enemy.Phase == EnemyPhase.Tracking)
        {
            EnemyAttackCoolingTime+=Time.deltaTime;
            if (EnemyAttackCoolingTime >= EnemyAttackCooldown)
            {
                EnemyAttackCoolingTime = 0;
                PlayerHP= PlayerHP - EnemyStrength;
                if (PlayerHP <= 0) SceneManager.LoadScene("GameOver");
            }
        }
    }
    //キーコントロールするやつ
    public void KeyControll()
    {
        if (Input.GetKey(KeyCode.A)) transform.position += new Vector3(-1 * Time.deltaTime, 0, 0);
        if (Input.GetKey(KeyCode.D)) transform.position += new Vector3(1 * Time.deltaTime, 0, 0);
        if (Input.GetKey(KeyCode.W)) transform.position += new Vector3(0, 0, 1 * Time.deltaTime);
        if (Input.GetKey(KeyCode.S)) transform.position += new Vector3(0, 0, -1 * Time.deltaTime);
        if (Input.GetKey(KeyCode.Q)) transform.Rotate(new Vector3(0, -2, 0), Space.Self);
        if (Input.GetKey(KeyCode.E)) transform.Rotate(new Vector3(0, 2, 0), Space.Self);
    }
    //エラー管理するやつ
    public void ErrorCheck()
    {
        if (EnemyAttackCooldown <= 0) Debug.LogError("EnemyAttackCooldownを0より高くするのだ");
        if (EnemyStrength <= 0) Debug.LogError("EnemyStrengthを0より高くするのだ");
    }
}
