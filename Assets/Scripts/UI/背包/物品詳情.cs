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
    [SerializeField]
    private Button 使用按鈕;
    [SerializeField]
    private GameObject 欄位;

    public item_SO 物品SO;
    private int 物品ID;

    void Start()
    {
        欄位.SetActive(false);
    }

    public void 物品內容(int 物品ID)
    {
        this.物品ID = 物品ID;
        欄位.SetActive(true);
        foreach (var item in 物品SO.物品)
        {
            if (item.物品ID == 物品ID)
            {
                物品名稱.text = item.名稱;
                物品描述.text = item.描述;
                物品圖片.sprite = item.圖片 != null ? item.圖片 : null;

                使用按鈕.gameObject.SetActive(item.CanUse);

                if (item.CanUse)
                {
                    Text buttonText = 使用按鈕.GetComponentInChildren<Text>();
                    if (buttonText != null)
                    {
                        if (物品ID / 1000 == 1)
                        {
                            buttonText.text = "使用";
                            使用按鈕.onClick.RemoveAllListeners();
                            使用按鈕.onClick.AddListener(() => 使用功能());
                        }
                        else if (物品ID / 1000 == 2)
                        {
                            if (物品ID % 2000 < 100)
                            {
                                buttonText.text = "裝備";
                                使用按鈕.onClick.RemoveAllListeners();
                                使用按鈕.onClick.AddListener(() => 裝備功能());
                            }
                            else
                            {
                                buttonText.text = "使用";
                                使用按鈕.onClick.RemoveAllListeners();
                                使用按鈕.onClick.AddListener(() => 使用功能());
                            }
                        }
                    }
                }
                break;
            }
        }
    }

    private void 使用功能()
    {
        Debug.Log("使用道具");
        // 添加實際的使用道具邏輯
    }

    private void 裝備功能()
    {
        消耗品.道具顯示(物品ID);
    }
    [SerializeField] private 消耗品 消耗品;
}
