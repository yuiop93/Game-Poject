using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 物品 : MonoBehaviour
{
    public int 物品ID;
    [HideInInspector]
    public Text 數量;
    [HideInInspector]
    public Image 圖片;
    public void 內容()
    {
       GameObject.Find("物品資訊").GetComponent<物品詳情>().物品內容(物品ID);
    }
}
