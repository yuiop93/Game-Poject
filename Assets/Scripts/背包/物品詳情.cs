using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class 物品詳情 : MonoBehaviour
{
    [SerializeField]
    private Text 物品名稱;
    [SerializeField]
    private Text 物品描述;
    [SerializeField]
    private Image 物品圖片;
    public item_SO 物品SO;
    [SerializeField]
    private GameObject 使用按鈕;
    public GameObject 欄位;
    
    public void 物品內容(int 物品ID )
    {
        欄位.SetActive(true);
        for (int i = 0; i < 物品SO.物品.Count; i++)
        {
            if (物品SO.物品[i].物品ID == 物品ID)
            {
                物品名稱.text = 物品SO.物品[i].名稱;
                物品描述.text = 物品SO.物品[i].描述;
                if (物品SO.物品[i].圖片 != null)
                {
                    物品圖片.sprite = 物品SO.物品[i].圖片;
                }
                else
                {
                    物品圖片.sprite = null;
                }
                if (物品SO.物品[i].CanUse == true)
                {
                    使用按鈕.SetActive(true);
                }
                else
                {
                    使用按鈕.SetActive(false);
                }
                break;
            }
        }
    }
}
