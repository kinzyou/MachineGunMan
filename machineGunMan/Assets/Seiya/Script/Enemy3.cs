using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    public float nearspeed;
    public Transform _self;
    public Transform _target;
    public Vector3 _forward = Vector3.forward;

    Animator animator;

    ParticleSystem Aura;
    public GameObject AuraObject;


    private void Start()
    {
        animator = GetComponent<Animator>();
        Aura = AuraObject.GetComponent<ParticleSystem>();

        Aura.Play();
    }

    void Update()
    {
        
            transform.position += transform.forward * Time.deltaTime * nearspeed;
            var dir = _target.position - _self.position;
            var lookAtRotation = Quaternion.LookRotation(dir, Vector3.up);
            var offsetRotation = Quaternion.FromToRotation(_forward, Vector3.forward);
            _self.rotation = lookAtRotation * offsetRotation;
            animator.SetBool("Walk", true);



    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag==("woll"))
        {
            nearspeed =2;
            animator.SetBool("Walk", false);
            animator.SetBool("Attack", true);
        }
    }
   
}
