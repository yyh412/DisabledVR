using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeScreen : MonoBehaviour
{
    [Header("Fade Settings")]
    public bool fadeOnStart = false;

    public float fadeDuration = 2f;

    public Color fadeColor = Color.black;

    private Renderer rend;

    private Material fadeMaterial;

    void Awake()
    {
        rend = GetComponent<Renderer>();

        if (rend != null)
        {
            fadeMaterial = rend.material;
        }
    }

    void Start()
    {
        if (fadeOnStart)
        {
            FadeIn();
        }
    }

    // 手动淡入
    public void FadeIn()
    {
        StartCoroutine(FadeRoutine(1f, 0f));
    }

    // 手动淡出
    public void FadeOut()
    {
        StartCoroutine(FadeRoutine(0f, 1f));
    }

    // 淡出并切换场景
    public void FadeOutAndLoadScene(string sceneName)
    {
        StartCoroutine(FadeOutAndLoadSceneRoutine(sceneName));
    }

    private IEnumerator FadeOutAndLoadSceneRoutine(string sceneName)
    {
        yield return StartCoroutine(FadeRoutine(0f, 1f));

        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator FadeRoutine(float startAlpha, float endAlpha)
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;

            float alpha = Mathf.Lerp(startAlpha, endAlpha, timer / fadeDuration);

            SetFadeAlpha(alpha);

            yield return null;
        }

        SetFadeAlpha(endAlpha);
    }

    private void SetFadeAlpha(float alpha)
    {
        if (fadeMaterial == null)
            return;

        Color newColor = fadeColor;

        newColor.a = alpha;

        if (fadeMaterial.HasProperty("_BaseColor"))
        {
            fadeMaterial.SetColor("_BaseColor", newColor);
        }
        else if (fadeMaterial.HasProperty("_Color"))
        {
            fadeMaterial.SetColor("_Color", newColor);
        }
    }
}