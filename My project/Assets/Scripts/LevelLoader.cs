using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    private static readonly int StartAnim = Animator.StringToHash("Start");
    private static readonly int EndAnim = Animator.StringToHash("End");

    public static LevelLoader instance;
    
    [SerializeField]
    private Animator transition;
    
    [SerializeField]
    private float transitionTime;
    
    [SerializeField] 
    private Image loadScreen;

    private void Awake()
    {
        if (instance == null) instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void LoadLevel(bool nextLevel)
    {
        int sceneToLoad = SceneManager.GetActiveScene().buildIndex;
        
        if (nextLevel) sceneToLoad++;
        else sceneToLoad--;

        StartCoroutine(LoadLevelNumber(sceneToLoad));
    }

    private IEnumerator LoadLevelNumber(int levelIndex)
    {
        transition.SetTrigger(StartAnim);

        yield return new WaitForSecondsRealtime(transitionTime);

        SceneManager.LoadScene(levelIndex);
        transition.SetTrigger(EndAnim);
    }
}
