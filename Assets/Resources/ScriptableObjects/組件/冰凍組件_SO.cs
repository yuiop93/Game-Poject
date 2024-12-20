using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "冰凍組件", menuName = "Data/冰凍組件參數")]
public class 冰凍組件_SO : ScriptableObject
{
    [Range(0.1f, 1f)]
    public float 傷害 = 0.1f;
    [Range(0.1f,2f)]
    public float 射程 = 0.3f;
    public int 冰凍效率 = 10;
    public int 能量消耗=2;

}
