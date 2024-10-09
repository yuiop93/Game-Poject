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
    public Text 劇情內容;
    public int index;
    public Button 繼續按鈕;
    public Button 跳過按鈕;
    public GameObject 攝影機;
    
    void Start()
    {
        劇情UI.SetActive(false);
    }
    public void 顯示劇情()
    {
        劇情UI.SetActive(true);
        index = 0;
        名稱.text = 劇情SO.劇情[index].名稱;
        劇情內容.text = 劇情SO.劇情[index].文字內容;
    }
    public void 下一個()
    {
        
        index++;
        if (index >= 劇情SO.劇情.Length)
        {
            跳過按鈕.onClick.Invoke();
            if(攝影機 != null)
            {
                攝影機.SetActive(false);
                攝影機 = null;
            }
        }
        else
        {
            名稱.text = 劇情SO.劇情[index].名稱;
            劇情內容.text = 劇情SO.劇情[index].文字內容;
        }
    }

}
