using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class 道具分類
{
    public GameObject 分類UI;
    public Sprite 分類圖片;
    public string 分類名稱;

    public 道具分類(GameObject UI, Sprite 圖片, string 名稱)
    {
        分類UI = UI;
        分類圖片 = 圖片;
        分類名稱 = 名稱;
    }
}

public class 分類顯示 : MonoBehaviour
{
    public GameObject 預置按鈕;
    public 道具分類[] 道具分類;
    private GameObject[] 分類按鈕;
    public Text 分類名稱;

    void Awake()
    {
        分類按鈕 = new GameObject[道具分類.Length];
        for (int i = 0; i < 道具分類.Length; i++)
        {
            分類按鈕[i] = Instantiate(預置按鈕, transform);
            分類按鈕[i].name = 道具分類[i].分類名稱;
            if (分類按鈕[i].GetComponent<Image>() != null)
                分類按鈕[i].GetComponent<Image>().sprite = 道具分類[i].分類圖片;

            int currentIndex = i;
            分類按鈕[i].GetComponent<Button>().onClick.AddListener(delegate { 顯示分類(currentIndex); });
            foreach (Transform child in 分類按鈕[i].transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        開啟();
    }
    public void 開啟()
    {
        分類名稱.text = "/ " + 道具分類[0].分類名稱;
        道具分類[0].分類UI.SetActive(true);
        if (道具分類[0].分類UI.GetComponent<物品顯示>() != null)
        {
            道具分類[0].分類UI.GetComponent<物品顯示>().更新背包(0);
        }
        foreach (Transform child in 分類按鈕[0].transform)
        {
            child.gameObject.SetActive(true);
        }
    }
    public void 顯示分類(int index)
    {
        for (int i = 0; i < 道具分類.Length; i++)
        {
            道具分類[i].分類UI.SetActive(false);
            foreach (Transform child in 分類按鈕[i].transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        道具分類[index].分類UI.SetActive(true);
        分類名稱.text = "/ " + 道具分類[index].分類名稱;
        if (道具分類[index].分類UI.GetComponent<物品顯示>() != null)
        {
            道具分類[index].分類UI.GetComponent<物品顯示>().更新背包(index);
        }

        foreach (Transform child in 分類按鈕[index].transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
