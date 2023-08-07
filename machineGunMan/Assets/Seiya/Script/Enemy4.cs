using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : MonoBehaviour
{
    private Vector3 target = new Vector3(0.0f, 0.0f, -10.0f);
    float angle = 50;
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        transform.Rotate(new Vector3(0, 100, 0));

        //RotateAround(’†S‚ÌêŠ,²,‰ñ“]Šp“x)
        transform.RotateAround(
            target,
            Vector3.up * 5,
            angle * Time.deltaTime/100
            );
        //animator.SetBool("Walk", true);
    }
}
