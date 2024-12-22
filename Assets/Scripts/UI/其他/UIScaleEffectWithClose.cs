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
    [SerializeField] private UnityEvent onOpened; // UI 開啟後的回調
    [SerializeField] private UnityEvent onClosed; // UI 關閉後的回調
    [HideInInspector]
    public bool isOpen = false; // UI 是否開啟
    private 控制 f1;
    private void Start()
    {
        f1 = GameObject.Find("程式/控制").GetComponent<控制>();
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
        if (a == null)
        {
            if (isOpen) return; // 防止重複執行
            isOpen = true;
            uiTransform.SetActive(true);
            if (onOpened != null)
            {
                onOpened.Invoke();
            }
            a = StartCoroutine(ScaleUI(Vector3.one));
        }
    }
    /// <summary>
    /// 關閉 UI
    /// </summary>
    public void CloseUI()
    {
        if (a == null)
        {
            if (!isOpen) return; // 防止重複執行
            isOpen = false;
            a = StartCoroutine(ScaleUI(Vector3.zero, () =>
            {
                // 動畫結束後執行
                if (disableOnClose)
                    uiTransform.SetActive(false);
                if (onClosed != null)
                    onClosed.Invoke();
                f1.CursorLock();
            }));
        }
    }
    private Coroutine a;
    /// <summary>
    /// 縮放 UI
    /// </summary>
    /// <param name="targetScale">目標大小</param>
    /// <param name="onComplete">動畫完成後的回調 (可選)</param>
    /// <returns></returns>
    private IEnumerator ScaleUI(Vector3 targetScale, UnityAction onComplete = null)
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
        onComplete?.Invoke();
        a = null;
    }
}
