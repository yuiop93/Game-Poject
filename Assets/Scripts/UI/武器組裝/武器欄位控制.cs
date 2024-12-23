using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 武器欄位控制 : MonoBehaviour
{
    [SerializeField] private GameObject[] 武器鏡頭;
    [SerializeField] private List<武器選擇> 武器列表;
    public Transform 組件背包;
    private int _currentWeaponIndex;
    public Button 裝備按鈕;
    public GameObject 資訊欄;
    public GameObject[] 玩家裝備位置;
    public GameObject 當前組件;
    public Sprite 當前組件Image;
    public 組件狀態 當前組件狀態;
    [SerializeField] private Image 組件圖片;

    public void 介面開關(bool 開關)
    {
        if (開關)
        {
            Open();
        }
        else
        {
            Close();
        }
    }

    public void 切換到指定索引的武器(int index)
    {
        if (index < 0 || index >= 武器列表.Count)
        {
            Debug.LogWarning("索引超出範圍！" + index);
            return;
        }
        foreach (var item in 武器列表)
        {
            item.槍械.SetActive(false);
        }
        武器列表[index].槍械.SetActive(true);
        _currentWeaponIndex = index;
        資訊欄.SetActive(false);
    }
    public void 資訊欄更新()
    {
        if (當前組件 == null)
        {
            資訊欄.SetActive(false);
            return;
        }
        資訊欄.SetActive(true);
        組件圖片.sprite = 當前組件Image;
        判斷組件是否裝備(_currentWeaponIndex);
    }
    public void 判斷組件是否裝備(int index)
    {
        if (當前組件狀態.裝備位置 == null && index != 0)
        {
            裝備按鈕.interactable = true;
            裝備按鈕.GetComponentInChildren<Text>().text = "裝備";
        }
        else if (當前組件狀態.裝備位置 == 武器列表[index].槍械)
        {
            裝備按鈕.interactable = true;
            裝備按鈕.GetComponentInChildren<Text>().text = "拆卸";
        }
        else
        {
            裝備按鈕.interactable = false;
            裝備按鈕.GetComponentInChildren<Text>().text = "已裝備";
        }
    }
    public void 裝備()
    {
        if (當前組件狀態.裝備位置 == null)
        {
            裝備組件();
        }
        else
        {
            拆卸組件();
        }
    }

    void 裝備組件()
    {
        當前組件狀態.裝備位置 = 武器列表[_currentWeaponIndex].槍械;
        武器列表[_currentWeaponIndex].組件.Add(Instantiate(當前組件, 玩家裝備位置[_currentWeaponIndex].transform));
        武器列表[_currentWeaponIndex].組件.Add(Instantiate(當前組件, 武器列表[_currentWeaponIndex].槍械.transform));
        判斷組件是否裝備(_currentWeaponIndex);
    }
    void 拆卸組件()
    {
        foreach (var 組件 in 武器列表[_currentWeaponIndex].組件)
        {
            if (組件 != null) // 確保組件存在
            {
                Destroy(組件); // 銷毀組件
            }
        }
        當前組件狀態.裝備位置 = null;
        判斷組件是否裝備(_currentWeaponIndex);
    }
    void Open()
    {
        foreach (var item in 武器鏡頭)
        {
            item.SetActive(true);
        }
        for (int i = 0; i < 武器列表.Count; i++)
        {
            武器列表[i].按鈕.interactable = GameObject.Find("程式/控制").GetComponent<控制>().武器取得狀態[i];
        }
        當前組件 = null;
        當前組件Image = null;
        武器列表[0].按鈕.Select();
        切換到指定索引的武器(0);
    }
    void Close()
    {
        foreach (var item in 武器鏡頭)
        {
            item.SetActive(false);
        }
    }
}
