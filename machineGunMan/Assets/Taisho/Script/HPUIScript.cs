using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HPUIScript : MonoBehaviour
{


    private void LateUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;/*���HP�o�[���J�����Ɍ���������*/

        /*Update�́A�ق��̏����̌v�Z���r�����낤�Ɩ��t���[���Ă΂��*/
        /*LateUpdate�����t���[���Ă΂�邪�A�ق��̏�����v�Z�����X�I����Ă���Ō�ɌĂ΂��*/
        /*�ق�ł����Ă��̃X�N���v�g�͓G�̃L�����o�X�ɂ������Ă�B��΂ɖY��邩�炱���ɏ����Ƃ�*/
    }

    
}
