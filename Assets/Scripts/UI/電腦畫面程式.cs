using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 電腦畫面程式 : MonoBehaviour
{
    private GameObject 攝影機1;
    public GameObject situi;
    void Start()
    {
    }
    public void 開啟(GameObject 攝影機)
    {
        if(攝影機!=null){
            攝影機1=攝影機;
            攝影機1.SetActive(true);
        }
        Invoke("顯示畫面", .5f);
    }
    private void 顯示畫面()
    {
        this.GetComponent<UIScaleEffectWithClose>().OpenUI();
    }
    public void 隱藏畫面()
    {
        if(坐下.isSitting)
        {
            situi.SetActive(true);
        }
        if(攝影機1!=null)
        攝影機1.SetActive(false);
    }

}
