using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public class 教學系統 : MonoBehaviour
{
    [SerializeField]
    private 教學_SO[] 教學清單;
    private 教學_SO 教學資料;
    [SerializeField]
    private Text 教學名稱UI;
    [SerializeField]
    private Image 教學圖片UI;
    [SerializeField]
    private RawImage 教學影片UI;
    [SerializeField]
    private Text 教學內容UI;
    [SerializeField]
    private GameObject 教學UI;
    [SerializeField]
    private GameObject 開啟教學按鈕預置物;
    private VideoPlayer videoPlayer;
    [SerializeField]
    private Transform 按鈕生成位置;
    [SerializeField]
    private List<GameObject> 教學按鈕;
    [SerializeField]
    private Button 下一頁按鈕;
    [SerializeField]
    private Button 上一頁按鈕;
    [SerializeField]
    private GameObject 換頁按鈕預置物;
    [SerializeField]
    private Transform 換頁按鈕生成位置;
    [SerializeField]
    private Button[] 換頁按鈕;
    [SerializeField]
    private GameObject 條款UI;

    private int index = 0;
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        下一頁按鈕.onClick.AddListener(顯示下一頁);
        上一頁按鈕.onClick.AddListener(顯示上一頁);
    }
    public void 生成教學按鈕(int 教學編號)
    {
        教學資料 = 教學清單[教學編號];
        GameObject gameObject = Instantiate(開啟教學按鈕預置物, 按鈕生成位置);
        gameObject.GetComponentInChildren<Text>().text = 教學資料.教學名稱;
        教學按鈕.Add(gameObject);
        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(() => 開始顯示教學(教學編號));
        開始顯示教學(教學編號);
    }
    public void 教學按鈕初始化()
    {
        foreach (var item in 教學按鈕)
        {
            item.SetActive(控制.教學);
        }
    }
    void 生成換頁按鈕()
    {
        換頁按鈕 = new Button[教學資料.教學列表.Count];
        for (int i = 0; i < 教學資料.教學列表.Count; i++)
        {
            GameObject gameObject = Instantiate(換頁按鈕預置物, 換頁按鈕生成位置);
            Button button = gameObject.GetComponent<Button>();
            int temp = i;
            button.onClick.AddListener(() => 切換頁面(temp));
            換頁按鈕[i] = button;
        }
    }
    void 切換頁面(int 頁碼)
    {
        index = 頁碼;
        顯示頁面();
    }
    public void 開始顯示教學(int 教學編號)
    {
        if(!控制.教學)
        {
            if(教學編號==0)
            {
            條款UI.SetActive(true);
            }
            else
            {
                return;
            }
        }
        this.GetComponent<UIScaleEffectWithClose>().OpenUI();
        index = 0;
        教學名稱UI.text = 教學清單[教學編號].教學名稱;
        教學資料 = 教學清單[教學編號];
        清除();
        生成換頁按鈕();
        顯示頁面();
    }
    void 顯示頁面()
    {
        if (教學資料.教學列表[index].教學圖片 != null)
        {
            教學圖片UI.sprite = 教學資料.教學列表[index].教學圖片;
        }
        if (教學資料.教學列表[index].教學影片 != null)
        {
            教學影片UI.gameObject.SetActive(true);
            videoPlayer.clip = 教學資料.教學列表[index].教學影片;
            videoPlayer.Play();
        }
        else
        {
            教學影片UI.gameObject.SetActive(false);
            videoPlayer.clip = null;
            videoPlayer.Stop();
        }
        教學內容UI.text = 教學資料.教學列表[index].教學內容;
        判斷頁碼();
    }
    void 清除()
    {
        教學圖片UI.sprite = null;
        videoPlayer.Stop();
        教學內容UI.text = "";
        for (int i = 0; i < 換頁按鈕.Length; i++)
        {
            Destroy(換頁按鈕[i].gameObject);
        }
    }
    public void 顯示下一頁()
    {
        if (index < 教學資料.教學列表.Count - 1)
        {
            index++;
            顯示頁面();
        }
    }

    public void 顯示上一頁()
    {
        if (index > 0)
        {
            index--;
            顯示頁面();
        }
    }
    void 判斷頁碼()
    {
        if (index == 0)
        {
            上一頁按鈕.interactable = false;
        }
        else
        {
            上一頁按鈕.interactable = true;
        }
        if (index == 教學資料.教學列表.Count - 1)
        {
            下一頁按鈕.interactable = false;
        }
        else
        {
            下一頁按鈕.interactable = true;
        }
        for (int i = 0; i < 教學資料.教學列表.Count; i++)
        {
            if (i == index)
            {
                換頁按鈕[i].interactable = false;
            }
            else
            {
                換頁按鈕[i].interactable = true;
            }
        }
    }
}
