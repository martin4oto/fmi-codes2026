using AYellowpaper.SerializedCollections;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializedDictionary("Virus Name", "Number Spawned")]
    public SerializedDictionary<VirusType, int> waveStats = new();

    [SerializedDictionary("Virus Name", "Virus Prefab")]
    public SerializedDictionary<VirusType, GameObject> virusPrefabs = new();
    
    public int GetTotalViruses()
    {
        int count = 0;
        
        foreach (var (key, value) in waveStats)
        {
            count += value;
        }

        return count;
    }
}