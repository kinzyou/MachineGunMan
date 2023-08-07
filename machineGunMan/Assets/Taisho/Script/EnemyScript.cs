using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyScript : MonoBehaviour
{
    public int MaxHP;/*�ő�̗�*/
    public int HP;/*���X�Ɍ��炵�Ă���HP�v�Z�Ɏg�p����*/
    private int finishHP;/*�ŏI�I��HP�v�Z�Ɏg��*/
    private float countTIme;/*��xHP���������ƌ��炵�Ă���̌o�ߎ���*/
    public float nextCountTime = 0;/*����HP�����炷�܂ł̎���*/
    private Slider HPSlider;/*HP�\���p�X���C�_�[*/
    private Slider bulkHPSlider;/*�ꊇHP�\���p�X���C�_�[*/
    private int damage = 0;/*���݂̃_���[�W��*/
    public int amountOfDamageAtOneTime = 100;/*���Ɍ��炷�_���[�W��*/
    private bool isReducing;/*HP�����炵�Ă��邩�ǂ���*/
    public float delayTime = 1f;/*HP�\���p�X���C�_�[�����炷�܂ł̑ҋ@����*/
    private int bulletPower;

    public AudioSource source;
    public AudioClip warningSound;

    GameObject et;
    EnemyText e_text;

    public ParticleSystem [] Hit;



    private void Start()
    {
        et = GameObject.Find("Enemy");
        e_text = et.GetComponent<EnemyText>();

        HPSlider = transform.Find("EnemyCanvas/HPSlider").GetComponent<Slider>();/*Canvas�̎q���ł���HPSlider�̃R���|�[�l���g���Q�b�g�����I*/
        bulkHPSlider = transform.Find("EnemyCanvas/BulkHPSlider").GetComponent<Slider>();/*�����͏�Ƃ���Ȃ������I*/

        HP = MaxHP;/*�J���ő̗͂�MAX�ɂ��Ƃ��܂��傤�˂�*/
        finishHP = MaxHP;/*�����͏�Ƃ���Ȃ������I*/

        HPSlider.value = 1;/*HP�̕\����̗�MAX��Ԃɂ��Ă������I*/
        bulkHPSlider.value = 1;/*�����͏�Ƃ���Ȃ������I*/
    }

    private void Update()
    {
        if (!isReducing)/*�_���[�W��������Ή������Ȃ��ɂ�*/
        {
            return;
        }

        if(countTIme >= nextCountTime)/*���Ɍ��炷���Ԃ�������*/
        {
            int tempDamage;

            /*���߂�ꂽ�ʂ����c��_���[�W�ʂ���������Ώ������ق������̃_���[�W�ɐݒ肷��ɂ�*/
            tempDamage = Mathf.Min(amountOfDamageAtOneTime, damage);
            HP -= tempDamage;

            HPSlider.value = (float)HP / MaxHP;/*�S�̂̔䗦�����߂�ɂ�*/
            damage -= tempDamage;/*�S�_���[�W�ʂ�����Ō��炵���_���[�W�ʂ����炷�ɂ�*/
            damage = Mathf.Max(damage, 0);/*�S�_���[�W�ʂ�0��艺�ɂȂ�����0��ݒ肷��ɂ�*/
           countTIme = 0;/*HP���������ƌ��炵�Ă���̌o�ߎ��Ԃ�0�ɖ߂��ɂ�*/

            if (damage <= 0)/*�_���[�W���Ȃ��Ȃ�����HP�o�[�̕ύX���������Ȃ��悤�ɂ���ɂ�*/
            {
                isReducing = false;
            }
           
            if (HP <= 0)/*HP��0�ȉ��ɂȂ�����G���O�b��ɍ폜����ɂ�*/
            {
                Instantiate(Hit[2], gameObject.transform.position, Quaternion.identity);
                e_text.count ++ ;
                Destroy(gameObject/*, 0.0f*/);
                
            }
        }

        countTIme += Time.deltaTime;
    }

    public void TakeEnemyDamage(int damage)/*�_���[�W�l��ǉ����郁�\�b�h�ɂ�*/
    {
        /*�_���[�W���󂯂��Ƃ��ɈꊇHP�p�̃o�[�̒l��ύX����ɂ�*/
        var tempHP = Mathf.Max(finishHP -= damage, 0);
        bulkHPSlider.value = (float)tempHP / MaxHP;
        this.damage += damage;
        countTIme = 0;

        /*��莞�Ԍ��HP�o�[�����炷�t���O��ݒ肷��ɂ�*/
        Invoke("StartReduceHP", delayTime);
    }

    public void StartReduceHP()/*���X��HP�o�[�����炷�̂��X�^�[�g*/
    {
        isReducing = true;
    }

    public void OnTriggerEnter(Collider other)
    {

        switch (other.gameObject.name)
        {
            case "ARbullet(Clone)":
                bulletPower = 1;
                TakeEnemyDamage(bulletPower);
                Hit[0].Play();
                break;

            case "SGbullet(Clone)":
                bulletPower = 1;
                TakeEnemyDamage(bulletPower);
                Hit[1].Play();
                break;
        }


        if (other.gameObject.tag == "Approach")/*�v���C���[�̎��ӂɋ߂Â�����*/
        {
            source.PlayOneShot(warningSound);/*�x��������*/
        }
    }
}
