using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 電腦畫面程式 : MonoBehaviour
{
    private GameObject 攝影機1;
    private 控制 取得控制程式;
    private GameObject situi;
    void Start()
    {
        取得控制程式=GameObject.Find("程式/控制").GetComponent<控制>();
        situi = GameObject.Find("UI控制/坐下/坐下UI");
    }
    public void 開啟(GameObject 攝影機)
    {
        if(攝影機!=null){
            攝影機1=攝影機;
            攝影機1.SetActive(true);
        }
        Invoke("顯示畫面", .5f);
        取得控制程式.CursorUnLock();
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
        this.GetComponent<UIScaleEffectWithClose>().CloseUI();
        if(攝影機1!=null)
        攝影機1.SetActive(false);
        取得控制程式.CursorLock();
    }

}
