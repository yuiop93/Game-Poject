using UnityEngine;
using System.Collections;
using UnityEngine.Events;
public class UIScaleEffectWithClose : MonoBehaviour
{
    [Header("UI 設置")]
    [SerializeField] private GameObject uiTransform; // UI 的 Transform
    [SerializeField] private float scaleDuration = 0.5f; // 縮放持續時間
    [SerializeField] private AnimationCurve scaleCurve;  // 動畫曲線

    [Header("狀態")]
    [SerializeField] private bool disableOnClose = true; // 關閉後是否隱藏物件
    [SerializeField] private UnityEvent onClosed; // UI 關閉後的回調

    private bool isOpen = false; // UI 是否開啟

    private void Start()
    {
        // 初始設置為關閉狀態
        uiTransform.transform.localScale = Vector3.zero;
        if (disableOnClose)
            uiTransform.SetActive(false);
    }

    /// <summary>
    /// 開啟 UI
    /// </summary>
    public void OpenUI()
    {
        if (isOpen) return; // 防止重複執行
        isOpen = true;

        // 確保物件激活
        uiTransform.SetActive(true);
        StartCoroutine(ScaleUI(Vector3.one));
    }

    /// <summary>
    /// 關閉 UI
    /// </summary>
    public void CloseUI()
    {
        if (!isOpen) return; // 防止重複執行
        isOpen = false;

        StartCoroutine(ScaleUI(Vector3.zero, () =>
        {
            // 動畫結束後執行
            if (disableOnClose)
                uiTransform.SetActive(false);
        }));
        if (onClosed != null)
            onClosed.Invoke();
    }

    /// <summary>
    /// 縮放 UI
    /// </summary>
    /// <param name="targetScale">目標大小</param>
    /// <param name="onComplete">動畫完成後的回調 (可選)</param>
    /// <returns></returns>
    private IEnumerator ScaleUI(Vector3 targetScale, System.Action onComplete = null)
    {
        float elapsedTime = 0f;
        Vector3 initialScale = uiTransform.transform.localScale;

        while (elapsedTime < scaleDuration)
        {
            float scaleValue = scaleCurve.Evaluate(elapsedTime / scaleDuration);
            uiTransform.transform.localScale = Vector3.Lerp(initialScale, targetScale, scaleValue);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        uiTransform.transform.localScale = targetScale;

        // 動畫完成後的回調
        onComplete?.Invoke();
    }
}
