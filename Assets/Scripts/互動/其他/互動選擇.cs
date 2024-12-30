using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class 互動選擇 : MonoBehaviour
{
    public StarterAssetsInputs _input;
    private GameObject 選擇UI;
    private GameObject instance;
    private int value = 0;
    private int changeAmount = 1;
    public static bool isSelecting = false;
    void Awake()
    {
        選擇UI = Resources.Load<GameObject>("Prefab/UI/互動/選擇UI");
    }
    void Update()
    {
        if (this.transform.childCount > 0)
        {
            if (instance == null)
            {
                CreateSelectionUI();
            }
        }
        else
        {
            DestroySelectionUI();
        }

        if (instance != null && instance.activeSelf)
        {
            HandleMouseScroll();
        }
    }
    private void CreateSelectionUI()
    {
        isSelecting = true;
        value = 0;
        instance = Instantiate(選擇UI);
        instance.transform.SetParent(this.transform.GetChild(0), false);
        instance.name = "選擇UI";
    }
    private void DestroySelectionUI()
    {
        if (instance != null)
        {
            Destroy(instance);
            instance = null;
        }
        isSelecting = false;
    }
    private void HandleMouseScroll()
    {
        float scrollData = Input.GetAxis("Mouse ScrollWheel");
        if (scrollData != 0)
        {
            value += (scrollData > 0 ? -changeAmount : changeAmount);
            value = Mathf.Clamp(value, 0, this.transform.childCount - 1);
            instance.transform.SetParent(this.transform.GetChild(value), false);
        }
    }
}
