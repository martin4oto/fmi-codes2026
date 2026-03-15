using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;
    
    private int waveNumber = 0;
    public bool isInWave = false;
    private bool lastWaveReached = false;
    int lastSpawnedNumber = 0;
    
    [SerializeField]
    private List<Wave> waves;

    [SerializeField]
    private List<Transform> spawnPoints;

    [SerializeField]
    private TMP_Text countdown;
    private int countdownInt = 10;

    [SerializeField]
    private TMP_Text currWave;
    
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

        countdown.gameObject.SetActive(true);
        
        while (countdownInt > 0)
        {
            countdown.text = countdownInt.ToString();
            yield return new WaitForSecondsRealtime(1.0f);
            countdownInt--;
        }

        countdownInt = 10;
        countdown.gameObject.SetActive(false);
        
        isInWave = true;
        
        foreach (var (key, value) in waves[waveNumber].waveStats)
        {
            VirusType typeToSpawn = key;
            int numberToSpawn = value;

            if (lastWaveReached)
            {
                numberToSpawn += Random.Range(lastSpawnedNumber / 4, lastSpawnedNumber / 2);
                lastSpawnedNumber = numberToSpawn;
            }
            
            for (int j = 0; j < numberToSpawn; j++)
            { 
                int transformChosen = Random.Range(0, spawnPoints.Count);

                if(typeToSpawn == VirusType.boss)
                {
                    CellManager.instance.SpawnBoss(spawnPoints[transformChosen].position);
                    break;
                }

                var virusObj = Instantiate(waves[waveNumber].virusPrefabs[typeToSpawn],
                    spawnPoints[transformChosen].position, Quaternion.identity);
                
                CellManager.instance.AddVirus(virusObj.GetComponent<Cell>());
            }
        }

        if (waveNumber < waves.Count - 1)
        {
            waveNumber++;
            currWave.text = "Wave: " + waveNumber.ToString();
        }
        else
        {
            currWave.text = "Wave: " + "Endless!";
            lastWaveReached = true;
        }
    }

    public void Start()
    {
        StartCoroutine(StartNextWave());
    }
}

public enum VirusType
{
    covid,
    smallpox,
    ebola,
    rabbies,
    cancer,
    boss
}