using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    private int test0;
    public int test1;
    [SerializeField] int test2;

    public GameObject Prefab;
    private int number;

    bool  destroy;
    bool  destroy2;
    bool  destroy3;

    void Start()
    {
        destroy = false;
        destroy2 = false;
        destroy3 = false;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Random.Range(-5.0f, 5.0f);
        float y = Random.Range(-5.0f, 5.0f);
        //float z = Random.Range(-5.0f, 5.0f);
        
        if (Input.GetButtonDown("Fire1"))
        {
            //number = Random.Range(0, Prefab.Length);
            Instantiate(Prefab/*[number]*/, new Vector3(x,y,0), Quaternion.identity);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Fire1();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            Fire2();
        }
        if (Input.GetButtonDown("Jump"))
        {
            Fire3();
        }

        if (destroy && destroy2 && destroy3)
        {
            /*GameObject[] Prefab = GameObject.FindGameObjectsWithTag("fire1");
            foreach (GameObject obj in Prefab)
            {
                Destroy(obj);
            }*/

            Destroy(GameObject.FindGameObjectWithTag("fire1"));

            destroy = false;
            destroy2 = false;
            destroy3 = false;
        }

    }

    void Fire1()
    {
        destroy = true;
    }void Fire2()
    {
        destroy2 = true;
    }void Fire3()
    {
        destroy3 = true;
    }
}
