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
        移動
    }
    [Range(-180, 180)]
    [SerializeField]
    private int 開啟範圍;
    [SerializeField]
    [Range(1, 10)]
    private int 速度 = 3;
    [SerializeField]
    private GameObject[] 物品;
    private bool 是否開啟;
    private Quaternion 初始旋轉;
    private Vector3 初始位置;
    private bool 正在使用 = false;
    private void Start()
    {
        初始旋轉 = transform.rotation;
        初始位置 = transform.position;
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
        正在使用 = true;
        this.GetComponent<Collider>().enabled = false;
        foreach (GameObject item in 物品)
        {
            if (item != null)
            {
                item.GetComponent<Collider>().enabled = true;
            }
        }
        if (狀態.ToString() == "旋轉")
        {
            Quaternion 目標旋轉 = 初始旋轉 * Quaternion.Euler(0, 0, 開啟範圍);
            while (Quaternion.Angle(transform.rotation, 目標旋轉) > 0.1f)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, 目標旋轉, Time.deltaTime * 速度);
                yield return null;
            }
            transform.rotation = 目標旋轉;
        }
        else if (狀態.ToString() == "移動")
        {
            Vector3 目標位置 = 初始位置 + transform.forward * 開啟範圍*0.01f;
            while (Vector3.Distance(transform.position, 目標位置) > 0.1f)
            {
                transform.position = Vector3.Lerp(transform.position, 目標位置, Time.deltaTime * 速度);
                yield return null;
            }
            transform.position = 目標位置;
        }
        this.GetComponent<Collider>().enabled = true;
        
        正在使用 = false;
        是否開啟 = true;

    }
    private System.Collections.IEnumerator 關門()
    {
        正在使用 = true;
        foreach (GameObject item in 物品)
        {
            item.GetComponent<Collider>().enabled = false;
        }
        this.GetComponent<Collider>().enabled = false;
        if (狀態.ToString() == "旋轉")
        {
            while (Quaternion.Angle(transform.rotation, 初始旋轉) > 0.1f)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, 初始旋轉, Time.deltaTime * 速度);
                yield return null;
            }
            transform.rotation = 初始旋轉;
        }
        else if (狀態.ToString() == "移動")
        {
            while (Vector3.Distance(transform.position, 初始位置) > 0.1f)
            {
                transform.position = Vector3.Lerp(transform.position, 初始位置, Time.deltaTime * 速度);
                yield return null;
            }
            transform.position = 初始位置;
        }
        this.GetComponent<Collider>().enabled = true;
        正在使用 = false;
        是否開啟 = false;
    }
    public void Open()
    {
        清除遺失物品();
        if (this.GetComponent<互動>() != null)
            {
                this.GetComponent<互動>().清除按鈕();
            }
        if (!正在使用)
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
