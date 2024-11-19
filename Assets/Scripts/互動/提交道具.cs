using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class 道具數據
{
    public int 道具ID;
    public int 道具數量;

    public 道具數據(int id, int count)
    {
        道具ID = id;
        道具數量 = count;
    }
}

public class 提交道具 : MonoBehaviour
{
    [SerializeField]
    private 道具數據[] 提交道具數據;
    [SerializeField]
    private bool 是否消耗;
    private 提交畫面 提交畫面;
    public UnityEvent onSubmitConfirmed;
    private int[] 道具ID;
    private int[] 道具數量;

    void Start()
    {
        提交畫面 = GameObject.Find("UI控制/彈出視窗/提交").GetComponent<提交畫面>();
        道具ID = new int[提交道具數據.Length];
        道具數量 = new int[提交道具數據.Length];
    }
    public void 提交()
    {
        for (int i = 0; i < 提交道具數據.Length; i++)
        {
            道具ID[i] = 提交道具數據[i].道具ID;
            道具數量[i] = 提交道具數據[i].道具數量;
        }
        提交畫面.顯示提交道具(道具ID, 道具數量, this);
    }
    public void 確定提交()
    {
        if (onSubmitConfirmed != null)
        {
            onSubmitConfirmed.Invoke();
            this.GetComponent<互動>().清除按鈕();
            Destroy(this.GetComponent<互動>());
        }
        if (是否消耗)
        {
            提交畫面.消耗道具(道具ID, 道具數量);
        }
        提交畫面.隱藏提交道具();
    }
}
