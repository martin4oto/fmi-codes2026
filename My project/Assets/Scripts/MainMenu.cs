using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    bool blockButtons;
    public GameObject settingsMenu;
    public void Quit()
    {
        if (blockButtons) return;
        Application.Quit();
        AudioManager.PlaySFX("click");
    }

    public void Play()
    {
        if (blockButtons) return;
        LevelLoader.instance.LoadLevel(true);
        AudioManager.PlaySFX("click");
        blockButtons = true;
    }

    public void Settings()
    {
        if (blockButtons) return;
        settingsMenu.SetActive(true);
        AudioManager.PlaySFX("click");
    }
}
