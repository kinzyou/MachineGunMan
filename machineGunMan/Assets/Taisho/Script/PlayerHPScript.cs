using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHPScript : MonoBehaviour
{
    public int MaxHP;/*最大体力*/
    public int HP;/*徐々に減らしていくHP計算に使用する*/
    private int finishHP;/*最終的なHP計算に使う*/
    private float countTIme;/*一度HPをごそっと減らしてからの経過時間*/
    public float nextCountTime = 0;/*次にHPを減らすまでの時間*/
    private Slider HPSlider;/*HP表示用スライダー*/
    private Slider bulkHPSlider;/*一括HP表示用スライダー*/
    private int damage = 0;/*現在のダメージ量*/
    public int amountOfDamageAtOneTime = 100;/*一回に減らすダメージ量*/
    private bool isReducing;/*HPを減らしているかどうか*/
    public float delayTime = 1f;/*HP表示用スライダーを減らすまでの待機時間*/
    public int enemy1Attack;/*Enemy1から受けるダメージ*/
    public int enemy3Attack;/*Enemy3から受けるダメージ*/
    public int enemyBulletDamage;/*敵の弾から受けるダメージ*/

    public Image damageImage;/*ダメージ表現に使うImageをぶち込む*/

    private void Start()
    {
        HPSlider = GameObject.Find("Canvas/PlayerHPSlider").GetComponent<Slider>();/*Canvasの子供であるHPSliderのコンポーネントをゲットだぜ！*/
        bulkHPSlider = GameObject.Find("Canvas/PlayerBulkHPSlider").GetComponent<Slider>();/*説明は上とおんなじだぜ！*/

        HP = MaxHP;/*開幕で体力をMAXにしときましょうねぇ*/
        finishHP = MaxHP;/*説明は上とおんなじだぜ！*/

        HPSlider.value = 1;/*HPの表示を体力MAX状態にしておくぜ！*/
        bulkHPSlider.value = 1;/*説明は上とおんなじだぜ！*/

    }

    

    private void Update()
    {
        damageImage.color = Color.Lerp(damageImage.color, Color.clear, Time.deltaTime);/*Imageのアルファ値をどんどん下げてる*/


        if (!isReducing)/*ダメージが無ければ何もしないにょ*/
        {
            return;
        }

        if (countTIme >= nextCountTime)/*次に減らす時間が来たら*/
        {
            int tempDamage;
            damageImage.color = new Color(0.5f, 0, 0, 0.5f);/*被弾時に画面を赤くする*/

            /*決められた量よりも残りダメージ量が小さければ小さいほうを一回のダメージに設定するにょ*/
            tempDamage = Mathf.Min(amountOfDamageAtOneTime, damage);
            HP -= tempDamage;

            HPSlider.value = (float)HP / MaxHP;/*全体の比率を求めるにょ*/
            damage -= tempDamage;/*全ダメージ量から一回で減らしたダメージ量を減らすにょ*/
            damage = Mathf.Max(damage, 0);/*全ダメージ量が0より下になったら0を設定するにょ*/
            countTIme = 0;/*HPをごそっと減らしてからの経過時間を0に戻すにょ*/

            if (damage <= 0)/*ダメージがなくなったらHPバーの変更処理をしないようにするにょ*/
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

    public void TakeDamage(int damage)/*ダメージ値を追加するメソッドにょ*/
    {
        /*ダメージを受けたときに一括HP用のバーの値を変更するにょ*/
        var tempHP = Mathf.Max(finishHP -= damage, 0);
        bulkHPSlider.value = (float)tempHP / MaxHP;
        this.damage += damage;
        countTIme = 0;

        /*一定時間後にHPバーを減らすフラグを設定するにょ*/
        Invoke("StartReduceHP", delayTime);
    }

    public void StartReduceHP()/*徐々にHPバーを減らすのをスタート*/
    {
        isReducing = true;
    }

    private void OnTriggerStay(Collider other)
    {
        

        switch (other.gameObject.name)/*敵の名前によって受けるダメージが変わる*/
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
