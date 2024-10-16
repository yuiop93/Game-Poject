using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class 玩家狀態 : MonoBehaviour
{
    public static Statetype 狀態;
    public enum Statetype
    {
        正常,
        遠程,
        近戰,
        死亡,
    }

    public static int 血量;
    [SerializeField]
    private int 血量上限 = 100;
    public static int 體力;
    [SerializeField]
    private int 體力上限 = 100;
    public static int 能量;
    [SerializeField]
    private int 能量上限 = 100;
    [SerializeField]
    private Image 血條;
    [SerializeField]
    private Image 體力條;
    [SerializeField]
    private Image 能量條;
    [SerializeField]
    private GameObject 血條UI;
    [SerializeField]
    private GameObject 體力條UI;
    [SerializeField]
    private GameObject 能量條UI;
    void Start()
    {
        血量 = 血量上限;
        體力 = 體力上限;
        能量 = 能量上限;
    }
    void Update()
    {
        血條.fillAmount = 血量 / 100f;
        體力條.fillAmount = 體力 / 100f;
        能量條.fillAmount = 能量 / 100f;
        if (血量 <= 0)
        {
            狀態 = Statetype.死亡;
        }
        if (狀態 == Statetype.正常)
        {
            體力條UI.SetActive(false);
            能量條UI.SetActive(false);
        }
        else if (狀態 == Statetype.近戰)
        {
            體力條UI.SetActive(true);
            能量條UI.SetActive(false);
        }
        else if (狀態 == Statetype.遠程)
        {
            體力條UI.SetActive(false);
            能量條UI.SetActive(true);
        }
    }
}
