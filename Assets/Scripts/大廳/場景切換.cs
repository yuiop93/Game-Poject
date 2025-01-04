using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class 場景切換 : MonoBehaviour
{
    public bool 切換場景條件;
    public Image fadeImage; // 用於漸變效果的Image
    public float fadeDuration = 0.5f; // 漸變持續時間
    public float waitTimeAfterFade = 1f; // 漸變結束後等待時間

    public void StartGame(int sceneNumber)
    {
        // 顯示加載界面並載入對應編號的場景
        StartCoroutine(LoadGameScene(sceneNumber));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void 切換場景()
    {
        切換場景條件 = true;
    }

    private IEnumerator LoadGameScene(int sceneNumber)
    {
        string sceneName = GetSceneNameByNumber(sceneNumber); // 根據編號獲取場景名稱
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError($"無效的場景編號: {sceneNumber}");
            yield break;
        }

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        // 顯示載入進度（可選加載條）
        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f)
            {
                if (切換場景條件)
                {
                    if (fadeImage != null)
                    {
                        // 開始漸變效果
                        yield return StartCoroutine(FadeToBlack());
                    }
                    // 等待N秒
                    yield return new WaitForSeconds(waitTimeAfterFade);

                    // 啟用場景加載
                    operation.allowSceneActivation = true;
                    yield return new WaitForSeconds(waitTimeAfterFade);
                    yield return StartCoroutine(FadeToClear());
                }
            }
            yield return null;
        }
    }

    private IEnumerator FadeToBlack()
    {
        fadeImage.gameObject.SetActive(true);
        float timer = 0f;
        Color color = fadeImage.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, timer / fadeDuration); // 漸變透明度
            fadeImage.color = color;
            yield return null;
        }
        color.a = 1f;
        fadeImage.color = color;
    }
    private IEnumerator FadeToClear()
    {
        float timer = 0f;
        Color color = fadeImage.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, timer / fadeDuration); // 透明度從1漸變到0
            fadeImage.color = color;
            yield return null;
        }

        // 確保完全透明
        color.a = 0f;
        fadeImage.color = color;
        fadeImage.gameObject.SetActive(false);
    }

    private string GetSceneNameByNumber(int sceneNumber)
    {
        // 使用編號對應場景名稱
        switch (sceneNumber)
        {
            case 1:
                return "newscene"; // 替換為實際的場景名稱
            case 2:
                return "GameScene2";
            case 3:
                return "GameScene3";
            default:
                return null; // 返回空表示無效場景
        }
    }
}
