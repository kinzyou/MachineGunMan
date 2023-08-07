using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextScript : MonoBehaviour
{
    public Text appearance;/*�������o���̃e�L�X�g*/
    public float speed;/*�����x���ς��X�s�[�h���Ǘ�����*/
    float red, green, blue, alpha;/*�e�L�X�g�̐F�A�s�����x���Ǘ�*/
    public bool isFadeOut = false;/*�t�F�[�h�A�E�g�̏����̊J�n�A�������Ǘ�����t���O*/
    public bool isFadeIn = false;/*�t�F�[�h�C���̏����̊J�n�A�������Ǘ�����t���O*/

    public Text enemyCountText;/*�c��̓G�̐����J�E���g���邽�߂̃e�L�X�g�������ɂԂ�����*/
    public int remainingEnemies;/*�c��̓G�̐�*/

    public GameObject da;
    void Start()
    {
        red = appearance.color.r;
        green = appearance.color.g;
        blue = appearance.color.b;
        alpha = appearance.color.a;

        
    }

    void Update()
    {
        if (isFadeIn)
        {
            StartFadeIn();
        }

        if (isFadeOut)
        {
            StartFadeOut();
        }

        

        if (Input.GetButtonDown("Jump"))
        {
            Instantiate(da);            
            StartCoroutine("Coru");
            
            Debug.Log("�ނ��[�񂾁[���ȁ[��[�߂́[���Ƃ́[");
        }


        enemyCountText.text = "����ǂ�������傢���̎c��l��" + remainingEnemies.ToString();
        Defeat();
    }

    void StartFadeIn()
    {
        alpha -= speed;/*�s�����x�����X�ɉ�����*/
        SetAlpha();/*�ύX�����s�����x���e�L�X�g�ɔ��f����*/
        if(alpha <= 0)/*���S�ɓ����ɂȂ����珈���𔲂���*/
        {
            isFadeIn = false;
        }
    }

    void StartFadeOut()
    {
        alpha += speed;/*�s�����x�����X�ɏグ��*/
        SetAlpha();/*�ύX�����s�����x���e�L�X�g�ɔ��f����*/
        if(alpha >= 1)/*���S�ɕs�����ɂȂ����珈���𔲂���*/
        {
            isFadeOut = false;
        }
    }
    
    void SetAlpha()
    {
        appearance.color = new Color(red, green, blue, alpha);
    }
    
    void Defeat()
    {
        remainingEnemies -= 1;
    }

    IEnumerator Coru()
    {
        isFadeOut = true;
        isFadeIn = false;
        yield return new WaitForSeconds(2);
        isFadeIn = true;
        isFadeOut = false;
    }

    
}
