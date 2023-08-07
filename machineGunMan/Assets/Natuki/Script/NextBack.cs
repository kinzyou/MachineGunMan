using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextBack : MonoBehaviour
{
    public GameObject[] Image;

    public void ChangeImage1()
    {
        Image[0].SetActive(false);
        Image[1].SetActive(true);
    }
}
