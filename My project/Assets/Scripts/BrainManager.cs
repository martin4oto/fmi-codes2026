using UnityEngine;

public class BrainManager : MonoBehaviour
{
    public static BrainManager instance;

    private void Awake()
    {
        instance = this;
    }
}
