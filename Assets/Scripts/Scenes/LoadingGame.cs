using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingGame : MonoBehaviour
{
    [SerializeField]
    private GameObject titleCanvas;
    [SerializeField]
    private GameObject loadingCanvas;

    [SerializeField]
    private Slider loadingBar;

    public void LoadGameScene()
    {
        StartCoroutine(loadAsycScene());
    }

    IEnumerator loadAsycScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);

        titleCanvas.SetActive(false);
        loadingCanvas.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            Debug.Log(progress);

            loadingBar.value = progress;

            yield return null;
        }

       
    }
}
