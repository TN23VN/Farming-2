using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public static string next_scene = "SampleScene";
    
    public GameObject progressBar;
    public Text txtPercent;
    private float fixedLoadTime = 3f;

    private void Start()
    {
        //StartCoroutine(LoadScene(next_scene)); loading nhanh
        StartCoroutine(LoadTime(next_scene));
    }

    /*/chua can thiet lam
    public IEnumerator LoadScene(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.GetComponent<Image>().fillAmount = progress;
            txtPercent.text = (progress * 100).ToString("0") + "%";
            yield return null;
        }
    }
    /*/

    public IEnumerator LoadTime(string sceneName)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fixedLoadTime)
        {
            float progress = Mathf.Clamp01(elapsedTime / fixedLoadTime);
            progressBar.GetComponent <Image>().fillAmount = progress;
            txtPercent.text = (progress * 100).ToString("0") + "%";

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }
}
