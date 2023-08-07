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

    public GameObject[] GunPrefab;//�E���A��������̕���
    public GameObject[] GunPrefab2;//�茳�ɂ��镐��

    GameObject ARobj;
    GameObject SGobj;//�e��e�̃I�u�W�F�N�g
    GameObject SRobj;

    Rigidbody ARRB;
    Rigidbody SGRB;//�e��e��Rigidbody
    Rigidbody SRRB;

    public GameObject ARbarrel;
    public GameObject SGbarrel;//�e�̏o�����
    public GameObject SRbarrel;

    GameObject bullet;

    int Situation = 0;//0=���햳���A1=AR�A2=SG�A3=SR

    float ARcount;
    float SGcount;//���ˊ��o
    float SRcount;

    //AR�̃X�e�[�^�X
    float ARlimit;
    public int maxARlimit;
    int minARlimit;

    //SG�̃X�e�[�^�X
    float SGlimit;
    public int maxSGlimit;
    int minSGlimit;

    //SR�̃X�e�[�^�X
    float SRlimit;
    public int maxSRlimit;
    int minSRlimit;

    public int SRbulletPower;//SR�̒e�̈З�

    public float ARspeed;
    public float SGspeed;//�e�̒e������
    public float SRspeed;

    /*�p�[�e�B�N���֌W�B�������킩���Ă���΂����₟*/
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
    
    /*���_�ړ��Ŏg���Ă����B�ʃX�N���v�g���珟��Ɏ����Ă���*/
    Vector3 roteuler;
    public float minangle;//��̊p�x����
    public float maxangle;//���̊p�x����

    /*�P�����������̔��ʂɎg���Ă���}�b�`���}��*/
    float TapTime;

    public CinemachineVirtualCamera vCamera;
    int motimoti = 1;
    bool utu;

    void Start()
    {
        Application.targetFrameRate = 60;
        utu = true;

        ARcount = 0;
        SGcount = 0;//���ˊ��o�̏����l
        SRcount = 0;

        ARlimit = 0;
        SGlimit = 0;//�e�̃N�[���_�E���̏����l
        SRlimit = 0;

        ARobj = GameObject.Find("AR");
        SGobj = GameObject.Find("SG");//�e�e�̃I�u�W�F�N�g���擾��
        SRobj = GameObject.Find("SR");

        ARRB = ARobj.GetComponent<Rigidbody>();
        SGRB = SGobj.GetComponent<Rigidbody>();//�e�e��RigidAbody���擾���
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

        //AR�̃N���[�_�E����limit��ݒ肷����
        ARlimit = System.Math.Min(ARlimit, maxARlimit);
        ARlimit = System.Math.Max(ARlimit, minARlimit);

        //SG�̃N���[�_�E����limit��ݒ肷����
        SGlimit = System.Math.Min(SGlimit, maxSGlimit);
        SGlimit = System.Math.Max(SGlimit, minSGlimit);

        //SR�̃N���[�_�E����limit��ݒ肷����
        SRlimit = System.Math.Min(SRlimit, maxSRlimit);
        SRlimit = System.Math.Max(SRlimit, minSRlimit);

        ARcount += Time.deltaTime;
        SGcount += Time.deltaTime;/*�e�̔��ˊ��o*/
        SRcount += Time.deltaTime;

        ARlimit -= 10 * Time.deltaTime;
        SGlimit -= 15 * Time.deltaTime;/*�e�̃N�[���_�E��*/
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
        
        if (Input.GetButton("Fire1"))//���_�ړ����
        {
            TapTime += 1;
            float mouseInputX = Input.GetAxis("Mouse X");
            float mouseInputY = Input.GetAxis("Mouse Y");

            roteuler = new Vector3(Mathf.Clamp(roteuler.x - mouseInputY * -1 , minangle, maxangle) , roteuler.y + mouseInputX * -1, 0f);
            transform.localEulerAngles = roteuler;

        }

        //�e�؂�ւ���Ƃ��̏���
        if (Input.GetButtonUp("Fire1") && Situation == 0)//AR�ɐ؂�ւ��邼
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
        if (Input.GetButtonUp("Fire1") && Situation == 0)//SG�ɐ؂�ւ��邼
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
        if (Input.GetButtonUp("Fire1") && Situation == 0)//SR�ɐ؂�ւ��邼
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

    //��������e�����������̏������
    void AR()//�A�T���g�����������̏������
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

    void SG()//�V���b�g�K�������������̏������
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

    void SR()//�X�i�C�p�[���C�t�������������̏������
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
            Debug.Log("�Ȃɂ���Ă񂾂�I�f�肶�ች���ł��Ȃ�����񂩂�I");
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

        //�������畐����̂Ă��
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

    //��ʂ��Y�[���������[
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

    //�t���O�ƕϐ���utu�Autanai�Ŏ��z����イ�`�H(��)
    void utanai()
    {
        utu = true;
    }
}