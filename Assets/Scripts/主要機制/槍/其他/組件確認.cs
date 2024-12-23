using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 組件確認 : MonoBehaviour
{
    public 組件類型 組件類型;

    public void 確認()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<冰凍組件>() != null)
            {
                組件類型.冰凍 = transform.GetChild(i).GetComponent<冰凍組件>();
                break;
            }else
            {
                組件類型.冰凍 = null;
            }
            if(transform.GetChild(i).GetComponent<火焰組件>() != null)
            {
                組件類型.火焰 = transform.GetChild(i).GetComponent<火焰組件>();
                break;
            }else
            {
                組件類型.火焰 = null;
            }
        }
    }
}
