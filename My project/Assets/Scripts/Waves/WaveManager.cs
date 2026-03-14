using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    
    [SerializeField]
    private List<Transform> spawnPoints;

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
        
        foreach (var (key, value) in waves[waveNumber].waveStats)
        {
            VirusType typeToSpawn = key;
            int numberToSpawn = value;

            for (int j = 0; j < numberToSpawn; j++)
            { 
                int transformChosen = Random.Range(0, spawnPoints.Count);
                    
                var virusObj = Instantiate(waves[waveNumber].virusPrefabs[typeToSpawn], spawnPoints[transformChosen]);
                CellManager.instance.AddVirus(virusObj.GetComponent<Cell>());
            }
        }
        
        waveNumber++;
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

public enum VirusType
{
    basic,
    bomb,
    ranged,
    spawner
}