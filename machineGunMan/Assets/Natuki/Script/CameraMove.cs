using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Vector3 roteuler;

    public float minangle;//è„ÇÃäpìxêßå¿
    public float maxangle;//â∫ÇÃäpìxêßå¿

    
    void Start()
    {
        roteuler = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0f);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float mouseInputX = Input.GetAxis("Mouse X");
            float mouseInputY = Input.GetAxis("Mouse Y");

            roteuler = new Vector3(Mathf.Clamp(roteuler.x - mouseInputY,minangle, maxangle), roteuler.y + mouseInputX, 0f);
            transform.localEulerAngles = roteuler;
        } 
    }
}
