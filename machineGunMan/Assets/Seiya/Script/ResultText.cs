using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResultText : MonoBehaviour
{
    public GameObject result;
    int a;
    void Start()
    {
        Text text = result.GetComponent<Text>();
        a = (Spawn.wave);
        if (a > 0)
        {
            text.text = "a";
        }   
        if (a > 1)
        {
            text.text = "b";
        }
        if (a > 2)
        {
            text.text = "c";
        }
        if (a > 3)
        {
            text.text = "d";
        }
        if (a > 4)
        {
            text.text = "e";
        }
    } 
}