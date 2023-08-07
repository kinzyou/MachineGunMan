using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atk : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Atk1", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Atk1()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name== "SandbagHitBox")
        {
            Destroy(gameObject);
        }
    }
}
