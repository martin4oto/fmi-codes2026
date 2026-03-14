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
    private List<Transform> spawnPoints;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private IEnumerator StartNextWave()
    {
        Debug.Log("starting wave: "  + waveNumber);
        
        yield return new WaitForSecondsRealtime(10f);
        
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
        
        waveNumber++;
        StartCoroutine(StartNextWave());
    }

    public void Start()
    {
        StartCoroutine(StartNextWave());
    }
}

public enum VirusType
{
    basic,
    bomb,
    ranged,
    spawner
}