using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class 撿起 : MonoBehaviour
{
    [SerializeField]
    private int count = 0;
    [SerializeField]
    private int 物品ID = 0;
    [SerializeField]
    [Range(0, 999)]
    private int 次數 = 1;
    public GameObject 文字;
    private item_SO 物品SO;
    [SerializeField]
    private Transform 位置;
    public void 獲取()
    {
        位置 = GameObject.Find("獲得").transform;
        物品SO = Resources.Load<item_SO>("ScriptableObjects/道具/背包");
        文字 = Resources.Load<GameObject>("Prefab/panel/獲取道具文字");
        for (int i = 0; i < 物品SO.物品.Count; i++)
        {
            if (物品SO.物品[i].物品ID == 物品ID)
            {
                物品SO.物品[i].數量 += count;
                次數--;
                GameObject 字 = Instantiate(文字);
                字.transform.SetParent(位置, false);
                字.GetComponent<Text>().text = 物品SO.物品[i].名稱 + "X" + count;
                break;
            }
        }
        if (次數 == 0)
        {
            if (this.GetComponent<互動>() != null)
            {
                this.GetComponent<互動>().清除按鈕();
            }
            Destroy(gameObject);
        }


    }
}
