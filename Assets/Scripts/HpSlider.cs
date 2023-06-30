using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpSlider : MonoBehaviour
{
    public GameObject Enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }


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
