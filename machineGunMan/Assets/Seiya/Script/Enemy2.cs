using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy2 : MonoBehaviour
{
   
    int num =1;
    float timer;
    public int a;
    public float farspeed;
    public Transform _self;
    public Transform _target;
    public Vector3 _forward = Vector3.forward;

    EnemyScript es;

    RawImage rawimage;
    void Update()
    {
        es = GetComponent<EnemyScript>();
        timer += Time.deltaTime;
        var dir = _target.position - _self.position;
        var lookAtRotation = Quaternion.LookRotation(dir, Vector3.up);
        var offsetRotation = Quaternion.FromToRotation(_forward, Vector3.forward);
        _self.rotation = lookAtRotation * offsetRotation;
        
        transform.position+=transform.right * Time.deltaTime * 3 * num * farspeed;
        if (timer > a)
        {
            num = -1;
        }
        if (timer > a*2)
        {
            num = 1;
            timer = 0;
        }
       
    }
}
