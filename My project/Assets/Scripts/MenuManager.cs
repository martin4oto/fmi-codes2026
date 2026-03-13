using Krisnat;
using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject menu;

    public void OpenUIAnimation(float targetSize, float duration)
    {
        StartCoroutine(OpenUIAnimationCoroutine(menu, targetSize, duration));

        InputManager.instance.StopAllInputs = true;
    }

    public void CloseUIAnimation(float targetSize, float duration)
    {
        StartCoroutine(CloseUIAnimationCoroutine(menu, targetSize, duration));

        InputManager.instance.StopAllInputs = false;
    }

    private IEnumerator OpenUIAnimationCoroutine(GameObject menu, float targetSize, float duration)
    {
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
    }

    private IEnumerator CloseUIAnimationCoroutine(GameObject menu, float targetSize, float duration)
    {
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
    }
}
