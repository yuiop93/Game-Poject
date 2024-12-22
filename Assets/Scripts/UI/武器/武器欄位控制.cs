using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class 武器選擇
{
    public GameObject 武器;
    public bool 是否解鎖;
    public Button 按鈕;
}

public class 武器欄位控制 : MonoBehaviour
{
    [SerializeField] private GameObject[] 武器鏡頭;
    [SerializeField] private List<武器選擇> 武器列表;
    [SerializeField] private GameObject[] 組件;
    private int _currentWeaponIndex;

    public void Open()
    {
        foreach (var item in 武器鏡頭)
        {
            item.SetActive(true);
        }
        foreach (var item in 武器列表)
        {
            item.按鈕.interactable = item.是否解鎖;
        }
        武器列表[0].按鈕.Select();
        切換到指定索引的武器(0);
        
    }
    public void Close()
    {
        foreach (var item in 武器鏡頭)
        {
            item.SetActive(false);
        }
    }
    public void 切換到指定索引的武器(int index)
    {
        if (index < 0 || index >= 武器列表.Count)
        {
            Debug.LogWarning("索引超出範圍！"+index);
            return;
        }
        foreach (var item in 武器列表)
        {
            item.武器.SetActive(false);
        }
        武器列表[index].武器.SetActive(true);
    }
}
