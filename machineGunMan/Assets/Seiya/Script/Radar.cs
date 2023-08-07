using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
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
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        if (Input.GetMouseButton(0))
        {
            float mouseInputX = Input.GetAxis("Mouse X");
            float mouseInputY = Input.GetAxis("Mouse Y");

            roteuler = new Vector3(Mathf.Clamp(roteuler.x - mouseInputY * -1 , minangle, maxangle), roteuler.y + mouseInputX * -1, 0f);
            transform.localEulerAngles = roteuler;
        }
    }
}