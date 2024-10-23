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
    private GameObject 預置按鈕;
    [HideInInspector]
    public GameObject 按鈕;
    private Text 按鈕文字;
    private GameObject player;
    [SerializeField]
    private Transform playerTransform;
    public UnityEvent onSubmitConfirmed;
    void Start()
    {
        互動UI = GameObject.Find("UI控制/互動").transform;
        player = GameObject.Find("Player");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (預置按鈕 == null)
            {
                預置按鈕 = Resources.Load<GameObject>("Prefab/panel/互動/按鈕");
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

        if (按鈕 != null && 按鈕.activeSelf == true)
        {
            if (!this.GetComponent<Collider>().enabled || 坐下.isSitting)
            {
                Destroy(按鈕);
                按鈕 = null;
            }
            if (Keyboard.current.fKey.wasPressedThisFrame)
            {
                if (按鈕 != null && 按鈕.activeSelf == true)
                    HandleInteraction();
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
                player.GetComponent<CharacterMovement>().MoveTo(playerTransform);
            if (this.GetComponent<對話>() != null)
            {
                this.GetComponent<對話>().播放劇情();
            }
            else if (this.GetComponent<坐下>() != null)
            {
                this.GetComponent<坐下>().sit();
                Destroy(按鈕.gameObject);
            }
            else if (this.GetComponent<打開>() != null)
            {
                this.GetComponent<打開>().Open();
                Destroy(按鈕.gameObject);
            }
            else if (this.GetComponent<撿起>() != null)
            {
                this.GetComponent<撿起>().獲取();
                Destroy(按鈕.gameObject);
            }
            else if (this.GetComponent<提交道具>() != null)
            {
                this.GetComponent<提交道具>().提交();
            }
            if (onSubmitConfirmed.GetPersistentEventCount() > 0)
            {
                onSubmitConfirmed.Invoke();
            }
        }

    }
}