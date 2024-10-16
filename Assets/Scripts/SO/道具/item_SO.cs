using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "item", menuName = "Data/item")]
public class item_SO : ScriptableObject
{
    public List<item> itemList;
}
[System.Serializable]
public class item
{
    public int itemID;
    public string itemName;
    public Sprite itemIcon;
    public int itemAmount;
    public string itemDescription;
    public bool CanUse = false;
}
