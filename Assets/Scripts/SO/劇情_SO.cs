using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "劇情", menuName = "Data/劇情")]
public class 劇情_SO : ScriptableObject
{
    public 劇情s[] 劇情;
}
[System.Serializable]
public class 劇情s
{
    public string 名稱;
    [TextArea]
    public string 文字內容;
    public int 攝影機位置;
}

