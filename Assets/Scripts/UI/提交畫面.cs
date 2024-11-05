using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 提交畫面 : MonoBehaviour
{
    public item_SO item; // 存储所有物品
    [SerializeField]
    private GameObject 提交道具UI; // 提交界面
    [SerializeField]
    private GameObject 欄位; // 显示道具的父物体
    [SerializeField]
    private GameObject 道具預置物; // 道具预设物
    private 控制 f1; // 控制脚本
    public Button 提交按鈕; // 提交按钮
    private bool[] 是否滿足; // 用于检查是否满足提交条件
    private 提交道具 提交道具; // 提交道具脚本的引用

    void Start()
    {
        提交道具UI.SetActive(false);
        f1 = GameObject.Find("程式/控制").GetComponent<控制>();
    }

    // 消耗道具方法
    public void 消耗道具(int[] ID, int[] 數量)
{
    // 确保ID和数量数组长度相同
    if (ID.Length != 數量.Length)
    {
        Debug.LogError("ID和数量数组长度不匹配");
        return;
    }

    for (int i = 0; i < ID.Length; i++) // 使用ID.Length而不是item.物品.Count
    {
        for (int j = 0; j < item.物品.Count; j++) // 遍历所有物品
        {
            if (item.物品[j].物品ID == ID[i])
            {
                int newCount = item.物品[j].數量 - 數量[i]; // 计算新的数量
                item.ChangeItemCount(ID[i], newCount); // 更新数量并确保不为负
                break; // 找到匹配后退出内层循环
            }
        }
    }
}
    // 显示提交道具界面
    public void 顯示提交道具(int[] ID, int[] 數量, 提交道具 f2)
    {
        提交道具 = f2;
        提交按鈕.onClick.RemoveAllListeners();
        提交按鈕.onClick.AddListener(提交道具.確定提交);
        f1.CursorUnLock();
        提交道具UI.SetActive(true);
        是否滿足 = new bool[ID.Length];

        // 清空之前的道具显示
        foreach (Transform child in 欄位.transform)
        {
            Destroy(child.gameObject);
        }

        // 实例化新的道具显示
        for (int i = 0; i < ID.Length; i++)
        {
            GameObject itemGO = Instantiate(道具預置物, 欄位.transform);
            物品 itemComponent = itemGO.GetComponent<物品>(); // 获取物品组件
            itemComponent.物品ID = ID[i];
            itemComponent.更新數量();
            itemComponent.判斷提交(數量[i]);
            是否滿足[i] = itemComponent.是否滿足;
        }

        // 根据是否满足条件更新按钮状态
        提交按鈕.interactable = 全部滿足(是否滿足);
    }

    // 检查是否所有条件都满足
    private bool 全部滿足(bool[] 是否滿足)
    {
        foreach (bool 滿足 in 是否滿足)
        {
            if (!滿足) return false;
        }
        return true;
    }

    // 隐藏提交道具界面
    public void 隱藏提交道具()
    {
        提交道具UI.SetActive(false);
        提交道具 = null;
        f1.CursorLock();
    }
}
