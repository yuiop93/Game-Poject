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
    private int 當前血量;
    private bool 是否死亡 = false;
    [SerializeField]
    private GameObject[] 身體部位;
    public bool Canhit = true; // 是否受擊效果

    [SerializeField]
    private int 冰凍條上限 = 100;

    [SerializeField]
    private float 冰凍傷害倍率 = 5.0f;
    [SerializeField]
    private float 冰凍下降速度 = 1.0f;

    private bool 是否冰凍 = false;
    public int 當前冰凍條;
    public bool 小怪物 = false;

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
                血條.SetActive(true);
            }
            else
            {
                血條.GetComponent<HealthBar>().ReSetHealth(怪物名稱, 怪物血量);
                血條.GetComponent<HealthBar>().SetHealth(當前血量);
                血條.SetActive(false);
                冰凍條.SetActive(false);
            }

        }

        for (int i = 0; i < 身體部位.Length; i++)
        {
            if (身體部位[i] == null) continue;
            身體部位[i].GetComponent<怪物身體部位>().怪物狀態 = this;
        }
    }
    private Coroutine 冰凍協程;
    public void 冰凍值(int 冰凍點數)
    {
        if (是否死亡) return;
        當前冰凍條 += 冰凍點數;
        冰凍條.SetActive(true);
        冰凍條.GetComponent<Image>().fillAmount = (float)當前冰凍條 / (float)冰凍條上限;
        if (當前冰凍條 >= 冰凍條上限)
        {
            當前冰凍條 = 冰凍條上限;
            是否冰凍 = true;
            冰凍();
            if (冰凍協程 == null)
            {
                冰凍協程 = StartCoroutine(冰凍回復());
            }
        }
    }
    private IEnumerator 冰凍回復()
    {
        while (當前冰凍條 > 0)
        {
            if (!是否死亡)
            {
                當前冰凍條 -= (int)(冰凍下降速度 * Time.deltaTime);
                冰凍條.GetComponent<Image>().fillAmount = (float)當前冰凍條 / (float)冰凍條上限;
                if (當前冰凍條 <= 0)
                {
                    當前冰凍條 = 0;
                    冰凍條.SetActive(false);
                    是否冰凍 = false;
                    冰凍();
                    break;
                }
                yield return new WaitForSeconds(1f);
            }
            else
            {
                break;
            }

        }
        while (當前冰凍條 < 0)
        {
            當前冰凍條 += (int)(冰凍下降速度 * Time.deltaTime);
            if (當前冰凍條 >= 0)
            {
                當前冰凍條 = 0;
                break;
            }
            yield return new WaitForSeconds(1f);
        }
        冰凍協程 = null;
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
}
