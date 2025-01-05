using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "劇情", menuName = "Data/劇情")]
public class 劇情_SO : ScriptableObject
{
    public 劇情s[] 劇情;
    public bool 是否黑幕 = false;
}
[System.Serializable]
public class 選項s
{
    public string 選項文字;
    public string 選擇後內容名稱;
    [TextArea]
    public string 選擇後內容;
    public int 事件編號;
}
[System.Serializable]
public class 劇情s
{
    public string 名稱;
    public int 事件編號;
    [TextArea]
    public string 文字內容;
    public AudioClip 音效;
    public 選項s[] 選項;
    public int 攝影機位置;
    public float 停留時間;

}

