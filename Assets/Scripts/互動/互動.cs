using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class 互動 : MonoBehaviour
{
    private Transform 互動UI;
    [SerializeField]
    private GameObject 預置按鈕;
    public GameObject 按鈕;
    private Text 按鈕文字;
    private GameObject player;
    private 對話 對話1;
    private 坐下 坐下1;
    private 打開 打開1;

    void Start()
    {
        if (this.GetComponent<對話>() != null)
        {
            對話1 = this.GetComponent<對話>();
        }
        if (this.GetComponent<坐下>() != null)
        {
            坐下1 = this.GetComponent<坐下>();
        }
        if (this.GetComponent<打開>() != null)
        {
            打開1 = this.GetComponent<打開>();
        }
        互動UI = GameObject.Find("互動").transform;
        player = GameObject.Find("Player");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (預置按鈕 == null)
            {
                預置按鈕 = Resources.Load<GameObject>("按鈕");
            }
            按鈕 = Instantiate(預置按鈕);
            按鈕文字 = 按鈕.transform.GetChild(0).GetComponent<Text>();
            按鈕文字.text =this.gameObject.name;
            按鈕.transform.SetParent(互動UI, false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(按鈕.gameObject);
            按鈕 = null;
        }
    }
    void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame && 按鈕 != null&&按鈕.activeSelf==true)
        {
            for (int i = 0; i < 按鈕.transform.childCount; i++)
            {
                Transform child = 按鈕.transform.GetChild(i);

                if (child.gameObject.name == "選擇UI")
                {
                    if (對話1 != null)
                    {
                        對話1.播放劇情();
                    }
                    else if (坐下1 != null)
                    {
                        坐下1.sit();
                        Destroy(按鈕.gameObject);
                    }
                    else if (打開1 != null)
                    {
                        打開1.Open();
                    }
                }
            }

        }
    }

}