using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNoise : MonoBehaviour
{
    public GameObject[] enemy4;
    public CameraFilter cf;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        enemy4 = GameObject.FindGameObjectsWithTag("Enemy4");
        if(enemy4.Length ==0)
        {
            cf.GetComponent<CameraFilter>().enabled = false;
        }
        if(enemy4.Length >= 1)
        {
            cf.GetComponent<CameraFilter>().enabled =true;
        }
    }
}
