using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "槍械", menuName = "Data/槍械")]
public class 槍械_SO : ScriptableObject
{
    public string 槍械名稱;
    [TextArea]
    public string 槍械描述;
    [Header("槍械參數")]
    [Range(0, 50)]
    public int 傷害;
    [Range(1, 10)]
    public int 射速;
    [Range(10, 30)]
    public int 射程;
    [Range(0, 100)]
    public int 能量消耗;
}