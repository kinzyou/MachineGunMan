using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PlayerController : MonoBehaviour
{
    Ray CamRay;
    public Camera Mycam;

    public GameObject Rader;

    public GameObject[] BulletPrefab;

    public GameObject[] GunPrefab;//拾い、投げる方の武器
    public GameObject[] GunPrefab2;//手元にある武器

    GameObject ARobj;
    GameObject SGobj;//各種銃のオブジェクト
    GameObject SRobj;

    Rigidbody ARRB;
    Rigidbody SGRB;//各種銃のRigidbody
    Rigidbody SRRB;

    public GameObject ARbarrel;
    public GameObject SGbarrel;//弾の出所やで
    public GameObject SRbarrel;

    GameObject bullet;

    int Situation = 0;//0=武器無し、1=AR、2=SG、3=SR

    float ARcount;
    float SGcount;//発射感覚
    float SRcount;

    //ARのステータス
    float ARlimit;
    public int maxARlimit;
    int minARlimit;

    //SGのステータス
    float SGlimit;
    public int maxSGlimit;
    int minSGlimit;

    //SRのステータス
    float SRlimit;
    public int maxSRlimit;
    int minSRlimit;

    public int SRbulletPower;//SRの弾の威力

    public float ARspeed;
    public float SGspeed;//弾の弾速だで
    public float SRspeed;

    /*パーティクル関係。俺だけわかっていればいいやぁ*/
    ParticleSystem.MainModule ARSteamParticle;
    ParticleSystem.MainModule SGSteamParticle;
    ParticleSystem.MainModule SRSteamParticle;
    public GameObject ARSteamObject;
    public GameObject SGSteamObject;
    public GameObject SRSteamObject;
    ParticleSystem.MainModule ARSteamParticle2;
    ParticleSystem.MainModule SGSteamParticle2;
    ParticleSystem.MainModule SRSteamParticle2;
    public GameObject ARSteamObject2;
    public GameObject SGSteamObject2;
    public GameObject SRSteamObject2;
    ParticleSystem ARMuzzleFlash;
    public GameObject ARMuzzleFlashObject;
    ParticleSystem SGMuzzleFlash;
    public GameObject SGMuzzleFlashObject;
    ParticleSystem SRMuzzleFlash;
    public GameObject SRMuzzleFlashObject;

    public GameObject ARHitObject;
    public GameObject SGHitObject;
    public GameObject SRHitObject;
    float gorira;

    public AudioClip[] audioClips;

    AudioSource audioSource;
    
    /*視点移動で使っているやつ。別スクリプトから勝手に持ってきた*/
    Vector3 roteuler;
    public float minangle;//上の角度制限
    public float maxangle;//下の角度制限

    /*単押し長押しの判別に使っているマッチョマン*/
    float TapTime;

    public CinemachineVirtualCamera vCamera;
    int motimoti = 1;
    bool utu;

    void Start()
    {
        Application.targetFrameRate = 60;
        utu = true;

        ARcount = 0;
        SGcount = 0;//発射感覚の初期値
        SRcount = 0;

        ARlimit = 0;
        SGlimit = 0;//銃のクールダウンの初期値
        SRlimit = 0;

        ARobj = GameObject.Find("AR");
        SGobj = GameObject.Find("SG");//各銃のオブジェクトを取得や
        SRobj = GameObject.Find("SR");

        ARRB = ARobj.GetComponent<Rigidbody>();
        SGRB = SGobj.GetComponent<Rigidbody>();//各銃のRigidAbodyを取得やで
        SRRB = SRobj.GetComponent<Rigidbody>();

        ARSteamParticle = ARSteamObject.GetComponent<ParticleSystem>().main;
        SGSteamParticle = SGSteamObject.GetComponent<ParticleSystem>().main;
        SRSteamParticle = SRSteamObject.GetComponent<ParticleSystem>().main;
        ARSteamParticle.startSize = 0;
        SGSteamParticle.startSize = 0;
        SRSteamParticle.startSize = 0;

        ARSteamParticle2 = ARSteamObject2.GetComponent<ParticleSystem>().main;
        SGSteamParticle2 = SGSteamObject2.GetComponent<ParticleSystem>().main;
        SRSteamParticle2 = SRSteamObject2.GetComponent<ParticleSystem>().main;

        ARMuzzleFlash = ARMuzzleFlashObject.GetComponent<ParticleSystem>();
        SGMuzzleFlash = SGMuzzleFlashObject.GetComponent<ParticleSystem>();
        SRMuzzleFlash = SRMuzzleFlashObject.GetComponent<ParticleSystem>();

        roteuler = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0f);

        TapTime = 0;

        audioSource = GetComponent<AudioSource>();
    }

    public void Play(string seName)
    {
        switch (seName)
        {
            case "ARFire":
                audioSource.PlayOneShot(audioClips[0]);
                break;
            case "ARSetUp":
                audioSource.PlayOneShot(audioClips[1]);
                break;
            case "SGFire":
                audioSource.PlayOneShot(audioClips[2]);
                break;
            case "SGSetUp":
                audioSource.PlayOneShot(audioClips[3]);
                break;
            case "SGCocking":
                audioSource.PlayOneShot(audioClips[4]);
                break;
            case "SRFire":
                audioSource.PlayOneShot(audioClips[5]);
                break;
            case "SRSetUp":
                audioSource.PlayOneShot(audioClips[6]);
                break;
            case "SRCocking":
                audioSource.PlayOneShot(audioClips[7]);
                break;
            case "OverHeat":
                audioSource.PlayOneShot(audioClips[8]);
                break;
        }
    }

    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            utu = false;
            return;
        }

        //ARのクルーダウンのlimitを設定するやつ
        ARlimit = System.Math.Min(ARlimit, maxARlimit);
        ARlimit = System.Math.Max(ARlimit, minARlimit);

        //SGのクルーダウンのlimitを設定するやつ
        SGlimit = System.Math.Min(SGlimit, maxSGlimit);
        SGlimit = System.Math.Max(SGlimit, minSGlimit);

        //SRのクルーダウンのlimitを設定するやつ
        SRlimit = System.Math.Min(SRlimit, maxSRlimit);
        SRlimit = System.Math.Max(SRlimit, minSRlimit);

        ARcount += Time.deltaTime;
        SGcount += Time.deltaTime;/*銃の発射感覚*/
        SRcount += Time.deltaTime;

        ARlimit -= 10 * Time.deltaTime;
        SGlimit -= 15 * Time.deltaTime;/*銃のクールダウン*/
        SRlimit -= 10 * Time.deltaTime;

        if(TapTime <= 7)
        {
            Shot();
            Invoke("utanai", 0.1f);

        }

        if (Input.GetMouseButtonUp(0))
        {
            TapTime = 0;
        }
        
        if (Input.GetButton("Fire1"))//視点移動やで
        {
            TapTime += 1;
            float mouseInputX = Input.GetAxis("Mouse X");
            float mouseInputY = Input.GetAxis("Mouse Y");

            roteuler = new Vector3(Mathf.Clamp(roteuler.x - mouseInputY * -1 , minangle, maxangle) , roteuler.y + mouseInputX * -1, 0f);
            transform.localEulerAngles = roteuler;

        }

        //銃切り替えるときの処理
        if (Input.GetButtonUp("Fire1") && Situation == 0)//ARに切り替えるぞ
        {
            CamRay = Mycam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(CamRay, out hit))
            {
                if (hit.collider.CompareTag("AR"))
                {
                    audioSource.volume = 1;
                    audioSource.PlayOneShot(audioClips[1]);
                    GunPrefab2[0].SetActive(true);
                    GunPrefab[0].SetActive(false);
                    GunPrefab[0].transform.LookAt(Mycam.transform.position); 
                    GunPrefab[0].transform.position = new Vector3(Mycam.transform.position.x, Mycam.transform.position.y, Mycam.transform.position.z);
                    Situation = 1;
                }
            }
        }
        if (Input.GetButtonUp("Fire1") && Situation == 0)//SGに切り替えるぞ
        {
            CamRay = Mycam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(CamRay, out hit))
            {
                if (hit.collider.CompareTag("SG"))
                {
                    audioSource.volume = 1;
                    audioSource.PlayOneShot(audioClips[3]);
                    GunPrefab2[1].SetActive(true);
                    GunPrefab[1].SetActive(false);
                    GunPrefab[1].transform.LookAt(Mycam.transform.position);
                    GunPrefab[1].transform.position = new Vector3(Mycam.transform.position.x, Mycam.transform.position.y, Mycam.transform.position.z);
                    Situation = 2;
                }
            }
        }
        if (Input.GetButtonUp("Fire1") && Situation == 0)//SRに切り替えるぞ
        {
            CamRay = Mycam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(CamRay, out hit))
            {
                if (hit.collider.CompareTag("SR"))
                {
                    audioSource.volume = 1;
                    audioSource.PlayOneShot(audioClips[6]);
                    //Rader.SetActive(true);
                    GunPrefab2[2].SetActive(true);
                    GunPrefab[2].SetActive(false);
                    GunPrefab[2].transform.LookAt(Mycam.transform.position);
                    GunPrefab[2].transform.position = new Vector3(Mycam.transform.position.x, Mycam.transform.position.y, Mycam.transform.position.z);;
                    Situation = 3;
                }
            }
        }
        Steam();

        if(Situation == 0)
        {
            motimoti = 1;
        }
    }

    //ここから銃を撃った時の処理やで
    void AR()//アサルトを撃った時の処理やで
    {

        if (ARcount >= 1 && utu)
        {
            ARlimit += 3;

            if (ARlimit <= maxARlimit)
            {
                ARMuzzleFlash.Play();
                audioSource.volume = 1;
                audioSource.PlayOneShot(audioClips[0]);
                CamRay = Mycam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(CamRay, out hit))
                {
                    if (hit.collider.CompareTag("Enemy")|| hit.collider.CompareTag("Enemy4"))
                    {
                        bullet = Instantiate(BulletPrefab[0], ARbarrel.transform.position, Quaternion.identity);
                        Vector3 worldDir = CamRay.direction;
                        bullet.GetComponent<ARbullet>().Shot(worldDir * ARspeed);
                        ARcount = 1f;
                        Destroy(bullet, 5f);
                    }           
                    else
                    {
                        bullet = Instantiate(BulletPrefab[0], ARbarrel.transform.position, Quaternion.identity);
                        Vector3 worldDir = CamRay.direction;
                        bullet.GetComponent<ARbullet>().Shot(worldDir * ARspeed);
                        ARcount = 1f;
                        Destroy(bullet, 5f);
                    }
                }
            }
            else if(ARlimit >= 99)
            {
                audioSource.volume = 1;
                audioSource.PlayOneShot(audioClips[8]);
            }
        }
    }

    void SG()//ショットガンを撃った時の処理やで
    {
        if (SGcount >= 1 && utu)
        {
            SGlimit += 25;

            if (SGlimit <= maxSGlimit)
            {
                SGMuzzleFlash.Play();
                audioSource.volume = 1;
                audioSource.PlayOneShot(audioClips[2]);
                CamRay = Mycam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(CamRay, out hit))
                {
                    if (hit.collider.CompareTag("Enemy")|| hit.collider.CompareTag("Enemy4"))
                    {
                        for (int i = 0; i < 15; i++)
                        {   
                            bullet = Instantiate(BulletPrefab[1], SGbarrel.transform.position, Quaternion.identity);
                            Vector3 worldDir = CamRay.direction;
                            bullet.GetComponent<SGbullet>().Shot(worldDir * SGspeed);
                            Destroy(bullet,3f);
                            SGcount = 0;
                            Invoke("Cocking", 0.5f);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 15; i++)
                        {
                            bullet = Instantiate(BulletPrefab[1], SGbarrel.transform.position, Quaternion.identity);
                            Vector3 worldDir = CamRay.direction;
                            bullet.GetComponent<SGbullet>().Shot(worldDir * SGspeed);
                            Destroy(bullet, 3f);
                            SGcount = 0;
                            Invoke("Cocking", 0.5f);
                        }
                    }
                    
                }
            }
            else if (SGlimit >= 85)
            {
                audioSource.volume = 1;
                audioSource.PlayOneShot(audioClips[8]);
            }
        }
    }

    void Cocking()
    {
        audioSource.volume = 0.1f;
        audioSource.PlayOneShot(audioClips[4]);
    }

    void SR()//スナイパーライフルを撃った時の処理やで
    {
        if (SRcount >= 3 && utu)
        {
            SRlimit += 45;

            if (SRlimit <= maxSRlimit)
            {
                SRMuzzleFlash.Play();
                audioSource.volume = 1;
                audioSource.PlayOneShot(audioClips[5]);
                CamRay = Mycam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(CamRay, out hit))
                {
                    if (hit.collider.CompareTag("Enemy") || (hit.collider.CompareTag("Enemy4") ))
                    {
                        Instantiate(SRHitObject, hit.point, Quaternion.identity);

                        hit.transform.GetComponent<EnemyScript>().TakeEnemyDamage(SRbulletPower);
                        bullet = Instantiate(BulletPrefab[2], SRbarrel.transform.position, Quaternion.identity);
                        Vector3 worldDir = CamRay.direction;
                        bullet.GetComponent<SRbullet>().Shot(worldDir * SRspeed);
                        SRcount = 0;
                        Destroy(bullet, 0.1f);
                        Invoke("Cocking1", 1.8f);
                    }
                    else
                    {
                        Instantiate(SRHitObject, hit.point, Quaternion.identity);
                        bullet = Instantiate(BulletPrefab[2], SRbarrel.transform.position, Quaternion.identity);
                        Vector3 worldDir = CamRay.direction;
                        bullet.GetComponent<SRbullet>().Shot(worldDir * SRspeed);
                        SRcount = 0;
                        Destroy(bullet, 0.1f);
                        Invoke("Cocking1", 1.8f);
                    }
                    
                    if (hit.collider.gameObject.tag == "Enemy2")
                        Destroy(hit.collider.gameObject);
                }
            }
            else if (SRlimit >= 85)
            {
                audioSource.volume = 1;
                audioSource.PlayOneShot(audioClips[8]);
            }
        }
    }

    void Cocking1()
    {
        audioSource.volume = 1f;
        audioSource.PlayOneShot(audioClips[7]);
    }

    void Steam()
    {
        gorira = Mathf.InverseLerp(minARlimit, maxARlimit, ARlimit);
        ARSteamParticle.startSize = gorira;
        ARSteamParticle2.startSize = gorira;

        gorira = Mathf.InverseLerp(minSGlimit, maxSGlimit, SGlimit);
        SGSteamParticle.startSize = gorira;
        SGSteamParticle2.startSize = gorira;

        gorira = Mathf.InverseLerp(minSRlimit, maxSRlimit, SRlimit);
        SRSteamParticle.startSize = gorira;
        SRSteamParticle2.startSize = gorira;
    }
    
    void Shot()
    {
        if (Input.GetMouseButtonUp(0) && Situation == 0)
        {
            Debug.Log("なにやってんだよ！素手じゃ何もできないじゃんかよ！");
        }

        if (Input.GetMouseButtonUp(0) && Situation == 1)
        {
            AR();
        }

        if (Input.GetMouseButtonUp(0) && Situation == 2)
        {
            SG();
        }

        if (Input.GetMouseButtonUp(0) && Situation == 3)
        {
            SR();
        }
    }

    public void OnClick()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            utu = false;
            return;
        }

        //ここから武器を捨てるで
        switch (Situation)
        {
            case 1:
                GunPrefab[0].SetActive(true);
                GunPrefab2[0].SetActive(false);
                Situation = 0;
                vCamera.m_Lens.FieldOfView = 40;
                ARRB.AddForce(transform.forward * 6, ForceMode.Impulse);
                break;

            case 2: 
                GunPrefab2[1].SetActive(false);
                GunPrefab[1].SetActive(true);
                Situation = 0;
                vCamera.m_Lens.FieldOfView = 40;
                SGRB.AddForce(transform.forward * 6, ForceMode.Impulse);
                break;

            case 3:
                //Rader.SetActive(false);
                GunPrefab2[2].SetActive(false);
                GunPrefab[2].SetActive(true);
                Situation = 0;
                vCamera.m_Lens.FieldOfView = 40;
                SRRB.AddForce(transform.forward * 6, ForceMode.Impulse);
                break;
        }
    }

    //画面をズームさせるよー
    public void Zoom()
    {
        switch (Situation)
        {
            case 1:
                utu = false;
                vCamera.m_Lens.FieldOfView = 20;
                Invoke("utanai", 0.1f);
                break;

            case 2:
                utu = false;
                vCamera.m_Lens.FieldOfView = 35;
                Invoke("utanai", 0.1f);
                break;

            case 3:
                utu = false;
                vCamera.m_Lens.FieldOfView = 10;
                Invoke("utanai", 0.1f);
                break;
        }
        switch (motimoti)
        {
            case 1:
                motimoti += 1;
                break;

            case 2:
                vCamera.m_Lens.FieldOfView = 40;
                motimoti -= 1;
                break;
        }
    }

    //フラグと変数をutu、utanaiで取る奴おりゅう～？(煽)
    void utanai()
    {
        utu = true;
    }
}