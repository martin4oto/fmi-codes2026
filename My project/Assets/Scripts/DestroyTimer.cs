using UnityEngine;
using static UnityEngine.ParticleSystem;

public class DestroyTimer : MonoBehaviour
{
    public float timer;
    public ParticleSystem particle;

    void Update()
    {
        if (timer <= 0) DestroyThis();
        timer -=Time.deltaTime;
    }

    private void DestroyThis()
    {
        particle?.Play();
        Destroy(gameObject);
    }
}
