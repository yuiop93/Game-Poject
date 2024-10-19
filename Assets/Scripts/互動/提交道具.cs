using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 提交道具 : MonoBehaviour
{
    [SerializeField]
    private int[] 道具ID;
    [SerializeField]
    private int[] 道具數量;
    [SerializeField]
    private bool 是否消耗;
    
    private 提交畫面 提交畫面;

    void Start()
    {
        提交畫面 = GameObject.Find("提交").GetComponent<提交畫面>();
    }
    public void 提交()
    {
        提交畫面.顯示提交道具(道具ID, 道具數量, 是否消耗);
    }
}
