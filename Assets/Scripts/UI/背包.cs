using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class 背包 : MonoBehaviour
{
    [SerializeField]
    private GameObject 背包UI;
    private 控制 f1;
    public void 顯示背包()
    {
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
        f1 = FindObjectOfType<控制>();
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
