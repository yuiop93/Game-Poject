using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class Item
{
    public string itemName;
    public GameObject itemPrefab; // 3D 道具的模型
    public Sprite itemIcon; // 動態生成的圖片
}
[System.Serializable]
public class 組件狀態
{
    public bool 是否裝備;
    public GameObject 裝備位置;
}
[System.Serializable]
public class 冰凍組件參數
{
    public float 傷害 = 1;
    public float 射程 = 1;
    public float 冰凍效率 = 20;
    public int 能量消耗 = 10;
}

[System.Serializable]
public class WeaponComponent
{
    public enum HoldStyle
    {
        Style1 = 1,
        Style2 = 2,
        Style3 = 3

    }
    public HoldStyle holdStyle = HoldStyle.Style1;
    public GameObject[] gunObject;
    public MonoBehaviour Script;
}

[System.Serializable]
public class 武器選擇
{
    public GameObject 槍械;
    public List<GameObject> 組件;
    public Button 按鈕;
    public 槍械_SO 槍械描述;
}

[System.Serializable]
public class 組件類型
{
    public 冰凍組件 冰凍;
    public 火焰組件 火焰;
}



