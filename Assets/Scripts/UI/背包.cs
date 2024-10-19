using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class 背包 : MonoBehaviour
{
    [SerializeField]
    private GameObject 背包UI;
    [SerializeField]
    private 控制 f1;
    [SerializeField]
    private 物品顯示 物品顯示1;
    public void 顯示背包()
    {
        物品顯示1.更新背包();
        背包UI.SetActive(true);
        f1.CursorUnLock();
    }
    public void 隱藏背包()
    {
        背包UI.SetActive(false);
        f1.CursorLock();
    }
    void Start()
    {
        背包UI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (背包UI.activeSelf == true & Input.GetKeyDown(KeyCode.Escape))
        {
            隱藏背包();
        }
        if (背包UI.activeSelf == false&Keyboard.current.bKey.wasPressedThisFrame)
        {
            顯示背包();
        }
    }
}
