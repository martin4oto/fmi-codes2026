using UnityEngine;

public class CellButton : MonoBehaviour
{
    Vector3 startPosition;
    Vector3 targetPosition;
    public float yChange;
    public float moveTime;
    bool moving;
    int dir;
    float timer;
    void Start()
    {
        startPosition = transform.localPosition;
        targetPosition = startPosition + new Vector3(0, yChange, 0);
    }

    void Update()
    {
        if (moving){
            timer += Time.deltaTime * dir;
            timer = Mathf.Clamp(timer, 0f, moveTime);
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, timer/moveTime);
            if (timer == moveTime) moving = false;
        }
    }
    public void Reset()
    {
        dir = -1;
        moving = true;
    }

    public void Activate()
    {
        dir = 1;
        moving = true;
    }
}
