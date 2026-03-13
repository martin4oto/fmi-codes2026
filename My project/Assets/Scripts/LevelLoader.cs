using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    private static readonly int StartAnim = Animator.StringToHash("Start");
    private static readonly int EndAnim = Animator.StringToHash("End");

    [SerializeField]
    private Animator transition;
    
    [SerializeField]
    private float transitionTime;
    
    [SerializeField]
    private GameObject loadingText;
    
    [SerializeField]
    private Text tipText;
    
    [SerializeField]
    private string[] tips;

    [SerializeField] 
    private Sprite[] loadingScreens;
    
    [SerializeField] 
    private Image loadScreen;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadLevel(bool nextLevel)
    {
        loadingText.SetActive(true);

        loadScreen.sprite = loadingScreens[Random.Range(0, loadingScreens.Length)];
        tipText.text = tips[Random.Range(0, tips.Length)];

        int sceneToLoad = SceneManager.GetActiveScene().buildIndex;
        
        if (nextLevel) sceneToLoad++;
        else sceneToLoad--;

        StartCoroutine(LoadLevelNumber(sceneToLoad));
    }

    private IEnumerator LoadLevelNumber(int levelIndex)
    {
        transition.SetTrigger(StartAnim);

        yield return new WaitForSecondsRealtime(3f);

        SceneManager.LoadScene(levelIndex);
        transition.SetTrigger(EndAnim);
    }
}
