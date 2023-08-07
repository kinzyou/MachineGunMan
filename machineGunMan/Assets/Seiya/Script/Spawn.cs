using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [Header("分布")]
     GameObject Camera;                 // 対象オブジェクト
    Transform CenterPosition;
   public int ArrangementMaxRedius;         // 配置位置の最大半径
    public int ArrangementMinRedius;         // 配置位置の最小半径
    public int ArrangementHeight;              // 配置位置の高さ

    [Header("個数")]
    [SerializeField] GameObject[] EnemyPrefab;                 // 対象オブジェクト

    [SerializeField] float appearNextTime;
    public int maxNumOfEnemys;
    public int numberOfEnemys;
    private float elapsedTime;

    public GameObject[] enemObj;
    public GameObject[] enem4;
    
    public static int wave = 1;
    float timer;
    private System.Random random;
    
    int Height;

    int[] enemID = new int[5];
    float[] enemm = new float[5];

    GameObject et;
    EnemyText etext;
    void Start()
    {
        Camera = GameObject.Find("Main Camera");
        CenterPosition = Camera.GetComponent<Transform>();

        et = GameObject.Find("Enemy");
        etext = et.GetComponent<EnemyText>();

        numberOfEnemys = 0;
        elapsedTime = 0f;
        wave = 30;
    }
  
    void Update()
    {
        enemObj = GameObject.FindGameObjectsWithTag("Enemy");
        enem4 = GameObject.FindGameObjectsWithTag("Enemy4");

        if (numberOfEnemys >= maxNumOfEnemys)
        {
            if (enemObj.Length == 0 && enem4.Length ==0)
            {
                timer += Time.deltaTime;
                if (timer > 3)
                {
                    wave++;
                    etext.count = 0;
                    timer = 0;
                    numberOfEnemys = 0;
                }
            }
            return;
        }
        elapsedTime += Time.deltaTime;
        if (elapsedTime > appearNextTime)
        {
            elapsedTime = 0f;
            Enemctrl();
        }
    }
    void Enemctrl()
    {
        int enem;
        int count = 0;
        int[] Enemmm = new int[100];

        for (int i = 0; i < 5; i++)
        {
            enemID[i] = i;
        }
       
        if(wave <= 3)
        {
            enemm[0] = 1.00f;
            enemm[1] = 0.00f;
            enemm[2] = 0.00f;
            enemm[3] = 0.00f;
            enemm[4] = 0.00f;
        }
        if (wave >= 4)
        {
            enemm[0] = 0.70f;
            enemm[1] = 0.30f;
            enemm[2] = 0.00f;
            enemm[3] = 0.00f;
            enemm[4] = 0.00f;
        }
        if (wave >= 5)
        {
            enemm[0] = 0.70f;
            enemm[1] = 0.30f;
            enemm[2] = 0.00f;
            enemm[3] = 0.00f;
            enemm[4] = 0.00f;
            maxNumOfEnemys = 7;
        }
        if(wave >= 7)
        {
            enemm[0] = 0.60f;
            enemm[1] = 0.20f;
            enemm[2] = 0.10f;
            enemm[3] = 0.10f;
            enemm[4] = 0.00f;
        }
        if(wave >= 8)
        {
            enemm[0] = 0.50f;
            enemm[1] = 0.20f;
            enemm[2] = 0.10f;
            enemm[3] = 0.10f;
            enemm[4] = 0.10f;
        }
        if(wave >= 10)
        {
            enemm[0] = 0.30f;
            enemm[1] = 0.30f;
            enemm[2] = 0.20f;
            enemm[3] = 0.10f;
            enemm[4] = 0.10f;
            maxNumOfEnemys = 10;
        }
        if (wave >= 15)
        {
            enemm[0] = 0.30f;
            enemm[1] = 0.30f;
            enemm[2] = 0.20f;
            enemm[3] = 0.10f;
            enemm[4] = 0.10f;
            maxNumOfEnemys = 15;
        }
        if (wave >= 20)
        {
            enemm[0] = 0.30f;
            enemm[1] = 0.30f;
            enemm[2] = 0.20f;
            enemm[3] = 0.10f;
            enemm[4] = 0.10f;
            maxNumOfEnemys = 20;
        }
        if (wave >= 25)
        {
            enemm[0] = 0.30f;
            enemm[1] = 0.30f;
            enemm[2] = 0.20f;
            enemm[3] = 0.10f;
            enemm[4] = 0.10f;
            maxNumOfEnemys = 25;
        } 
        if (wave >= 30)
        {
            enemm[0] = 0.30f;
            enemm[1] = 0.30f;
            enemm[2] = 0.20f;
            enemm[3] = 0.10f;
            enemm[4] = 0.10f;
            maxNumOfEnemys = 30;
        }



            for (int i = 0; i < Enemmm.Length; i++)
        {
            Enemmm[i] = 0;
        }
        for (int i = 0; i < enemID.Length; i++)
        {
            if (enemm[i] != 0)
            {
                for (int j = 0; j < Mathf.Floor(enemm[i] * 100); j++)
                {
                    Enemmm[count] = enemID[i];
                    count++;
                }
            }
        }
        Random.InitState(System.DateTime.Now.Millisecond);
        int randomIndex = Random.Range(0, Enemmm.Length);
        enem = Enemmm[randomIndex];

        random = new System.Random();
        Height = Random.Range(1, ArrangementHeight);
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
             if (enem == enemID[0])
             {
                 Instantiate(EnemyPrefab[0], (new Vector3(x, 0, z)) + CenterPosition.position, Quaternion.identity);
                 numberOfEnemys++;
             }
             if (enem == enemID[1])
             {
                Instantiate(EnemyPrefab[1], (new Vector3(x, Height, z)) + CenterPosition.position, Quaternion.identity);
                numberOfEnemys++;
             }
             if (enem == enemID[2])
             {
                Instantiate(EnemyPrefab[2], (new Vector3(x, -2, z)) + CenterPosition.position, Quaternion.identity);
                numberOfEnemys++;
             }
            if (enem == enemID[3])
            {
                Instantiate(EnemyPrefab[3], (new Vector3(x, -2, z)) + CenterPosition.position, Quaternion.identity);
                numberOfEnemys++;
            }
            if (enem == enemID[4])
            {
                Instantiate(EnemyPrefab[4], (new Vector3(x, 0, z)) + CenterPosition.position, Quaternion.identity);
                numberOfEnemys++;
            }
        }
    }   
}
