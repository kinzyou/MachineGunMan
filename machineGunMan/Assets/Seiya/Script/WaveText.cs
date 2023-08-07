using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WaveText : MonoBehaviour
{
    public GameObject wave_object = null;
    GameObject spawn;
    Spawn sp;
    int wavenum;
    void Start()
    {
       
    }

    void Update()
    {
        Text wave_text = wave_object.GetComponent<Text>();
        wave_text.text =  Spawn.wave + "";
    }    
}
