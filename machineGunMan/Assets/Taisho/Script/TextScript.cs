using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextScript : MonoBehaviour
{
    public Text appearance;/*遠距離出現のテキスト*/
    public float speed;/*透明度が変わるスピードを管理する*/
    float red, green, blue, alpha;/*テキストの色、不透明度を管理*/
    public bool isFadeOut = false;/*フェードアウトの処理の開始、完了を管理するフラグ*/
    public bool isFadeIn = false;/*フェードインの処理の開始、完了を管理するフラグ*/

    public Text enemyCountText;/*残りの敵の数をカウントするためのテキストをここにぶちこむ*/
    public int remainingEnemies;/*残りの敵の数*/

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
            
            Debug.Log("むげーんだーいなーゆーめのーあとのー");
        }


        enemyCountText.text = "ずんどこわっしょい侍の残り人数" + remainingEnemies.ToString();
        Defeat();
    }

    void StartFadeIn()
    {
        alpha -= speed;/*不透明度を徐々に下げる*/
        SetAlpha();/*変更した不透明度をテキストに反映する*/
        if(alpha <= 0)/*完全に透明になったら処理を抜ける*/
        {
            isFadeIn = false;
        }
    }

    void StartFadeOut()
    {
        alpha += speed;/*不透明度を徐々に上げる*/
        SetAlpha();/*変更した不透明度をテキストに反映する*/
        if(alpha >= 1)/*完全に不透明になったら処理を抜ける*/
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
