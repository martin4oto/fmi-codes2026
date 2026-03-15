using UnityEngine;
using static UnityEngine.ParticleSystem;

public class DestroyTimer : MonoBehaviour
{
    public float timer;
    public ParticleSystem particle;
    public bool playOnDestroy;

    void Update()
    {
        if (timer <= 0) DestroyThis();
        timer -=Time.deltaTime;
    }

    private void DestroyThis()
    {
        if (playOnDestroy) particle?.Play();
        Destroy(gameObject);
    }
}
