using UnityEngine;
using System.Collections;
public class HealthBarAnimation : MonoBehaviour
{
    [SerializeField]
    private RectTransform backgroundBar; // 背景血条
    [SerializeField]
    private float animationDuration = 1f; // 动画持续时间

    private Coroutine animationCoroutine;

    // 每次启用时自动执行动画
    private void OnEnable()
    {
        if (backgroundBar != null)
        {
            StartBackgroundBarAnimation();
        }
    }

    // 启动背景血条动画
    private void StartBackgroundBarAnimation()
    {
        // 如果动画正在进行，停止当前动画
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }
        animationCoroutine = StartCoroutine(AnimateBackgroundBar());
    }

    // 动画协程
    private IEnumerator AnimateBackgroundBar()
    {
        float elapsedTime = 0f;

        // 初始缩放为 0
        backgroundBar.localScale = new Vector3(0, 1, 1);

        // 缓慢从 0 缩放到 1
        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float scale = Mathf.Lerp(0, 1, elapsedTime / animationDuration);
            backgroundBar.localScale = new Vector3(scale, 1, 1);
            yield return null;
        }

        // 确保最终缩放值为 1
        backgroundBar.localScale = new Vector3(1, 1, 1);
    }
}
