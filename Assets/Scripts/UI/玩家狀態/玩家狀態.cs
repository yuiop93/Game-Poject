using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;
public class 玩家狀態 : MonoBehaviour
{
    public static Statetype 狀態;
    public enum Statetype
    {
        正常,
        瞄準,
        死亡,
    }
    private StarterAssetsInputs _input;
    public static int 血量;
    public static int 體力;
    public int 能量;

    [SerializeField] private int 血量上限 = 100;
    [SerializeField] private int 體力上限 = 100;
    [SerializeField] private int 能量上限 = 100;

    [SerializeField] private Image 血條;
    [SerializeField] private Image 體力條;
    [SerializeField] private Image 能量條;
    [SerializeField] private GameObject 血條UI;
    [SerializeField] private GameObject 體力條UI;
    [SerializeField] private GameObject 能量條UI;


    void Awake()
    {
        _input = GetComponent<StarterAssetsInputs>();
        血量 = 血量上限;
        體力 = 體力上限;
        能量 = 能量上限;
    }
    void 上限()
    {
        if (血量 > 血量上限)
        {
            血量 = 血量上限;
        }
        if (體力 > 體力上限)
        {
            體力 = 體力上限;
        }
        if (能量 > 能量上限)
        {
            能量 = 能量上限;
        }
    }
    public bool 能量使用中 = false;
    [SerializeField]
    private int 能量回復速度 = 1;
    private Coroutine 能量回復協程;

    public void 開始能量回復()
    {
        if (能量回復協程 == null)  // 確保不會有重複的協程
        {
            能量回復協程 = StartCoroutine(能量回復());
        }
    }

    private IEnumerator 能量回復()
    {
        yield return new WaitForSeconds(2f);
        while (能量 < 能量上限)
        {
            if (!能量使用中)
            {
                能量 += 能量回復速度;
                能量條.fillAmount = (float)能量 / 能量上限;

                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                break;
            }
        }
        能量回復協程 = null;
    }

    public void 能量使用(int 能量消耗)
    {
        if (能量 < 能量消耗)
        {
            return;  // 如果能量不足則不進行消耗
        }
        能量 -= 能量消耗;  // 減少能量
        能量條.fillAmount = (float)能量 / 能量上限;  // 更新UI顯示
    }

    public void 受傷(int 傷害, bool 是否被控制)
    {
        if (是否被控制)
        {
            _input.move = Vector2.zero;
        }
        ShowDamageEffect();
        血量 -= 傷害;
    }
    public void 恢復狀態()
    {
    }
    void Update()
    {
        上限();
        血條.fillAmount = (float)血量 / 血量上限;
        體力條.fillAmount = (float)體力 / 體力上限;
        if (血量 <= 0)
        {
            狀態 = Statetype.死亡;
        }
        // 根據不同狀態顯示不同的條
        switch (狀態)
        {
            case Statetype.正常:
                體力條UI.SetActive(false);
                能量條UI.SetActive(false);
                break;
            case Statetype.瞄準:
                體力條UI.SetActive(false);
                能量條UI.SetActive(true);
                break;
            case Statetype.死亡:
                // 您可以在此處添加死亡狀態的邏輯
                break;
        }
    }
    public Image damageOverlay;
    public float fadeDuration = .3f;

    void Start()
    {
        if (damageOverlay != null)
        {
            damageOverlay.color = new Color(1, 0, 0, 0); // 初始為完全透明
        }
    }

    public void ShowDamageEffect()
    {
        if (damageOverlay != null)
        {
            StopCoroutine(FadeOutEffect());
            StartCoroutine(FadeOutEffect());
        }
    }

    private IEnumerator FadeOutEffect()
    {
        float timer = fadeDuration;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            float alpha = Mathf.Clamp01((timer / fadeDuration) * .5f);
            damageOverlay.color = new Color(1, 0, 0, alpha);
            yield return null; // 每幀等待一次
        }
        damageOverlay.color = new Color(1, 0, 0, 0); // 確保完全透明
    }
}
