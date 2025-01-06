using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class 教學系統 : MonoBehaviour
{
    public 教學_SO[] 教學清單;
    private 教學_SO 教學資料;
    public Text 教學名稱UI;
    public Image 教學圖片UI;
    public Text 教學內容UI;
    public GameObject 教學UI;
    public GameObject 開啟教學按鈕預置物;
    public Transform 按鈕生成位置;
    public Button 下一頁按鈕;
    public Button 上一頁按鈕;

    private int index = 0;
    public void 生成教學按鈕(int 教學編號)
    {
        教學資料 = 教學清單[教學編號];
        GameObject gameObject = Instantiate(開啟教學按鈕預置物, 按鈕生成位置);
        gameObject.GetComponentInChildren<Text>().text = 教學資料.教學名稱;
        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(() => 開始顯示教學(教學編號));
        開始顯示教學(教學編號);
    }
    public void 開始顯示教學(int 教學編號)
    {
        教學UI.SetActive(true);
        index = 0;
        教學名稱UI.text = 教學清單[教學編號].教學名稱;
        教學資料 = 教學清單[教學編號];
        if (教學資料.教學列表[index].教學圖片 != null)
        {
            教學圖片UI.sprite = 教學資料.教學列表[index].教學圖片;
        }
        教學內容UI.text = 教學資料.教學列表[index].教學內容;
        判斷頁碼();
    }
    public void 顯示下一頁()
    {
        if (index < 教學資料.教學列表.Count - 1)
        {
            index++;
            if (教學資料.教學列表[index].教學圖片 != null)
            {
                教學圖片UI.sprite = 教學資料.教學列表[index].教學圖片;
            }
            教學內容UI.text = 教學資料.教學列表[index].教學內容;
        }
        判斷頁碼();
    }

    public void 顯示上一頁()
    {
        if (index > 0)
        {
            index--;
            if (教學資料.教學列表[index].教學圖片 != null)
            {
                教學圖片UI.sprite = 教學資料.教學列表[index].教學圖片;
            }
            教學內容UI.text = 教學資料.教學列表[index].教學內容;
        }
        判斷頁碼();
    }
    void 判斷頁碼()
    {
        if (index == 0)
        {
            上一頁按鈕.interactable = false;
        }
        else
        {
            上一頁按鈕.interactable = true;
        }
        if (index == 教學資料.教學列表.Count - 1)
        {
            下一頁按鈕.interactable = false;
        }
        else
        {
            下一頁按鈕.interactable = true;
        }
    }
}
