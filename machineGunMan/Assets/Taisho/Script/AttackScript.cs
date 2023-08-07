using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public Camera mainCamera;
    public int bulletPower;

    void Start()
    {
        Application.targetFrameRate = 60;/*FPS‚ğ60‚ÉŒÅ’è*/
        mainCamera = Camera.main;

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))/*¶ƒNƒŠƒbƒN‚ğ‰Ÿ‚µ‚½‚ç*/
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out RaycastHit hit, 1000f))
            {
                hit.transform.GetComponent<EnemyScript>().TakeEnemyDamage(bulletPower);
            }
        }

        
    }
}
