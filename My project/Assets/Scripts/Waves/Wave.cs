using AYellowpaper.SerializedCollections;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializedDictionary("Virus Prefab", "Number Spawned")]
    public SerializedDictionary<Cell, int> waveStats = new SerializedDictionary<Cell, int>();
    
}
