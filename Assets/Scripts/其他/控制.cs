using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class 控制 : MonoBehaviour
{
    private StarterAssetsInputs inputs;
    [SerializeField]
    private PlayerInput player;
    private GameObject 互動;
    public static bool 互動中 = false;
    public bool[] 武器取得狀態 = new bool[3];
    public static bool 教學 = false;
    public static bool 背包 = false;
    public static bool 武器 = false;
    public GameObject[] 功能UI;
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        判斷功能是否啟用();
    }
    void 判斷功能是否啟用()
    {
        if (教學)
        {
            功能UI[0].SetActive(true);
        }
        else
        {
            功能UI[0].SetActive(false);
        }
        if (背包)
        {
            功能UI[1].SetActive(true);
        }
        else
        {
            功能UI[1].SetActive(false);
        }
        if (武器)
        {
            功能UI[2].SetActive(true);
        }
        else
        {
            功能UI[2].SetActive(false);
        }

    }
    public void 功能開啟(int i)
    {
        switch (i)
        {
            case 0:
                功能UI[0].SetActive(true);
                教學 = true;
                break;
            case 1:
                功能UI[1].SetActive(true);
                背包 = true;
                break;
            case 2:
                功能UI[2].SetActive(true);
                武器 = true;
                break;
        }
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 進入場景時根據設置應用陰影品質
        GameSettings.SetShadowQuality(GameSettings.ShadowQuality);
    }
    public void 取得武器(int i)
    {
        武器取得狀態[i] = true;
    }

    void Awake()
    {
        inputs = player.GetComponent<StarterAssetsInputs>();
        互動 = GameObject.Find("UI控制/提示欄位/互動");
        CursorLock();
    }
    public void CursorLock()
    {
        互動中 = false;
        inputs.Cursorlock();
        互動.SetActive(true);
        player.enabled = true;
    }
    public void CursorUnLock()
    {
        互動中 = true;
        player.enabled = false;
        互動.SetActive(false);
        inputs.CursorUnLock();
    }
}
