using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5 : MonoBehaviour
{    
    public float middlespeed ;
    [SerializeField] private Transform _self;
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _forward = Vector3.forward;

    GameObject spa;
    Spawn sp;

    int num = 1;
    int number;
    float timer;
    public float a;
    EnemyScript es;

    Animator animator;

    int[] enem1 = new int[3];
    float[] enemy = new float[3];

    bool w = true;

    ParticleSystem Smoke;
    public GameObject SmokeObject;

    void Start()
    {
        spa = GameObject.Find("Spawn");
        sp = spa.GetComponent<Spawn>();

        animator = GetComponent<Animator>();

        es = GetComponent<EnemyScript>();
        number  =Random.Range(1, 100);

        Smoke = SmokeObject.GetComponent<ParticleSystem>();

        Smoke.Play();
    }

    void Update()
    {        
        
        timer += Time.deltaTime;
        var dir = _target.position - _self.position;
        var lookAtRotation = Quaternion.LookRotation(dir, Vector3.up);
        var offsetRotation = Quaternion.FromToRotation(_forward, Vector3.forward);
        _self.rotation = lookAtRotation * offsetRotation;

        int enem;
        int count = 0;
        int[] Enemm = new int[100];

        for (int i = 0; i < 2; i++)
        {
            enem1[i] = i;
        }

        if (Spawn.wave <= 3)
        {
            enemy[0] = 1.00f;
            enemy[1] = 0.00f;
        }
        if (Spawn.wave >= 4)
        {
            enemy[0] = 0.75f;
            enemy[1] = 0.25f;
        }
        if(Spawn.wave >= 10)
        {
            enemy[0] = 0.25f;
            enemy[1] = 0.75f;
        }


        for (int i = 0; i < Enemm.Length; i++)
        {
            Enemm[i] = 0;
        }
        for (int i = 0; i < enem1.Length; i++)
        {
            if (enemy[i] != 0)
            {
                for (int j = 0; j < Mathf.Floor(enemy[i] * 100); j++)
                {
                    Enemm[count] = enem1[i];
                    count++;
                }
            }
        }
        Random.InitState(System.DateTime.Now.Millisecond);
        int randomIndex = Random.Range(0, Enemm.Length);
        enem = Enemm[randomIndex];

        if (enem == enem1[0])
        {
            transform.position += transform.forward * Time.deltaTime * middlespeed;
            animator.SetBool("Walk", true);
        }

        else 
        {
            transform.position += transform.forward * Time.deltaTime * middlespeed;
            animator.SetBool("Walk", true);
            if (w)
            {
                transform.position += transform.right * Time.deltaTime * 3 * num;
                if (timer > a)
                {
                    num = -1;
                }
                if (timer > a * 2)
                {
                    num = 1;
                    timer = 0;
                }
            }
           
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("woll"))
        {
            w = false;
            middlespeed = 1;
            animator.SetBool("Walk", false);
            animator.SetBool("Attack", true);

        }
    }
}
