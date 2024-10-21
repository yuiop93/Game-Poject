using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 打開 : MonoBehaviour
{
    [SerializeField]
    private Statetype 狀態;
    private enum Statetype
    {
        旋轉,
        水平
    }
    [Range(-180, 180)]
    [SerializeField]
    private int 開啟範圍;
    [SerializeField]
    [Range(1, 10)]
    private int 旋轉速度 = 3;
    [SerializeField]
    private GameObject[] 物品;
    private bool 是否開啟;
    private Quaternion 初始旋轉;
    private bool 正在旋轉 = false;
    private void Start()
    {
        初始旋轉 = transform.rotation;
        是否開啟 = false;
        foreach (GameObject item in 物品)
        {
            if (item != null)
            {
                item.GetComponent<Collider>().enabled = false;
            }
        }
    }

    private System.Collections.IEnumerator 開門()
    {
        正在旋轉 = true;
        Quaternion 目標旋轉 = 初始旋轉 * Quaternion.Euler(0, 0, 開啟範圍);
        this.GetComponent<Collider>().enabled = false;
        foreach (GameObject item in 物品)
        {
            if (item != null)
            {
                item.GetComponent<Collider>().enabled = true;
            }
        }
        while (Quaternion.Angle(transform.rotation, 目標旋轉) > 0.1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, 目標旋轉, Time.deltaTime * 旋轉速度);
            yield return null;
        }
        this.GetComponent<Collider>().enabled = true;
        transform.rotation = 目標旋轉;
        正在旋轉 = false;
        是否開啟 = true;
    }
    private System.Collections.IEnumerator 關門()
    {
        正在旋轉 = true;
        foreach (GameObject item in 物品)
        {
            item.GetComponent<Collider>().enabled = false;
        }
        this.GetComponent<Collider>().enabled = false;
        while (Quaternion.Angle(transform.rotation, 初始旋轉) > 0.1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, 初始旋轉, Time.deltaTime * 旋轉速度);
            yield return null;
        }
        this.GetComponent<Collider>().enabled = true;
        transform.rotation = 初始旋轉;
        正在旋轉 = false;
        是否開啟 = false;
        
    }
    public void Open()
    {
        清除遺失物品();
        if (狀態 == Statetype.旋轉)
        {
            if (!正在旋轉)
            {
                if (!是否開啟)
                {
                    StartCoroutine(開門());
                }
                else
                {
                    StartCoroutine(關門());
                }
            }
        }
    }
    public void 清除遺失物品()
    {
        List<GameObject> 有效物品列表 = new List<GameObject>();
        for (int i = 0; i < 物品.Length; i++)
        {
            if (物品[i] != null)
            {
                有效物品列表.Add(物品[i]);
            }
        }
        物品 = 有效物品列表.ToArray();
    }
}
