using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class 主選單 : MonoBehaviour
{
    public bool 切換場景條件;
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
        //Invoke("切換場景", 5f);
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
                    operation.allowSceneActivation = true;
                }
            }
            yield return null;
        }
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
