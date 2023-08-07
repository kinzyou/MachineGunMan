using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.name)
        {
            case "ARbullet(Clone)":
               Destroy(gameObject);
                break;
            case "SGbullet(Clone)":
                Destroy(gameObject);
                break;
            case "SRbullet(Clone)":
                Destroy(gameObject);
                break;
            case "SandbagHitBox":
                Destroy(gameObject);
                break;
        }

        Destroy(this.gameObject, 15f);
    }
}
