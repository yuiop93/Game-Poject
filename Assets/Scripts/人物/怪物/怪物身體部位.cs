using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 怪物身體部位 : MonoBehaviour
{
    [HideInInspector]
    public 怪物狀態 怪物狀態;

    [SerializeField]
    private float 傷害倍率 = 1;
    public void 受傷(float 傷害 ,bool 受擊效果)
    {
        怪物狀態.受傷((int)(傷害 * 傷害倍率), 受擊效果);
    }
    public void 冰凍(int 冰凍點數,int 燃燒傷害)
    {
        怪物狀態.冰凍值(冰凍點數,燃燒傷害);
    }
    
}
