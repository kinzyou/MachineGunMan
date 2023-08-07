using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstDammy : MonoBehaviour
{
    GameObject Camera;                 // 対象オブジェクト
    Transform CenterPosition;
    public int ArrangementMaxRedius;         
    public int ArrangementMinRedius;         
    private System.Random random;

    public GameObject instdammy;
    void Start()
    {
        Camera = GameObject.Find("Main Camera");
        CenterPosition = Camera.GetComponent<Transform>();

        InvokeRepeating("Dammy", 0.5f, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Dammy()
    {

        random = new System.Random();
       
        int x;
        int z;

        double xAbs;
        double zAbs;

        double maxR = Mathf.Pow(ArrangementMaxRedius, 2);
        double minR = Mathf.Pow(ArrangementMinRedius, 2);

        x = random.Next(-ArrangementMaxRedius, ArrangementMaxRedius);
        z = random.Next(-ArrangementMaxRedius, ArrangementMaxRedius);

        xAbs = Mathf.Abs(Mathf.Pow(x, 2));
        zAbs = Mathf.Abs(Mathf.Pow(z, 2));

        if (maxR > xAbs + zAbs && xAbs + zAbs > minR)
        {
            Instantiate(instdammy, (new Vector3(x, -1, z)) + CenterPosition.position, Quaternion.identity);
        }
    }
}
