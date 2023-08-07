using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dammy : MonoBehaviour
{
    float timer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 5)
        {
            Destroy(gameObject);
        }
    }
}
