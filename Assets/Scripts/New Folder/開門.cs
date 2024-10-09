using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 開門 : MonoBehaviour
{
    public enum Statetype
    {
        旋轉,
        水平
    }
    public Statetype 狀態;
    [Range(-180, 180)]
    public int 開啟;
    public bool 是否開啟;

    private Vector3 Location;

    public void Start()
    {
        是否開啟 = false;
    }

    public void 互動()
    {
        是否開啟 = !是否開啟;
        if (狀態 == Statetype.旋轉)
        {
            if (是否開啟)
            {
                打開();
            }
            else
            {
                關門();
            }
        }
        else if (狀態 == Statetype.水平)
        {
            if (是否開啟)
            {
                打開();
            }
            else
            {
                關門();
            }
        }
    }
    void 打開()
    {
        if (狀態 == Statetype.旋轉)
        {
            Location = new Vector3(0, 0, 0);
            Vector3 currentAngles = transform.eulerAngles;
            transform.eulerAngles = new Vector3(currentAngles.x, currentAngles.y, currentAngles.z + 開啟);
        }
        else if (狀態 == Statetype.水平)
        {

        }
    }
    void 關門()
    {
        if (狀態 == Statetype.旋轉)
        {
            Location = new Vector3(0, 0, 0);
            Vector3 currentAngles = transform.eulerAngles;
            transform.eulerAngles = new Vector3(currentAngles.x, currentAngles.y, currentAngles.z - 開啟);
        }
        else if (狀態 == Statetype.水平)
        {

        }
    }
}
