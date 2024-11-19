using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class 道具分類
{
    public GameObject 分類UI;
    public Sprite 分類圖片;
    public string 分類名稱;

    public 道具分類(GameObject UI, Sprite 圖片, string 名稱)
    {
        分類UI = UI;
        分類圖片 = 圖片;
        分類名稱 = 名稱;
    }
}

public class 分類顯示 : MonoBehaviour
{
    [SerializeField]
    private GameObject 預置按鈕;
    [SerializeField]
    private List<道具分類> 道具分類;
    private List<GameObject> 分類按鈕 = new List<GameObject>();
    [SerializeField]
    private Text 分類名稱;
    [SerializeField]
    private GameObject 物品詳情UI;

    void Awake()
    {
        初始化按鈕();
        開啟(); // 預設開啟第一個分類
    }

    private void 初始化按鈕()
    {
        for (int i = 0; i < 道具分類.Count; i++)
        {
            GameObject newButton = Instantiate(預置按鈕, transform);
            newButton.name = 道具分類[i].分類名稱;
            Image buttonImage = newButton.GetComponent<Image>();
            if (buttonImage != null)
            {
                buttonImage.sprite = 道具分類[i].分類圖片;
            }
            int currentIndex = i;
            newButton.GetComponent<Button>().onClick.AddListener(() => 顯示分類(currentIndex));
            ToggleButtonChildren(newButton, false);
            分類按鈕.Add(newButton);
        }
    }

    public void 開啟()
    {
        if (道具分類.Count == 0) return;
        顯示分類(0);
    }

    public void 顯示分類(int index)
    {
        物品詳情UI.SetActive(false);
        for (int i = 0; i < 道具分類.Count; i++)
        {
            bool isSelected = i == index;
            道具分類[i].分類UI.SetActive(isSelected);
            ToggleButtonChildren(分類按鈕[i], isSelected);
        }

        分類名稱.text = "/ " + 道具分類[index].分類名稱;
        
        var 物品顯示 = 道具分類[index].分類UI.GetComponent<物品顯示>();
        if (物品顯示 != null)
        {
            物品顯示.更新背包(index);
        }
    }

    private void ToggleButtonChildren(GameObject button, bool isActive)
    {
        foreach (Transform child in button.transform)
        {
            child.gameObject.SetActive(isActive);
        }
    }
}
