using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class 電腦畫面 : MonoBehaviour
{
    [SerializeField]
    private Text 時間顯示文字;


    void OnDisable()
    {
        StopAllCoroutines();
    }
    void OnEnable()
    {
        StartCoroutine(顯示混亂時間());
    }
    private IEnumerator 顯示混亂時間()
    {
        while (true)
        {
            string 混亂時間 = 生成打亂的時間();
            時間顯示文字.text = 混亂時間;
            yield return new WaitForSeconds(1); // 每秒更新一次
        }
    }

    private string 生成打亂的時間()
    {
        // 定義固定部分的文字與符號
        string[] 前綴詞 = { "現","在","時","間","上","下","午","當","前","刻" };
        string 符號集 = "!@#$%^&*()-_=+~<>?[]{}|";
        string 混亂前綴 = "";
        int 隨機數 = Random.Range(5, 8);
        for (int i = 0; i < 隨機數; i++)
        {
            if (Random.Range(0, 2) != 0)
            {
                混亂前綴 += 前綴詞[Random.Range(0, 前綴詞.Length)];
            }
            else
            {
                混亂前綴 += 符號集[Random.Range(0, 符號集.Length)];
            }

            // 加一些隨機空格或符號
            混亂前綴 += Random.Range(0, 2) == 0 ? " " : 符號集[Random.Range(0, 符號集.Length)];
        }

        // 時間相關字符集
        string 時間符號集 = "0123456789:!@#$%^&*?";
        string 打亂時間 = "";

        // 隨機生成類似時間的片段
        for (int i = 0; i < 8; i++) // 限制字符數接近時間格式
        {
            if (i == 2 || i == 5) // 在時間分隔符的位置插入冒號或隨機符號
            {
                打亂時間 += Random.Range(0, 2) == 0 ? ":" : 時間符號集[Random.Range(10, 時間符號集.Length)];
            }
            else // 插入數字或混亂符號
            {
                打亂時間 += 時間符號集[Random.Range(0, 時間符號集.Length)];
            }
        }

        // 最後整合前綴與時間部分
        return 混亂前綴 + 打亂時間;
    }

}
