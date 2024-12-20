using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 冰凍組件 : MonoBehaviour
{
    public 冰凍組件_SO 冰凍組件參數;
    public Transform GunMuzzle;
    private int _傷害;
    private int _射程;
    public bool 受擊效果 = false;
    public void 步槍(int 射程,int 傷害)
    {
        _傷害 =(int) (傷害*冰凍組件參數.傷害);
        _射程 = (int)(射程*冰凍組件參數.射程);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit,_射程))
        {
            if (hit.collider.tag == "Mosters")
            {
                hit.collider.GetComponent<怪物身體部位>().受傷(_傷害, 受擊效果);
                hit.collider.GetComponent<怪物身體部位>().冰凍(冰凍組件參數.冰凍效率);
            }
            if(hit.collider.tag == "Untagged")
            {
                Debug.Log("未命中");
            }
        }
        玩家狀態.能量 -= 冰凍組件參數.能量消耗;
    }
    public void 狙擊槍()
    {
        
    }
    public void 霰彈槍()
    {
        
    }
}
