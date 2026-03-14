using System.Collections;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private const float TARGET_SIZE_CLOSED = 0.01f;
    private const float TARGET_SIZE_OPEN = 1f;

    [SerializeField]
    private GameObject options;
    [SerializeField]
    private GameObject darkEffect;
    [SerializeField]
    private float animationSpeed = 0.5f;

    private bool isOptionsAnimationActive;

    private void Update()
    {
        if (InputManager.instance.OptionsInput && !isOptionsAnimationActive)
        {
            if (!options.activeInHierarchy) OpenOptionsAnimation(TARGET_SIZE_OPEN, animationSpeed);
            else CloseOptionsAnimation(TARGET_SIZE_CLOSED, animationSpeed);

            InputManager.instance.UseOptionsInput();
        }
    }

    public void OpenOptionsAnimation(float targetSize, float duration)
    {
        StartCoroutine(OpenOptionsAnimationCoroutine(options, targetSize, duration));

        InputManager.instance.StopGameInputs = true;
    }

    public void CloseOptionsAnimation(float targetSize, float duration)
    {
        StartCoroutine(CloseOptionsAnimationCoroutine(options, targetSize, duration));

        InputManager.instance.StopGameInputs = false;
    }

    private IEnumerator OpenOptionsAnimationCoroutine(GameObject menu, float targetSize, float duration)
    {
        isOptionsAnimationActive = true;
        darkEffect.SetActive(true);

        menu.SetActive(true);

        Vector3 initialScale = menu.transform.localScale;
        Vector3 targetScale = new Vector3(targetSize, targetSize, menu.transform.localScale.z);

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            menu.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        menu.transform.localScale = targetScale;
        isOptionsAnimationActive = false;
    }

    private IEnumerator CloseOptionsAnimationCoroutine(GameObject menu, float targetSize, float duration)
    {
        isOptionsAnimationActive = true;
        Vector3 initialScale = menu.transform.localScale;
        Vector3 targetScale = new Vector3(targetSize, targetSize, menu.transform.localScale.z);

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            menu.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        menu.transform.localScale = targetScale;
        menu.SetActive(false);
        isOptionsAnimationActive = false;
        darkEffect.SetActive(false);
    }
    
    #region ButtonActions

    public void Resume()
    {
        CloseOptionsAnimation(TARGET_SIZE_CLOSED, animationSpeed);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        LevelLoader.instance.LoadLevel(false);
    }

    #endregion
}
