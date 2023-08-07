using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyScript : MonoBehaviour
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

        HPSlider = transform.Find("EnemyCanvas/HPSlider").GetComponent<Slider>();/*Canvasの子供であるHPSliderのコンポーネントをゲットだぜ！*/
        bulkHPSlider = transform.Find("EnemyCanvas/BulkHPSlider").GetComponent<Slider>();/*説明は上とおんなじだぜ！*/

        HP = MaxHP;/*開幕で体力をMAXにしときましょうねぇ*/
        finishHP = MaxHP;/*説明は上とおんなじだぜ！*/

        HPSlider.value = 1;/*HPの表示を体力MAX状態にしておくぜ！*/
        bulkHPSlider.value = 1;/*説明は上とおんなじだぜ！*/
    }

    private void Update()
    {
        if (!isReducing)/*ダメージが無ければ何もしないにょ*/
        {
            return;
        }

        if(countTIme >= nextCountTime)/*次に減らす時間が来たら*/
        {
            int tempDamage;

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
           
            if (HP <= 0)/*HPが0以下になったら敵を三秒後に削除するにょ*/
            {
                Instantiate(Hit[2], gameObject.transform.position, Quaternion.identity);
                e_text.count ++ ;
                Destroy(gameObject/*, 0.0f*/);
                
            }
        }

        countTIme += Time.deltaTime;
    }

    public void TakeEnemyDamage(int damage)/*ダメージ値を追加するメソッドにょ*/
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


        if (other.gameObject.tag == "Approach")/*プレイヤーの周辺に近づいたら*/
        {
            source.PlayOneShot(warningSound);/*警告音が鳴る*/
        }
    }
}
