using UnityEngine;
using System.Collections;

public class Scale : MonoBehaviour
{
    public bool normalScale = false;
    private bool expand = true;
    public float scaleSpeed = 1f;
    public float maxScaleThreshold;
    public float minScaleThreshold;
    public bool lerpScale = true;
    public float lerpExpandTarget = 1.5f;
    public float lerpShrinkTarget = 0.5f;
    public float lerpDuration;
    private RectTransform rt;

    void Start()
    {
        rt = GetComponent<RectTransform>();
    }
    public static IEnumerator ChangeObjectScale(Transform transform, float expandTarget, float shrinkTarget, float duration)
    {
        Vector3 pos = transform.localScale; //Start object's position
        float x_start;
        float y_start;
        float elapsed_time; //Elapsed time

        while(true)
        {
            elapsed_time = 0; //Elapsed time
            x_start = pos.x;
            y_start = pos.y;
            
            while (elapsed_time <= duration) //Inside the loop until the time expires
            {
                pos.x = Mathf.Lerp(x_start, expandTarget, LerpFunctions.EaseInOutF(elapsed_time / duration)); 
                pos.y = Mathf.Lerp(y_start, expandTarget, LerpFunctions.EaseInOutF(elapsed_time / duration)); //Changes and interpolates the position's "y" value
        
                transform.localScale = pos;//Changes the object's position
        
                yield return null; //Waits/skips one frame
        
                elapsed_time += Time.deltaTime; //Adds to the elapsed time the amount of time needed to skip/wait one frame
            }

            elapsed_time = 0;
            x_start = pos.x;
            y_start = pos.y;

            while (elapsed_time <= duration) //Inside the loop until the time expires
            {
                pos.x = Mathf.Lerp(x_start, shrinkTarget, LerpFunctions.EaseInOutF(elapsed_time / duration)); 
                pos.y = Mathf.Lerp(y_start, shrinkTarget, LerpFunctions.EaseInOutF(elapsed_time / duration)); //Changes and interpolates the position's "y" value
        
                transform.localScale = pos;//Changes the object's position
        
                yield return null; //Waits/skips one frame
        
                elapsed_time += Time.deltaTime; //Adds to the elapsed time the amount of time needed to skip/wait one frame
            }
        }
    }    
    void OnEnable()
    {
        if(lerpScale)
            StartCoroutine(ChangeObjectScale(transform , lerpExpandTarget , lerpShrinkTarget , lerpDuration));
    }

    void Update()
    {
        if(normalScale)
        {
            if(expand)
            {
                rt.sizeDelta += new Vector2(scaleSpeed, scaleSpeed);
                if(rt.sizeDelta.x >= maxScaleThreshold)
                {
                    expand = false;
                }
            }
            else
            {
                rt.sizeDelta -= new Vector2(scaleSpeed, scaleSpeed);
                if(rt.sizeDelta.x <= minScaleThreshold)
                {
                    expand = true;
                }
            }
        }
    }
}