using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpSlider : MonoBehaviour
{
    public GameObject Enemy;
    // Update is called once per frame
    void Update()
    {
        try
        {
            transform.position = Enemy.transform.position + new Vector3(0, 2, 0);
        }catch(MissingReferenceException)
        {
            Destroy(transform.gameObject);
        }
    }
}
