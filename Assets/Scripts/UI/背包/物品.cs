using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class 物品 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int 物品ID;
    [SerializeField] private item_SO itemSO;
    [SerializeField] private Text 物品數量;
    [SerializeField] private Text 物品名稱;
    [SerializeField] private Image 物品圖片;
    [SerializeField] private Text 需要數量;
    [SerializeField] private GameObject 灰色背景;
    [HideInInspector] public bool 是否滿足 = false;
    public GameObject 顯示物品名稱;

    public void OnPointerEnter(PointerEventData eventData)
    {
        顯示物品名稱?.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        顯示物品名稱?.SetActive(false);
    }

    public void 更新數量()
    {
        var item = itemSO.物品.FirstOrDefault(i => i.物品ID == 物品ID);
        if (item != null)
        {
            物品圖片.sprite = item.圖片;
            物品數量.text = item.數量.ToString();
            物品名稱.text = item.名稱;
        }
    }

    public void 判斷提交(int 數量)
    {
        需要數量.text = "/" + 數量.ToString();
        var item = itemSO.物品.FirstOrDefault(i => i.物品ID == 物品ID);

        if (item != null)
        {
            if (item.數量 >= 數量)
            {
                灰色背景?.SetActive(false);
                物品數量.color = Color.white;
                是否滿足 = true;
            }
            else
            {
                灰色背景?.SetActive(true);
                物品數量.color = Color.red;
                是否滿足 = false;
            }
        }
    }

    public void 內容()
    {
        GameObject 資訊 = GameObject.Find("UI控制/背包/背景/物品資訊");
        資訊?.GetComponent<物品詳情>().物品內容(物品ID);
    }
}
