using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 撿起 : MonoBehaviour
{
    [SerializeField]
    [Range(1, 999)]
    private int count = 0; // 物品数量
    [SerializeField]
    private int 物品ID = 0; // 物品ID
    [SerializeField]
    [Range(1, 999)]
    private int 次數 = 1; // 剩余拾取次数
    private item_SO 物品SO; // 物品脚本able对象
    private Transform 位置; // 文本位置

    // 获取物品
    public void 獲取()
    {
        位置 = GameObject.Find("UI控制/提示欄位/獲得").transform; // 获取显示物品的父物体
        物品SO = Resources.Load<item_SO>("ScriptableObjects/道具/背包"); // 加载物品数据
        GameObject 文字Prefab = Resources.Load<GameObject>("Prefab/UI/獲取道具文字"); // 加载文字Prefab

        if (物品SO == null || 文字Prefab == null)
        {
            Debug.LogError("物品数据或文字Prefab未能加载！");
            return;
        }

        for (int i = 0; i < 物品SO.物品.Count; i++)
        {
            if (物品SO.物品[i].物品ID == 物品ID)
            {
                // 使用 ChangeItemCount 方法更新物品数量
                int newCount = 物品SO.物品[i].數量 + count;
                物品SO.ChangeItemCount(物品ID, newCount);
                次數--; // 减少剩余拾取次数

                // 实例化文字显示
                GameObject 字 = Instantiate(文字Prefab);
                字.transform.SetParent(位置, false);
                字.GetComponent<Text>().text = $"{物品SO.物品[i].名稱} X {count}";

                // 如果次数为0，则销毁物体
                if (次數 <= 0)
                {
                    if (this.GetComponent<互動>() != null)
                    {
                        this.GetComponent<互動>().清除按鈕(); // 清除互动按钮
                    }
                    Destroy(gameObject); // 销毁当前物体
                }
                break; // 找到物品后退出循环
            }
        }
    }

}
