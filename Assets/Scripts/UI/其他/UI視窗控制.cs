using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI視窗控制 : MonoBehaviour
{
    [SerializeField]
    private GameObject[] uiTransform;
    private 控制 f1;

    void Start()
    {
        f1 = GameObject.Find("程式/控制").GetComponent<控制>();
    }
    void Update()
    {
        for (int i = 0; i < uiTransform.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (uiTransform[i].activeSelf)
                {
                    uiTransform[i].GetComponent<UIScaleEffectWithClose>().CloseUI();
                }
            }
        }
    }
}
