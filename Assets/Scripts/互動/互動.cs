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
    [HideInInspector]
    public GameObject 按鈕;
    private Text 按鈕文字;
    private GameObject player;
    void Start()
    {
        互動UI = GameObject.Find("互動").transform;
        player = GameObject.Find("Player");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (預置按鈕 == null)
            {
                預置按鈕 = Resources.Load<GameObject>("Prefab/按鈕");
            }
            按鈕 = Instantiate(預置按鈕);
            按鈕文字 = 按鈕.transform.GetChild(0).GetComponent<Text>();
            按鈕文字.text = this.gameObject.name;
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
        if (按鈕 != null)
        {
            if (this.GetComponent<Collider>().enabled == false)
            {
                Destroy(按鈕.gameObject);
                按鈕 = null;
            }
            if (Keyboard.current.fKey.wasPressedThisFrame && 按鈕.activeSelf == true)
            {
                for (int i = 0; i < 按鈕.transform.childCount; i++)
                {
                    Transform child = 按鈕.transform.GetChild(i);

                    if (child.gameObject.name == "選擇UI")
                    {
                        if (this.GetComponent<對話>() != null)
                        {
                            this.GetComponent<對話>().播放劇情();
                            break;
                        }
                        else if (this.GetComponent<坐下>() != null)
                        {
                            this.GetComponent<坐下>().sit();
                            Destroy(按鈕.gameObject);
                            break;
                        }
                        else if (this.GetComponent<打開>() != null)
                        {
                            this.GetComponent<打開>().Open();
                            Destroy(按鈕.gameObject);
                            break;
                        }
                        else if (this.GetComponent<撿起>() != null)
                        {
                            this.GetComponent<撿起>().獲取();
                            Destroy(按鈕.gameObject);
                            break;
                        }
                        else if (this.GetComponent<提交道具>() != null)
                        {
                            this.GetComponent<提交道具>().提交();
                            break;
                        }
                    }

                }
            }
        }
    }
}