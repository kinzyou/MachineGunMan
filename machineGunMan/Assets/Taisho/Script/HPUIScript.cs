using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HPUIScript : MonoBehaviour
{


    private void LateUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;/*常にHPバーがカメラに向き続ける*/

        /*Updateは、ほかの処理の計算が途中だろうと毎フレーム呼ばれる*/
        /*LateUpdateも毎フレーム呼ばれるが、ほかの処理や計算が諸々終わってから最後に呼ばれる*/
        /*ほんでもってこのスクリプトは敵のキャンバスにくっついてる。絶対に忘れるからここに書いとく*/
    }

    
}
