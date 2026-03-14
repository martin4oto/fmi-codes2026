using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;
    
    private int waveNumber = 0;
    
    [SerializeField]
    private List<Wave> waves;
    
    [SerializeField]
    private List<Transform> spawnPointsLeftUp;
    [SerializeField]
    private List<Transform> spawnPointsRightUp;
    [SerializeField]
    private List<Transform> spawnPointsLeftDown;
    [SerializeField]
    private List<Transform> spawnPointsRightDown;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void StartNextWave()
    {
        StartCoroutine(WaitBetweenWaves());

        int enemiesSpawned = 0;
        int totalViruses = waves[waveNumber].GetTotalViruses();
        
        int directionToFavor = Random.Range(0, 3);
        
        int enemiesPerDirectionLeftUp = Random.Range(0, totalViruses / 4);
        enemiesSpawned += enemiesPerDirectionLeftUp;
        
        int enemiesPerDirectionRightUp = Random.Range(0, totalViruses - enemiesSpawned);
        enemiesSpawned += enemiesPerDirectionRightUp;
        
        int enemiesPerDirectionLeftDown = Random.Range(0, totalViruses - enemiesSpawned);
        enemiesSpawned +=  enemiesPerDirectionLeftDown;
        
        int enemiesPerDirectionRightDown = Random.Range(0, totalViruses - enemiesSpawned);
        enemiesSpawned += enemiesPerDirectionRightDown;

        if (directionToFavor == 0) enemiesPerDirectionLeftUp += totalViruses - enemiesSpawned;
        if (directionToFavor == 1) enemiesPerDirectionRightUp += totalViruses - enemiesSpawned;
        if (directionToFavor == 2) enemiesPerDirectionLeftDown += totalViruses - enemiesSpawned;
        if (directionToFavor == 3) enemiesPerDirectionRightDown += totalViruses - enemiesSpawned;
        
        waveNumber++;
        
        Debug.Log(enemiesSpawned);
        Debug.Log(enemiesPerDirectionLeftUp);
        Debug.Log(enemiesPerDirectionRightUp);
        Debug.Log(enemiesPerDirectionLeftDown);
        Debug.Log(enemiesPerDirectionRightDown);
    }

    public void Start()
    {
        StartNextWave();
    }

    private IEnumerator WaitBetweenWaves()
    {
        yield return new WaitForSecondsRealtime(10f);
    }
}
