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
    public GameObject[] PlayerAttackWeapons; 
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
                PlayerHP = PlayerHP - EnemyStrength;
                if (PlayerHP <= 0) SceneManager.LoadScene("GameOver");
            }
        } 
    }
    //キーコントロールするやつ
    public void KeyControll()
    {
        if (Input.GetKey(KeyCode.A)) transform.Translate(-1 * Time.deltaTime, 0, 0);
        if (Input.GetKey(KeyCode.D)) transform.Translate(1 * Time.deltaTime, 0, 0);
        if (Input.GetKey(KeyCode.W)) transform.Translate(0, 0, 1 * Time.deltaTime);
        if (Input.GetKey(KeyCode.S)) transform.Translate(0, 0, -1 * Time.deltaTime);
        if (Input.GetKey(KeyCode.Q)) transform.Rotate(new Vector3(0, -2, 0), Space.Self);
        if (Input.GetKey(KeyCode.E)) transform.Rotate(new Vector3(0, 2, 0), Space.Self);
        if (Input.GetKey(KeyCode.Alpha1)) PlayerChangeWeapons(0);
        if (Input.GetKey(KeyCode.Alpha2)) PlayerChangeWeapons(1);
        if (Input.GetKey(KeyCode.Alpha3)) PlayerChangeWeapons(2);
        if (Input.GetKey(KeyCode.Alpha4)) PlayerChangeWeapons(3);
        if (Input.GetKey(KeyCode.Alpha5)) PlayerChangeWeapons(4);
        if (Input.GetKey(KeyCode.Alpha6)) PlayerChangeWeapons(5);
        if (Input.GetKey(KeyCode.Alpha7)) PlayerChangeWeapons(6);
        if (Input.GetKey(KeyCode.Alpha8)) PlayerChangeWeapons(7);
        if (Input.GetKey(KeyCode.Alpha9)) PlayerChangeWeapons(8);
    }
    //エラー管理するやつ
    public void ErrorCheck()
    {
        if (EnemyAttackCooldown <= 0) Debug.LogError("EnemyAttackCooldownを0より高くするのだ");
        if (EnemyStrength <= 0) Debug.LogError("EnemyStrengthを0より高くするのだ");
    }
    public void PlayerChangeWeapons(int number)
    {
        if (number <= PlayerAttackWeapons.Length)
        {
            for (int i = 0; i < PlayerAttackWeapons.Length; i++)
            {
                PlayerAttackWeapons[i].SetActive(false);
            }
            PlayerAttackWeapons[number].SetActive(true);
        }
    }
}
