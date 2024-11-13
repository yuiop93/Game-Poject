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
            清除按鈕();
        }
    }
    public void 清除按鈕()
    {
        Destroy(按鈕.gameObject);
        按鈕 = null;
    }
    void Update()
    {

        if (按鈕 != null && 按鈕.activeSelf == true)
        {
            if (!this.GetComponent<Collider>().enabled || 坐下.isSitting)
            {
                清除按鈕();
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
                //player.transform.SetPositionAndRotation(playerTransform.transform.position, playerTransform.transform.rotation);
                player.GetComponent<CharacterMovement>().MoveTo(playerTransform);
            if (onSubmitConfirmed.GetPersistentEventCount() > 0)
            {
                onSubmitConfirmed.Invoke();
            }else
            {
                清除按鈕();
                Debug.Log("No event assigned to this button");
            }
        }

    }
}