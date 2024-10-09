using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class 互動 : MonoBehaviour
{
    public Statetype 狀態;
    public enum Statetype
    {
        開啟,
        劇情,
        撿起,

    }
    public Button 按鈕;
    [HideInInspector]
    public 劇情 劇情1;
    public item_SO item;
    [SerializeField]
    [Range(0, 100)]
    private int 物品數量;
    public GameObject 攝影機;
    public 劇情_SO 劇情SO;

    void Start()
    {
        if (狀態 == Statetype.劇情)
        {
            劇情1 = GameObject.Find("劇情").GetComponent<劇情>();
        }
        按鈕.gameObject.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            按鈕.gameObject.SetActive(true);
        }
    }
    void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame && 按鈕.gameObject.activeSelf == true)
        {
            按鈕.onClick.Invoke();
            if (狀態 == Statetype.劇情)
            {
                if (攝影機!=null)
                {
                    劇情1.攝影機 = 攝影機;
                    攝影機開關();
                }
                else
                {
                    播放劇情();
                }


            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            按鈕.gameObject.SetActive(false);
        }
    }
    void 攝影機開關()
    {
        攝影機.SetActive(true);
        Invoke("播放劇情", 1f);
    }
    void 播放劇情()
    {
        劇情1.劇情SO = 劇情SO;
        劇情1.顯示劇情();
    }
}
