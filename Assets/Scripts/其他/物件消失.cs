using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class 物件消失 : MonoBehaviour
{
    [SerializeField]
    private float fadeDuration = 1f; // 衰减持续时间
    private Text textComponent;

    void Start()
    {
        // 获取 Text 组件
        textComponent = GetComponent<Text>();
        StartFadeOut();
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        if (textComponent != null)
        {
            Color originalColor = textComponent.color;

            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                float normalizedTime = t / fadeDuration;
                // 使用 Lerp 函数来逐渐减少透明度
                Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(1, 0, normalizedTime));
                textComponent.color = newColor;
                yield return null; // 等待下一帧
            }

            // 确保透明度设置为 0
            textComponent.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
            Destroy(gameObject); // 如果需要销毁文本物体
        }
        else
        {
            Debug.LogError("没有找到 Text 组件！");
        }
    }
}

