using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 互動選擇 : MonoBehaviour
{
    [SerializeField]
    private GameObject 選擇UI;
    private GameObject instance;
    private int value = 0;
    private int changeAmount = 1;
    void Update()
    {
        if (this.transform.childCount > 0)
        {
            if (instance == null)
            {
                instance = Instantiate(選擇UI);
                instance.transform.SetParent(this.transform.GetChild(0), false);
                instance.name = "選擇UI";
            }
        }
        else
        {
            Destroy(instance);

        }
        if (instance != null)
        {
            if (instance.activeSelf)
            {
                float scrollData = Input.GetAxis("Mouse ScrollWheel");
                if (scrollData != 0)
                {
                    value -= (scrollData > 0 ? changeAmount : -changeAmount);
                    if (value < 0)
                    {
                        value = 0;
                    }
                    if (value > this.transform.childCount - 1)
                    {
                        value = this.transform.childCount - 1;
                    }
                    instance.transform.SetParent(this.transform.GetChild(value), false);
                }
            }
        }
    }
}
