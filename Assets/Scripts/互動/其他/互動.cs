using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Events;
public class 互動 : MonoBehaviour
{
    private Transform 互動UI;
    [SerializeField]
    private string 互動文字;
    [SerializeField]
    private GameObject 預置按鈕;
    [HideInInspector]
    public GameObject 按鈕;
    private Text 按鈕文字;
    private GameObject player;
    [SerializeField]
    private Transform playerTransform;
    public UnityEvent onSubmitConfirmed;
    [SerializeField] private bool 強制互動 = false;
    void Start()
    {
        互動UI = GameObject.Find("UI控制/提示欄位/互動").transform;
        player = GameObject.Find("Player");
    }
    bool 已生成按鈕=false;


    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (強制互動)
            {
                if (onSubmitConfirmed.GetPersistentEventCount() > 0)
                {
                    onSubmitConfirmed.Invoke();
                    Destroy(this.GetComponent<Collider>());
                }
                return;
            }
            else if(已生成按鈕==false)
            {
                生成按鈕();
            }

        }
    }
    void 生成按鈕()
    {
        已生成按鈕 = true;
        if (預置按鈕 == null)
        {
            預置按鈕 = Resources.Load<GameObject>("Prefab/UI/互動/按鈕");
        }
        按鈕 = Instantiate(預置按鈕);
        按鈕文字 = 按鈕.transform.GetChild(0).GetComponent<Text>();
        if (!string.IsNullOrWhiteSpace(互動文字))
        {
            按鈕文字.text = 互動文字;
        }
        else
        {
            按鈕文字.text = this.gameObject.name;
        }
        按鈕.transform.SetParent(互動UI, false);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            清除按鈕();
        }
    }
    public void 清除按鈕()
    {
        Destroy(按鈕.gameObject);
        按鈕 = null;
        已生成按鈕 = false;
    }
    void Update()
    {
        if (按鈕 != null && 按鈕.activeSelf == true)
        {
            if (!this.GetComponent<Collider>().enabled || 坐下.isSitting)
            {
                清除按鈕();
            }
            if (!控制.互動中)
            {
                if (Keyboard.current.fKey.wasPressedThisFrame)
                {
                    if (按鈕 != null && 按鈕.activeSelf == true)
                        HandleInteraction();
                }
            }
        }
    }
    private void HandleInteraction()
    {
        bool hasSelectionUI = false;
        foreach (Transform child in 按鈕.transform)
        {
            if (child.name == "選擇UI")
            {
                hasSelectionUI = true;
                break;
            }
        }
        if (hasSelectionUI)
        {
            if (playerTransform != null)
                //player.transform.SetPositionAndRotation(playerTransform.transform.position, playerTransform.transform.rotation);
                player.GetComponent<CharacterMovement>().MoveTo(playerTransform);
            if (onSubmitConfirmed.GetPersistentEventCount() > 0)
            {
                onSubmitConfirmed.Invoke();
            }
            else
            {
                清除按鈕();
                Debug.Log("No event assigned to this button");
            }
        }

    }
}