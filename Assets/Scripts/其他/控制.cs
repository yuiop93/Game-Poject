using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class 控制 : MonoBehaviour
{
    private StarterAssetsInputs inputs;
    [SerializeField]
    private PlayerInput player;
    private GameObject 互動;
    public static bool 互動中 = false;
    public bool[] 武器取得狀態 = new bool[3];
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
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
