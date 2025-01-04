using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;
public class 怪物狀態 : MonoBehaviour
{
    [SerializeField]
    private int 怪物血量 = 100;
    // [SerializeField]
    // private int 怪物攻擊力 = 10;
    [SerializeField]
    private string 怪物名稱 = "怪物";

    [SerializeField]
    private GameObject 血條;
    [SerializeField]
    private GameObject 冰凍條;
    [SerializeField]
    private GameObject 燃燒條;
    private int 當前血量;
    private bool 是否死亡 = false;
    [SerializeField]
    private Transform 骨架;
    public bool Canhit = true; // 是否受擊效果

    [SerializeField]
    private int 冰凍條上限 = 100;

    [SerializeField]
    private float 冰凍傷害倍率 = 1.0f;
    [SerializeField]
    private float 燃燒傷害倍率 = 1.0f;
    [SerializeField]
    private float 冰凍下降速度 = 1000f;
    [SerializeField]
    private float 燃燒下降速度 = 1000f;
    private bool 是否冰凍 = false;
    public int 當前冰凍條;
    public bool 小怪物 = false;

    public GameObject[] FireEffect; // 火焰特效
    public GameObject CombustionEffect; // 燃燒特效
    [HideInInspector]
    public 怪物區塊 怪物區域;

    void Start()
    {
        當前血量 = 怪物血量;
        if (血條 != null)
        {
            if (!小怪物)
            {
                血條 = Instantiate(血條);
                血條.name = 怪物名稱 + "血條";
                血條.transform.SetParent(GameObject.Find("UI控制/BOSS血條").transform, false);
                血條.GetComponent<HealthBar>().ReSetHealth(怪物名稱, 怪物血量);
                血條.GetComponent<HealthBar>().SetHealth(當前血量);
                冰凍條 = 血條.transform.Find("冰凍條").gameObject;
                燃燒條 = 血條.transform.Find("燃燒條").gameObject;
                血條.SetActive(true);
            }
            else
            {
                血條.GetComponent<HealthBar>().ReSetHealth(怪物名稱, 怪物血量);
                血條.GetComponent<HealthBar>().SetHealth(當前血量);
                血條.SetActive(false);
                冰凍條.SetActive(false);
                燃燒條.SetActive(false);
            }

        }
        if (骨架 == null)
        {
            骨架 = this.transform;
        }
        搜尋所有子物件(骨架);

    }
    void 搜尋所有子物件(Transform parent)
    {
        foreach (Transform child in parent)
        {
            var 身體部位Component = child.GetComponent<怪物身體部位>();
            if (身體部位Component != null)
            {
                身體部位Component.怪物狀態 = this;
            }
            搜尋所有子物件(child);
        }
    }
    public void 燃燒(bool 是否燃燒)
    {
        FireEffect[0].SetActive(false);
        FireEffect[1].SetActive(是否燃燒);
    }
    public void 爆炸(int 爆炸傷害)
    {
        GameObject obj = Instantiate(CombustionEffect, transform.position, Quaternion.identity);
        if (obj.GetComponent<爆炸物件>() != null)
        {
            obj.GetComponent<爆炸物件>().爆炸傷害 = 爆炸傷害;
        }
    }
    public bool 是否燃燒中 = false;
    private bool 是否冰凍中 = false;
    private float 狀態更新間隔 = 1f;
    private Coroutine 狀態協程;
    public void 冰凍值(int 冰凍點數, int 引爆傷害)
    {

        if (是否死亡) return;
        當前冰凍條 += 冰凍點數;
        當前冰凍條 = Mathf.Clamp(當前冰凍條, -冰凍條上限, 冰凍條上限); // 限制範圍
        if (當前冰凍條 > 0)
        {
            if (!冰凍條.activeSelf) 冰凍條.SetActive(true);
            if (燃燒條.activeSelf) 燃燒條.SetActive(false);

            冰凍條.GetComponent<Image>().fillAmount = (float)當前冰凍條 / 冰凍條上限;

            if (當前冰凍條 >= 冰凍條上限)
            {
                當前冰凍條 = 冰凍條上限;
                是否冰凍 = true;
                if (狀態協程 == null)
                {
                    狀態協程 = StartCoroutine(冰凍回復());
                }
            }
        }
        else if (當前冰凍條 < 0)
        {
            if (!燃燒條.activeSelf) 燃燒條.SetActive(true);
            if (冰凍條.activeSelf) 冰凍條.SetActive(false);
            float 燃燒條比例 = Mathf.Abs((float)當前冰凍條 / 冰凍條上限);
            燃燒條.GetComponent<Image>().fillAmount = 燃燒條比例;
            FireEffect[0].SetActive(true); // 啟用或禁用燃燒特效
            if (當前冰凍條 <= -冰凍條上限)
            {
                爆炸(引爆傷害);
                當前冰凍條 = -冰凍條上限 / 2;
                燃燒(true);
            }
            if (狀態協程 == null)
            {
                狀態協程 = StartCoroutine(燃燒效果());
            }
        }
        else
        {
            if (冰凍條.activeSelf) 冰凍條.SetActive(false);
            if (燃燒條.activeSelf) 燃燒條.SetActive(false);
        }
    }
    private IEnumerator 燃燒效果()
    {
        int _燃燒傷害 = 怪物血量 / 100;
        是否燃燒中 = true;
        float 燃燒累計量 = 0;
        int 燃燒傷害 = (int)(燃燒傷害倍率 * _燃燒傷害);
        燃燒條.SetActive(true);

        while (當前冰凍條 < 0)
        {
            if (是否死亡)
            {
                燃燒(false);
                狀態協程 = null;
                break;
            }
            更新狀態條(燃燒條, 當前冰凍條, 冰凍條上限);
            受傷(燃燒傷害, false);
            if (當前冰凍條 >= 0)
            {
                當前冰凍條 = 0;
                break;
            }
            燃燒累計量 += 燃燒下降速度 * Time.deltaTime;

            // 當累計超過 1 時，將其轉換為整數並更新當前冰凍條
            if (燃燒累計量 >= 1f)
            {
                int 減少值 = (int)燃燒累計量;
                當前冰凍條 += 減少值;
                燃燒累計量 -= 減少值;
            }
            yield return new WaitForSeconds(狀態更新間隔);
        }
        是否燃燒中 = false;
        if(燃燒條 != null) 燃燒條.SetActive(是否燃燒中);
        燃燒(是否燃燒中);
        狀態協程 = null;
    }

