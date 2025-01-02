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
    // [SerializeField]
    // private float 回復時間 = 2;
    [SerializeField]
    int 擊破所需傷害 = 50;
    int _累計傷害 = 0;
    [SerializeField] private UnityEvent 擊破;
    [SerializeField] private UnityEvent 回復;

    public void 受傷(int 傷害, bool 受擊效果)
    {
        _累計傷害 += 傷害;
        if (是否為致命部位)
        {
            怪物狀態.受傷((int)(傷害 * 傷害倍率 ), true);
        }
        else if (是否可被擊破)
        {
            if (_累計傷害 >= 擊破所需傷害)
            {
                擊破.Invoke();
                this.gameObject.GetComponent<Collider>().enabled = false;
                怪物狀態.受傷((int)(傷害 * 傷害倍率 ), true);
            }
            else
            {
                怪物狀態.受傷((int)(傷害), 受擊效果);
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
