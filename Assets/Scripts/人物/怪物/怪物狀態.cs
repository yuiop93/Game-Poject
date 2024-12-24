using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
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
    private GameObject[] 身體部位;
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

        for (int i = 0; i < 身體部位.Length; i++)
        {
            if (身體部位[i] == null) continue;
            身體部位[i].GetComponent<怪物身體部位>().怪物狀態 = this;
        }
    }
    public void 燃燒(bool 是否燃燒)
    {
        FireEffect[0].SetActive(是否燃燒); // 啟用或禁用燃燒特效
    }
    public void 爆炸()
    {
        GameObject obj = Instantiate(CombustionEffect, transform.position, Quaternion.identity);
        Destroy(obj, 1f);
    }
    private bool 是否燃燒中 = false;
    private bool 是否冰凍中 = false;
    private float 狀態更新間隔 = 1f;
    private Coroutine 狀態協程;
    public void 冰凍值(int 冰凍點數, int _燃燒傷害)
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

            if (狀態協程 == null)
            {
                狀態協程 = StartCoroutine(燃燒效果(_燃燒傷害));
            }
        }
    }
    private IEnumerator 燃燒效果(int _燃燒傷害)
    {
        if (是否燃燒中) yield break;
        是否燃燒中 = true;

        int 燃燒傷害 = (int)(燃燒傷害倍率 * _燃燒傷害);
        燃燒條.SetActive(true);

        while (當前冰凍條 < 0)
        {
            if (是否死亡) break;

            當前冰凍條 += (int)(燃燒下降速度 * Time.deltaTime);
            更新狀態條(燃燒條, 當前冰凍條, 冰凍條上限);
            受傷(燃燒傷害, false);

            if (當前冰凍條 >= 0)
            {
                當前冰凍條 = 0;
                break;
            }
            else if (當前冰凍條 <= -冰凍條上限*0.9f)
            {
                當前冰凍條 = -冰凍條上限 / 2;
                燃燒(true)  ;
                受傷((int)(燃燒傷害倍率 * 冰凍條上限), false);
            }

            yield return new WaitForSeconds(狀態更新間隔);
        }

        燃燒(false);
        是否燃燒中 = false;
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
            if (是否死亡) break;

            當前冰凍條 -= (int)(冰凍下降速度 * Time.deltaTime);
            更新狀態條(冰凍條, 當前冰凍條, 冰凍條上限);

            if (當前冰凍條 <= 0)
            {
                當前冰凍條 = 0;
                冰凍條.SetActive(false);
                是否冰凍 = false;
                冰凍();
                break;
            }

            yield return new WaitForSeconds(狀態更新間隔);
        }

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
        if (血條 != null)
            血條.SetActive(true);
        傷害 = 是否冰凍 ? (int)(傷害 * 冰凍傷害倍率) : 傷害;
        當前血量 -= 傷害;
        if (當前血量 <= 0)
        {
            死亡();
        }
        else
        {
            if (血條 != null)
            {
                血條.GetComponent<HealthBar>().SetHealth(當前血量);
            }
            if (Canhit && !是否冰凍 && 受擊效果)
            {
                this.GetComponent<怪物動畫>().受擊();
            }
        }
    }
    public void 死亡()
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
    }
    private void OnDestroy() {
        if(this.GetComponent<撿起>() != null)
        {
            this.GetComponent<撿起>().獲取();
        }
    }
}
