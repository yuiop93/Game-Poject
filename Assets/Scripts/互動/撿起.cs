using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 撿起 : MonoBehaviour
{
    [SerializeField]
    private int count = 0;
    [SerializeField]
    private int 物品ID = 0;
    [SerializeField]
    [Range(0, 999)]
    private int 次數 = 1;
    private item_SO 物品SO;
    public void 獲取()
    {
        物品SO = Resources.Load<item_SO>("ScriptableObjects/道具/背包");
        for (int i = 0; i < 物品SO.物品.Count; i++)
        {
            if (物品SO.物品[i].物品ID == 物品ID){
                物品SO.物品[i].數量 += count;
                次數--;
                break;
            }
        }
        if (次數==0)
        Destroy(gameObject);
    }
}
