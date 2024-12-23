using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class 組件UI : MonoBehaviour
{
    public Image _組件圖片;
    public Text 組件名稱;
    public 武器欄位控制 武器欄位控制;
    public Item _item;
    public 組件狀態 _狀態;
    public void 設定組件(Item item)
    {
        _item = item;
        組件名稱.text = item.itemName;
        _組件圖片.sprite = GameObject.Find("程式/控制").GetComponent<ItemIconGenerator>().GenerateIcon(item);
        this.GetComponent<Button>().onClick.AddListener(組件);
    }
    public void 組件()
    {
        武器欄位控制.當前組件狀態 = _狀態;
        武器欄位控制.當前組件 = _item.itemPrefab;
        武器欄位控制.當前組件Image = _組件圖片.sprite;
        武器欄位控制.資訊欄更新();
    }
}
