using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 物品 : MonoBehaviour
{
    public int 物品ID;
    public Text 名稱;
    public Text 數量;
    public Image 圖片;
    private bool CanUse;
    public void 使用()
    {
        if (CanUse)
        {
            Debug.Log("使用" + 名稱.text);
        }
    }
    public void 內容()
    {
        GameObject.Find("物品詳情").GetComponent<物品詳情>().物品內容(物品ID, CanUse);
    }
}
