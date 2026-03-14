using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;
    
    private int waveNumber = 0;
    public bool isInWave = false;
    
    [SerializeField]
    private List<Wave> waves;

    [SerializeField]
    private List<Transform> spawnPoints;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    public IEnumerator StartNextWave()
    {
        isInWave = false;
        
        yield return new WaitForSecondsRealtime(10f);
        
        isInWave = true;
        
        foreach (var (key, value) in waves[waveNumber].waveStats)
        {
            VirusType typeToSpawn = key;
            int numberToSpawn = value;

            for (int j = 0; j < numberToSpawn; j++)
            { 
                int transformChosen = Random.Range(0, spawnPoints.Count);

                var virusObj = Instantiate(waves[waveNumber].virusPrefabs[typeToSpawn],
                    spawnPoints[transformChosen].position, Quaternion.identity);
                
                CellManager.instance.AddVirus(virusObj.GetComponent<Cell>());
            }
        }
        
        if(waveNumber < waves.Count - 1) waveNumber++;
        
    }

    public void Start()
    {
        StartCoroutine(StartNextWave());
    }
}

public enum VirusType
{
    covid,
    bomb,
    ebola,
    rabbies,
    duplicating
}