    private IEnumerator 冰凍回復()
    {
        if (是否冰凍中) yield break;
        是否冰凍中 = true;

        冰凍();
        冰凍條.SetActive(true);

        while (當前冰凍條 > 0)
        {
            if (是否死亡)
            {
                狀態協程 = null;
                break;
            }
            當前冰凍條 -= (int)(冰凍下降速度 * Time.deltaTime);
            更新狀態條(冰凍條, 當前冰凍條, 冰凍條上限);

            if (當前冰凍條 <= 0)
            {
                當前冰凍條 = 0;
                是否冰凍 = false;
                冰凍();
                break;
            }
            yield return new WaitForSeconds(狀態更新間隔);
        }
        if(冰凍條 != null) 冰凍條.SetActive(是否冰凍);
        是否冰凍中 = false;
        狀態協程 = null;
    }
    private void 更新狀態條(GameObject 狀態條, int 當前值, int 最大值)
    {
        float 比例 = Mathf.Abs((float)當前值 / 最大值);
        狀態條.GetComponent<Image>().fillAmount = 比例;
    }
    void 冰凍()
    {
        this.GetComponent<怪物動畫>().冰凍(是否冰凍);
    }
    public void 受傷(int 傷害, bool 受擊效果)
    {
        鎖定敵人();
        if (血條 != null)
        {
            血條.SetActive(true);
        }

        傷害 = 是否冰凍 ? (int)(傷害 * 冰凍傷害倍率) : 傷害;
        當前血量 -= 傷害;

        if (當前血量 <= 0)
        {
            if (是否死亡) return;
            死亡();
        }
        else
        {
            if (血條 != null)
            {
                血條.GetComponent<HealthBar>()?.SetHealth(當前血量);
            }

            if (Canhit && !是否冰凍 && 受擊效果)
            {
                GetComponent<怪物動畫>()?.受擊();
            }
        }
    }

    void 鎖定敵人()
    {
        if (怪物區域 != null)
        {
            怪物區域.鎖定敵人();
        }
    }
    void 死亡()
    {
        是否死亡 = true;
        if (是否冰凍)
        {
            this.GetComponent<怪物動畫>().冰凍死亡();
        }
        else
        {
            this.GetComponent<怪物動畫>().死亡();
        }
        if (血條 != null)
        {
            Destroy(血條);
        }
        if (this.GetComponent<撿起>() != null)
        {
            this.GetComponent<撿起>().獲取();
        }
    }
}
