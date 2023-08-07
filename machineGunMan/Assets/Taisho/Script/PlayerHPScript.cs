using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHPScript : MonoBehaviour
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
    public int enemy1Attack;/*Enemy1����󂯂�_���[�W*/
    public int enemy3Attack;/*Enemy3����󂯂�_���[�W*/
    public int enemyBulletDamage;/*�G�̒e����󂯂�_���[�W*/

    public Image damageImage;/*�_���[�W�\���Ɏg��Image���Ԃ�����*/

    private void Start()
    {
        HPSlider = GameObject.Find("Canvas/PlayerHPSlider").GetComponent<Slider>();/*Canvas�̎q���ł���HPSlider�̃R���|�[�l���g���Q�b�g�����I*/
        bulkHPSlider = GameObject.Find("Canvas/PlayerBulkHPSlider").GetComponent<Slider>();/*�����͏�Ƃ���Ȃ������I*/

        HP = MaxHP;/*�J���ő̗͂�MAX�ɂ��Ƃ��܂��傤�˂�*/
        finishHP = MaxHP;/*�����͏�Ƃ���Ȃ������I*/

        HPSlider.value = 1;/*HP�̕\����̗�MAX��Ԃɂ��Ă������I*/
        bulkHPSlider.value = 1;/*�����͏�Ƃ���Ȃ������I*/

    }

    

    private void Update()
    {
        damageImage.color = Color.Lerp(damageImage.color, Color.clear, Time.deltaTime);/*Image�̃A���t�@�l���ǂ�ǂ񉺂��Ă�*/


        if (!isReducing)/*�_���[�W��������Ή������Ȃ��ɂ�*/
        {
            return;
        }

        if (countTIme >= nextCountTime)/*���Ɍ��炷���Ԃ�������*/
        {
            int tempDamage;
            damageImage.color = new Color(0.5f, 0, 0, 0.5f);/*��e���ɉ�ʂ�Ԃ�����*/

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

            if(HP <= 0)
            {
                Invoke("changescene", 0.5f);
            }
        }

        countTIme += Time.deltaTime;
    }

    public void TakeDamage(int damage)/*�_���[�W�l��ǉ����郁�\�b�h�ɂ�*/
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

    private void OnTriggerStay(Collider other)
    {
        

        switch (other.gameObject.name)/*�G�̖��O�ɂ���Ď󂯂�_���[�W���ς��*/
        {
            case "Atk1(Clone)":
                TakeDamage(enemy1Attack);

                break;


            
            case "Atk3(Clone)":
                TakeDamage(enemy3Attack);

                break;


            case "FlameAttack(Clone)":
                TakeDamage(enemyBulletDamage);

                break;

                
        }
    }

    void changescene()
    {
        SceneManager.LoadScene("ResultScene");
    }

    
}
