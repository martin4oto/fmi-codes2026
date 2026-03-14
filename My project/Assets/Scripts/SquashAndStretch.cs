using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SquashAndStretch : MonoBehaviour
{
    public float animationTime; // 0.1
    public float timer;
    public float strenght; // 0.1
    public bool half = false;
    public float xScale;
    public float yScale;
    public Vector3 baseScale;
    bool animationInProgress = false;
    public bool triggerOnClick;
    void Start()
    {
        baseScale = transform.localScale;
    }
    void Update()
    {
        if (timer >= -animationTime/3 && animationInProgress){
            timer-=Time.deltaTime;
            if (timer <= animationTime/2){
                half = true;
            }

            if (!half){
                xScale += strenght * animationTime;
                yScale -= strenght * animationTime;
            }else{
                xScale -= strenght * animationTime;
                yScale += strenght * animationTime;
            }
        }else{
            if (timer <= 0){
                timer += Time.deltaTime;
                xScale += strenght * animationTime;
                yScale -= strenght * animationTime;
                animationInProgress = false;
            }else{
                xScale = baseScale.x;
                yScale = baseScale.y;
            }
        }

        transform.localScale = new Vector3(xScale, yScale, baseScale.z);
    }
    public void Play(){
        animationInProgress = true;
        transform.localScale = baseScale;
        xScale = baseScale.x;
        yScale = baseScale.y;
        timer = animationTime;
        half = false;
    }

    public void OnMouseDown()
    {
        if (triggerOnClick) Play();
    }

}
