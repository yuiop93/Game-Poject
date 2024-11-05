using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class 消耗品 : MonoBehaviour
{
    [SerializeField] private item_SO item_SO;
    [SerializeField] private Text 數量UI;
    [SerializeField] private Image image;
    [SerializeField] private GameObject 耗盡UI;
    public int 物品ID;

    // 冷卻時間變數
    [SerializeField] private float 冷卻時間 = 1.0f; // 冷卻時間（秒）
    private bool 是否冷卻中 = false; // 紀錄冷卻狀態
    [SerializeField] private Image 冷卻進度條; // 冷卻進度條 UI

    void Start()
    {

        道具顯示(0);
        冷卻進度條.fillAmount = 0; // 初始化進度條

        // 订阅事件
        item_SO.OnItemCountChanged += 更新數量UI;
    }

    private void OnDestroy()
    {
        // 取消订阅事件以避免内存泄漏
        item_SO.OnItemCountChanged -= 更新數量UI;
    }

    public void 道具顯示(int ID)
    {
        物品ID = ID;
        耗盡UI.SetActive(false);

        // 找到對應的物品
        var item = item_SO.物品.FirstOrDefault(i => i.物品ID == ID);
        if (item != null)
        {
            image.sprite = item.圖片;
            數量UI.text = item.數量.ToString();
            if (item.數量 <= 0)
            {
                道具耗盡();
            }
        }
        else
        {
            道具耗盡();
            image.sprite = null;
            數量UI.text = "未使用";
        }
    }

    public void 使用道具()
    {
        // 檢查是否在冷卻狀態
        if (是否冷卻中)
        {
            return; // 如果還在冷卻，則退出
        }

        // 查找指定ID的物品
        var item = item_SO.物品.FirstOrDefault(i => i.物品ID == 物品ID);
        if (item != null)
        {
            if (item.數量 > 0)
            {
                item.數量--; // 減少物品數量

                // 恢復玩家狀態
                玩家狀態.血量 += item.消耗品.恢復血量;
                玩家狀態.體力 += item.消耗品.恢復體力;
                玩家狀態.能量 += item.消耗品.恢復能量;

                // 更新UI顯示
                數量UI.text = item.數量.ToString(); // 更新數量UI
                if (item.數量 <= 0)
                {
                    道具耗盡();
                }

                // 開始冷卻協程
                StartCoroutine(冷卻());
            }
            else
            {
                數量UI.text = "未使用"; // 如果數量為0，顯示未使用
            }
        }
    }

    private void 更新數量UI(int itemID)
    {
        if (物品ID == itemID) // 仅更新当前物品的数量
        {
            var item = item_SO.物品.FirstOrDefault(i => i.物品ID == itemID);
            if (item != null)
            {
                數量UI.text = item.數量.ToString(); // 更新数量UI
                if (item.數量 <= 0)
                {
                    道具耗盡();
                }
            }
        }
    }

    private void 道具耗盡()
    {
        耗盡UI.SetActive(true);
    }

    private IEnumerator 冷卻()
    {
        是否冷卻中 = true; // 設置為冷卻狀態
        冷卻進度條.fillAmount = 1; // 初始化進度條為滿

        float elapsedTime = 0f; // 計算已過時間
        while (elapsedTime < 冷卻時間)
        {
            elapsedTime += Time.deltaTime; // 增加已過時間
            冷卻進度條.fillAmount = 1 - (elapsedTime / 冷卻時間); // 更新進度條為相反效果
            yield return null; // 等待下一幀
        }

        冷卻進度條.fillAmount = 0; // 確保進度條變空
        是否冷卻中 = false; // 結束冷卻狀態
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            使用道具();
        }
    }
}
