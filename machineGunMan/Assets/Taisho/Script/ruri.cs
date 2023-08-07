using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ruri : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyEffect", 2);
    }

    void DestroyEffect()
    {
        Destroy(this.gameObject);
    }
}
