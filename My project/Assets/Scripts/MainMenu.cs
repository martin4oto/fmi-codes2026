using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsMenu;
    public void Quit()
    {
        Application.Quit();
        AudioManager.PlaySFX("click");
    }

    public void Play()
    {
        LevelLoader.instance.LoadLevel(true);
        AudioManager.PlaySFX("click");
    }

    public void Settings()
    {
        settingsMenu.SetActive(true);
        AudioManager.PlaySFX("click");
    }
}
