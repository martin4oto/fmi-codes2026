using AYellowpaper.SerializedCollections;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializedDictionary("Virus Prefab", "Number Spawned")]
    public SerializedDictionary<GameObject, int> waveStats = new SerializedDictionary<GameObject, int>();

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