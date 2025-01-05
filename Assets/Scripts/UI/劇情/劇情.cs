using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class 劇情 : MonoBehaviour
{
    // 劇情資料
    [HideInInspector] public 劇情_SO 劇情SO;
    [HideInInspector] public 對話 對話1;

    // UI 元素
    [SerializeField] private GameObject 劇情UI;
    [SerializeField] private Image 劇情視窗;
    [SerializeField] private Text 名稱;
    [SerializeField] private Text 對話內容;
    [SerializeField] private GameObject 互動欄位;
    [SerializeField] private Button 繼續按鈕;
    [SerializeField] private Button 自動按鈕;
    [SerializeField] private GameObject 選項按鈕;
    [SerializeField] private GameObject 歷史紀錄面板;
    [SerializeField] private Transform 歷史紀錄;
    [SerializeField] private GameObject 歷史紀錄內容;
    [SerializeField][Range(1, 100)] private int 文字速度 = 10;
    [SerializeField] private Transform 選項位置;
    [SerializeField] private GameObject 狀態欄位;
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration;
    [SerializeField] private float waitTime;

    // 攝影機控制
    [HideInInspector] public GameObject[] 攝影機;

    // 音效
    private AudioSource audioSource;

    // 劇情控制
    private int index;
    private string currentDialogue;
    private bool 自動播放;
    private bool 顯示完成 = false;
    private bool Control = false;
    private bool 已選擇 = false;
    private 控制 控制;
    private GameObject 互動UI;
    private Coroutine typingCoroutine;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        劇情UI.SetActive(false);
        控制 = GameObject.Find("程式/控制").GetComponent<控制>();
        自動播放 = false;
        互動UI = GameObject.Find("UI控制/提示欄位/互動");
    }

    public void 顯示劇情(bool 是否控制)
    {
        Control = 是否控制;
        foreach (Transform child in 歷史紀錄)
        {
            Destroy(child.gameObject);
        }
        歷史紀錄面板.SetActive(false);
        if (是否控制)
        {
            狀態欄位.SetActive(false);
            劇情視窗.enabled = true;
            自動播放 = false;
            自動按鈕.GetComponentInChildren<Text>().text = "▷";
            互動欄位.gameObject.SetActive(true);
            foreach (Transform child in 互動UI.transform)
            {
                child.gameObject.SetActive(false);
            }
            劇情UI.SetActive(true);
            index = 0;
            顯示當前劇情();
            控制.CursorUnLock();
            切換攝影機();
            SetAlignment(TextAnchor.UpperLeft);
        }
        else
        {
            劇情視窗.enabled = false;
            自動播放 = true;
            互動欄位.gameObject.SetActive(false);
            劇情UI.SetActive(true);
            index = 0;
            顯示當前劇情();
            切換攝影機();
            SetAlignment(TextAnchor.MiddleCenter);
        }
    }
    public void SetAlignment(TextAnchor alignment)
    {
        if (對話內容 != null)
        {
            對話內容.alignment = alignment;
        }
    }

    private void 顯示當前劇情()
    {
        if (對話1.unityEvents.Length > 0 && 劇情SO.劇情[index].事件編號 < 對話1.unityEvents.Length && 劇情SO.劇情[index].事件編號 > 0)
        {
            if (對話1.unityEvents[劇情SO.劇情[index].事件編號 - 1] != null)
            {
                對話1.unityEvents[劇情SO.劇情[index].事件編號 - 1].Invoke();
            }
        }
        if (劇情SO.劇情[index].音效 != null)
        {
            audioSource.clip = 劇情SO.劇情[index].音效;
            audioSource.Play();
        }
        if (Control)
        {
            名稱.text = 劇情SO.劇情[index].名稱;
            currentDialogue = 劇情SO.劇情[index].文字內容;
            typingCoroutine = StartCoroutine(TypeDialogue(劇情SO.劇情[index].文字內容));
            繼續按鈕.gameObject.SetActive(true);
            生成歷史紀錄(劇情SO.劇情[index].名稱, 劇情SO.劇情[index].文字內容);
        }
        else
        {
            名稱.text = 劇情SO.劇情[index].名稱;
            currentDialogue = 劇情SO.劇情[index].文字內容;
            typingCoroutine = StartCoroutine(TypeDialogue(劇情SO.劇情[index].文字內容));
        }
    }
    void 生成歷史紀錄(string 名稱, string 對話)
    {
        GameObject newObject = Instantiate(歷史紀錄內容, 歷史紀錄);
        newObject.transform.SetParent(歷史紀錄);
        newObject.transform.GetChild(0).GetComponent<Text>().text = 名稱;
        newObject.transform.GetChild(1).GetComponent<Text>().text = 對話;
    }
    void 生成選項按鈕()
    {
        繼續按鈕.gameObject.SetActive(false);
        for (int i = 0; i < 劇情SO.劇情[index].選項.Length; i++)
        {
            GameObject newObject = Instantiate(選項按鈕, 選項位置);
            newObject.GetComponentInChildren<Text>().text = 劇情SO.劇情[index].選項[i].選項文字;
            int capturedIndex = i;
            newObject.GetComponent<Button>().onClick.AddListener(() => 選項設定(capturedIndex));
        }
    }
    void 選項設定(int i)
    {
        生成歷史紀錄("選項", 劇情SO.劇情[index].選項[i].選項文字);
        名稱.text = 劇情SO.劇情[index].選項[i].選擇後內容名稱;
        currentDialogue = 劇情SO.劇情[index].選項[i].選擇後內容;
        typingCoroutine = StartCoroutine(TypeDialogue(劇情SO.劇情[index].選項[i].選擇後內容));
        生成歷史紀錄(劇情SO.劇情[index].選項[i].選擇後內容名稱, 劇情SO.劇情[index].選項[i].選擇後內容);
        foreach (Transform child in 選項位置)
        {
            Destroy(child.gameObject);
        }
        已選擇 = true;
        繼續按鈕.gameObject.SetActive(true);
    }
    private IEnumerator TypeDialogue(string dialogue)
    {
        顯示完成 = false;
        對話內容.text = "";
        foreach (char letter in dialogue)
        {
            對話內容.text += letter;
            yield return new WaitForSeconds(1f / 文字速度);
        }
        顯示完成 = true;
        yield return new WaitForSeconds(1f);
        if (自動播放)
        {
            yield return new WaitForSeconds(劇情SO.劇情[index].停留時間);
            下一個();
        }
    }
    public void 自動()
    {
        if (!自動播放)
        {
            播放();
        }
        else
        {
            暫停();
        }
    }
    public void 播放()
    {
        自動播放 = true;
        自動按鈕.GetComponentInChildren<Text>().text = "▶";
        if (顯示完成)
        {
            StopCoroutine(typingCoroutine);
            下一個();
        }
    }
    public void 暫停()
    {
        自動按鈕.GetComponentInChildren<Text>().text = "▷";
        自動播放 = false;
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
        if (劇情SO.劇情[index].選項 != null && 劇情SO.劇情[index].選項.Length > 0 && 已選擇 == false)
        {
            生成選項按鈕();
            return;
        }
        index++;
        已選擇 = false;
        if (index >= 劇情SO.劇情.Length)
        {
            結束();
        }
        else
        {
            顯示當前劇情();
            切換攝影機();
        }
    }

    private void 切換攝影機()
    {
        if (攝影機 != null)
        {
            for (int i = 0; i < 攝影機.Length; i++)
            {
                if (攝影機[i] != null)
                {
                    攝影機[i].SetActive(false);
                }
            }
            if (劇情SO.劇情[index].攝影機位置 < 0 || 劇情SO.劇情[index].攝影機位置 > 攝影機.Length - 1)
            {
                if (攝影機.Length != 0)
                {
                    if (攝影機[0] != null)
                    {
                        攝影機[0].SetActive(true);
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                if (攝影機[劇情SO.劇情[index].攝影機位置] != null)
                {
                    攝影機[劇情SO.劇情[index].攝影機位置].SetActive(true);
                }
                else
                {
                    return;
                }
            }

        }
        else
        {
            return;
        }
    }

    public void 結束()
    {
        if (劇情SO.是否黑幕)
        {
            黑幕();
        }
        else
        {
            關閉劇情();
        }
    }
    void 關閉劇情()
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
        if (Control)
        {
            控制.CursorLock();
            狀態欄位.SetActive(true);
        }
        if (對話1.EndEvent != null)
        {
            對話1.EndEvent.Invoke();
        }
    }
    public void 黑幕()
    {
        StartCoroutine(Fade());
    }
    private IEnumerator Fade()
    {
        fadeImage.gameObject.SetActive(true);
        float timer = 0f;
        Color color = fadeImage.color;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, timer / fadeDuration); // 漸變透明度
            fadeImage.color = color;
            yield return null;
        }
        yield return new WaitForSeconds(waitTime);
        關閉劇情();
        timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, timer / fadeDuration); // 透明度從1漸變到0
            fadeImage.color = color;
            yield return null;
        }
        color.a = 1f;
        fadeImage.color = color;
        fadeImage.gameObject.SetActive(false);
    }
}
