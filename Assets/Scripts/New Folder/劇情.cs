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
    public GameObject[] 攝影機;

    public 控制 控制;
    public GameObject 互動UI;
    void Start()
    {
        劇情UI.SetActive(false);
    }
    public void 顯示劇情()
    {
        互動UI.SetActive(false);
        劇情UI.SetActive(true);
        index = 0;
        名稱.text = 劇情SO.劇情[index].名稱;
        對話內容.text = 劇情SO.劇情[index].文字內容;
        控制.CursorUnLock();
        if (攝影機 != null)
        {
            for (int i = 0; i < 攝影機.Length; i++)
            {
                攝影機[i].SetActive(false);
            }
            攝影機[劇情SO.劇情[index].攝影機位置].SetActive(true);
        }
    }
    public void 下一個()
    {
        index++;
        if (index >= 劇情SO.劇情.Length)
        {
            結束();
        }
        else
        {
            名稱.text = 劇情SO.劇情[index].名稱;
            對話內容.text = 劇情SO.劇情[index].文字內容;
            if (攝影機 != null)
            {
                for (int i = 0; i < 攝影機.Length; i++)
                {
                    攝影機[i].SetActive(false);
                }
                攝影機[劇情SO.劇情[index].攝影機位置].SetActive(true);
            }
        }
    }
    public void 結束()
    {
        if (攝影機 != null)
            {
                for (int i = 0; i < 攝影機.Length; i++)
                {
                    攝影機[i].SetActive(false);
                }
                攝影機 = null;
            }
            互動UI.SetActive(true);
            劇情UI.SetActive(false);
            控制.CursorLock();
    }

}
