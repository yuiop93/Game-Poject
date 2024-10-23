using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class 劇情 : MonoBehaviour
{
    [HideInInspector]
    public 劇情_SO 劇情SO;
    public GameObject 劇情UI;
    public Text 名稱;
    public Text 對話內容;
    [SerializeField]
    private int index;
    [SerializeField]
    private Button 繼續按鈕;
    [SerializeField]
    private Button 跳過按鈕;
    [SerializeField]
    private Button 自動按鈕;
    public GameObject[] 攝影機;
    [HideInInspector]
    public bool 自動播放;
    private 控制 控制;
    public GameObject 互動UI;
    private string currentDialogue;
    private Coroutine typingCoroutine;
    private bool 顯示完成 = false;
    void Start()
    {
        劇情UI.SetActive(false);
        控制 = GameObject.Find("程式/控制").GetComponent<控制>();
        自動播放 = false;
    }

    public void 顯示劇情(bool 是否控制)
    {
        if (是否控制)
        {
            自動按鈕.gameObject.SetActive(true);
            跳過按鈕.gameObject.SetActive(true);
            foreach (Transform child in 互動UI.transform)
            {
                child.gameObject.SetActive(false);
            }
            劇情UI.SetActive(true);
            index = 0;
            顯示當前劇情();
            控制.CursorUnLock();
            切換攝影機();
        }
        else
        {
            自動按鈕.gameObject.SetActive(false);
            跳過按鈕.gameObject.SetActive(false);
            foreach (Transform child in 互動UI.transform)
            {
                child.gameObject.SetActive(false);
            }
            劇情UI.SetActive(true);
            index = 0;
            顯示當前劇情();
        }
    }

    private void 顯示當前劇情()
    {
        名稱.text = 劇情SO.劇情[index].名稱;
        currentDialogue = 劇情SO.劇情[index].文字內容;
        typingCoroutine = StartCoroutine(TypeDialogue(劇情SO.劇情[index].文字內容));
    }
    private IEnumerator TypeDialogue(string dialogue)
    {
        顯示完成 = false;
        對話內容.text = "";
        foreach (char letter in dialogue)
        {
            對話內容.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
        顯示完成 = true; 
        yield return new WaitForSeconds(1f);
        if (自動播放)
        {
            下一個();
        }
    }
    public void 自動()
    {
        自動播放 = !自動播放;
        if(自動播放)
        {
            自動按鈕.GetComponentInChildren<Text>().text = "回手動";
            if(顯示完成)
        {
            StopCoroutine(typingCoroutine);
            下一個();
        }
        }
        else
        {
            自動按鈕.GetComponentInChildren<Text>().text = "自動";
        }
        
    }
    public void 繼續()
    {
        if (顯示完成)
        {
            StopCoroutine(typingCoroutine);
            下一個();
        }
        else if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            對話內容.text = currentDialogue;
            顯示完成 = true;
        }
    }

    private void 下一個()
    {
        index++;
        if (index >= 劇情SO.劇情.Length)
        {
            結束();
        }
        else
        {
            顯示當前劇情(); // 顯示下一段劇情
            切換攝影機(); // 切換攝影機
        }
    }

    private void 切換攝影機()
    {
        if (攝影機 != null)
        {
            for (int i = 0; i < 攝影機.Length; i++)
            {
                攝影機[i].SetActive(false);
            }
            if(劇情SO.劇情[index].攝影機位置>攝影機.Length-1){
                攝影機[0].SetActive(true);
            }else
            攝影機[劇情SO.劇情[index].攝影機位置].SetActive(true);
        }
    }

    public void 結束()
    {
        index = 劇情SO.劇情.Length;
        StopCoroutine(typingCoroutine);
        if (攝影機 != null)
        {
            for (int i = 0; i < 攝影機.Length; i++)
            {
                攝影機[i].SetActive(false);
            }
            攝影機 = null;
        }
        foreach (Transform child in 互動UI.transform)
        {
            child.gameObject.SetActive(true);
        }
        劇情UI.SetActive(false);
        控制.CursorLock();
    }

}
