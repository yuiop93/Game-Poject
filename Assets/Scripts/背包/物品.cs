using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 物品 : MonoBehaviour
{
    public int 物品ID;
    [SerializeField]
    private item_SO itemSO;
    [SerializeField]
    private Text 物品數量;
    private Image 圖片;
    [SerializeField]
    private Text 需要數量;
    [SerializeField]
    private GameObject 灰色背景;
    public void 更新數量()
    {
        for (int i = 0; i < itemSO.物品.Count; i++)
        {
            if (itemSO.物品[i].物品ID == 物品ID)
            {
                // if (itemSO.物品[i].數量 == 0)
                // {
                //     // Destroy(gameObject);
                // }
                if (itemSO.物品[i].圖片 != null)
                {
                    圖片.sprite = itemSO.物品[i].圖片;
                }
                物品數量.text = itemSO.物品[i].數量.ToString();
                break;
            }
        }
    }
    public void 判斷提交(int 數量)
    {
        需要數量.text = "/" + 數量.ToString();
        for (int i = 0; i < itemSO.物品.Count; i++)
        {
            if (itemSO.物品[i].物品ID == 物品ID)
            {
                if (itemSO.物品[i].數量 >= 數量)
                {
                    灰色背景.SetActive(false);
                    物品數量.color = Color.white;
                }
                else
                {
                    灰色背景.SetActive(true);
                    物品數量.color = Color.red;
                }
            }
        }
    }

    public void 內容()
    {
        GameObject.Find("物品資訊").GetComponent<物品詳情>().物品內容(物品ID);
    }
}
