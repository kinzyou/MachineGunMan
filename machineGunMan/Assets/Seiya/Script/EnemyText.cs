using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyText : MonoBehaviour
{
    public GameObject enemy_object = null;
    GameObject spawn;
    Spawn sp;

    public int count;
    // Start is called before the first frame update
    void Start()
    {
        spawn = GameObject.Find("Spawn");
        sp = spawn.GetComponent<Spawn>();
    }

    // Update is called once per frame
    void Update()
    {
        int a = sp.maxNumOfEnemys - count;
        Text enemy_text = enemy_object.GetComponent<Text>();
        enemy_text.text = a + "";
    }
}
