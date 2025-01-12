using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Events;

public class 坐下 : MonoBehaviour
{
    [SerializeField]
    private Transform sitpoint;
    [SerializeField]
    private Transform standpoint;
    private Animator animator;
    private GameObject player;
    private StarterAssetsInputs inputs;
    public static bool isSitting = false;
    private bool sittingdown = false;
    private GameObject sitUI;
    private GameObject eventhit;
    public GameObject VsCamera;
    public UnityEvent onSubmitConfirmed;
    [SerializeField]
    private string eventtext;

    void Start()
    {
        sitUI = GameObject.Find("UI控制/提示欄位/坐下/坐下UI");
        if (onSubmitConfirmed != null)
        {
            eventhit = sitUI.transform.GetChild(1).gameObject;
        }
        player = GameObject.Find("Player");
        inputs = player.GetComponent<StarterAssetsInputs>();
        animator = FindObjectOfType<PlayerInput>().GetComponent<Animator>();
    }
    IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        sittingdown = false;
        sitUI.SetActive(true);
        if (onSubmitConfirmed.GetPersistentEventCount() > 0||eventtext!="")
        {
            eventhit.SetActive(true);
            eventhit.GetComponent<Text>().text = "按下空白鍵" + eventtext;
        }
        else
        {
            eventhit.SetActive(false);
        }
    }
    public void sit()
    {
        if (this.GetComponent<互動>() != null)
        {
            this.GetComponent<互動>().清除按鈕();
        }
        isSitting = true;
        sittingdown = true;
        inputs.Cursorlock();
        StartCoroutine(WaitAndPrint(2f));
        player.transform.SetPositionAndRotation(sitpoint.transform.position, sitpoint.transform.rotation);
        player.GetComponent<CharacterController>().enabled = false;
        animator.SetTrigger("Sit");
    }
    public void stand()
    {
        Invoke("Standing", .2f);
        animator.SetTrigger("Stand");
        sitUI.SetActive(false);
    }
    void Standing()
    {
        inputs.jump = false;
        player.GetComponent<CharacterController>().enabled = true;
        if (sitpoint != null)
            player.GetComponent<CharacterMovement>().MoveTo(standpoint);
        isSitting = false;
    }
    void Update()
    {
        if (isSitting && !sittingdown && sitUI.activeSelf)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                GameObject.Find("UI控制/彈出UI/電腦").GetComponent<電腦畫面程式>().開啟(VsCamera);
                if (onSubmitConfirmed.GetPersistentEventCount() > 0)
                {
                    onSubmitConfirmed.Invoke();
                }
                sitUI.SetActive(false);
            }
            if (Keyboard.current.fKey.wasPressedThisFrame)
            {
                stand();
            }
        }

    }
}
