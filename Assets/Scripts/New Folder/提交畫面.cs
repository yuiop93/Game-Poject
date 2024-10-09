using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class 提交畫面 : MonoBehaviour
{
    public GameObject 提交畫面UI;
    public GameObject 提交按鈕;
    public Button 取消按鈕;
    public void 顯示提交畫面()
    {
        提交畫面UI.SetActive(true);
    }
    public void 提交()
    {
        提交畫面UI.SetActive(false);
        提交按鈕.SetActive(false);
        取消按鈕.gameObject.SetActive(false);
    }
    void Start()
    {
        提交畫面UI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (提交畫面UI.activeSelf == true & Input.GetKeyDown(KeyCode.Escape))
        {
            取消按鈕.onClick.Invoke();
        }
    }
}
