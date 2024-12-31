using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class 怪物身體部位 : MonoBehaviour
{
    [HideInInspector]
    public 怪物狀態 怪物狀態;
    [SerializeField]
    private bool 是否為致命部位;
    [SerializeField]
    public bool 是否可被擊破=false;

    [SerializeField]
    private float 傷害倍率 = 1;
    [SerializeField]
    private float 回復時間 = 1;
    [SerializeField] private UnityEvent 被擊破;
    [SerializeField] private UnityEvent 回復;

    public void 受傷(float 傷害, bool 受擊效果)
    {
        if (是否為致命部位)
        {
            怪物狀態.受傷((int)(傷害 * 傷害倍率 ), true);
            if (是否可被擊破)
            {
                this.gameObject.GetComponent<Collider>().enabled = false;
                被擊破.Invoke();
                Invoke("擊破回復", 回復時間);
            }
        }
        else
        {
            怪物狀態.受傷((int)(傷害 * 傷害倍率), 受擊效果);
        }
    }
    void 擊破回復()
    {
        回復.Invoke();
        this.gameObject.GetComponent<Collider>().enabled = true;
    }
    public void 冰凍(int 冰凍點數,int 引爆傷害)
    {
        怪物狀態.冰凍值(冰凍點數,引爆傷害);
    }

}
