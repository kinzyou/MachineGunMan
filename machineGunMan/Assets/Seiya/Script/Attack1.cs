using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1 : MonoBehaviour
{
    public GameObject attackPrefab;
    public float interval;
    public float AttackSpeed;
    void Start()
    {
        InvokeRepeating("Attack", 1, interval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Attack()
    {
        GameObject bullet = Instantiate(attackPrefab, transform.position, Quaternion.identity);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        bulletRb.AddForce(transform.forward * AttackSpeed * 100);
    }
}
