using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class 教學
{
    public Sprite 教學圖片;
    [TextArea]
    public string 教學內容;
}

[CreateAssetMenu(fileName = "新教學", menuName = "教學/教學內容")]
public class 教學_SO : ScriptableObject
{
    public string 教學名稱;
    public List<教學> 教學列表 = new List<教學>();
}
