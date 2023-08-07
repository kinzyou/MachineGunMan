using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLauncher : MonoBehaviour
{
    [Header("çUåÇ")]
    public GameObject bulletPrefab;
    public float interval;
    public float bulletSpeed;
    public GameObject cylinder;

    void Start()
    {
        cylinder.SetActive(false);
        InvokeRepeating("Shot", 2f, interval);
    }

    void Shot()
    {
        cylinder.SetActive(true);
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        bulletRb.AddForce(transform.forward * bulletSpeed * 100);

        Invoke("DisCover", 1f);
    }
    
    void DisCover()
    {
        cylinder.SetActive(false);
    }
}
