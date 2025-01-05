using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 獲取槍械 : MonoBehaviour
{
    public void 獲取槍械事件(int i)
    {
        GameObject.Find("程式/控制").GetComponent<控制>().取得武器(i);
    }
}
