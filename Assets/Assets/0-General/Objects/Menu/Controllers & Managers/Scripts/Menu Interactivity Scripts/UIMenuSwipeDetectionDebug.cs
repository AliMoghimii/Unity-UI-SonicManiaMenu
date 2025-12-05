using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UIMenuSwipeDetectionDebug : MonoBehaviour
{
    [Header("Swipe Movement Variables :")]
    public int pixelDistanceToDetect = 20;
    public float swipeSpeed = 500;
    public float swipeSpeedMultiplyRate = 5;
    public float swipeSpeedDecreaseRate = 10;
    [SerializeField] private float swipeSpeedTemp;

    [Header("Swipe Limitations Variables :")]
    public Transform SwipeLimitAnchor;
    public int maxDistanceToRelease = 100;
    public float vectorDirectionScale = 50;

    [Header("Vectors :")]
    public bool allowUpSwipe = true;
    public Vector3 UpTargetVector = Vector3.up;
    public bool allowDownSwipe = true;
    public Vector3 DownTargetVector = Vector3.down;
    public bool allowRightSwipe = true;
    public Vector3 RightTargetVector = Vector3.right;
    public bool allowLeftSwipe = true;
    public Vector3 LeftTargetVector = Vector3.left;

    [Header("State Booleans :")]
    [SerializeField] private bool fingerDown;
    public bool DebugMode;
    private Vector3 targetPos;
    private Vector2 startFingerPos;
    private Vector2 startContainerPos;
    [SerializeField] private int debugPixelDistanceToDetect = 20;
    [SerializeField] private float debugSwipeSpeed = 500;
    [SerializeField] private float debugSwipeSpeedMultiplyRate = 5;
    [SerializeField] private float debugSwipeSpeedDecreaseRate = 10;
    [SerializeField] private float debugVectorDirectionScale = 50;
    void Start()
    {
        targetPos = transform.position;

        if(DebugMode)
        {
            pixelDistanceToDetect = debugPixelDistanceToDetect;
            swipeSpeed = debugSwipeSpeed;
            swipeSpeedMultiplyRate = debugSwipeSpeedMultiplyRate;
            swipeSpeedDecreaseRate = debugSwipeSpeedDecreaseRate;
            vectorDirectionScale = debugVectorDirectionScale;
        }
    }

    void Update()
    {
        UpdatePosition();

        if(!DebugMode)
        {
            if(!fingerDown)
            {
                if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
                {
                    startFingerPos = Input.touches[0].position;
                    startContainerPos = transform.position;
                    fingerDown = true;
                }
                swipeSpeedTemp -= swipeSpeedTemp > 0 ? swipeSpeedDecreaseRate : 0;
            }
            else //finger is held
            {
                if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
                {
                    fingerDown = false;
                }
                
                if(Mathf.Abs(startContainerPos.x - transform.position.x) > maxDistanceToRelease || Mathf.Abs(startContainerPos.y - transform.position.y) > maxDistanceToRelease)
                {
                    fingerDown = false;
                }

                if(Input.touches[0].position.y >= startFingerPos.y + pixelDistanceToDetect && allowUpSwipe)
                {
                    if(transform.GetChild(transform.childCount - 1).transform.position.y < SwipeLimitAnchor.position.y)
                    {
                        //Debug.Log("Touch Swipe up");
                        Move(UpTargetVector);
                    }
                    else
                    {
                        fingerDown = false;
                        swipeSpeedTemp = 0;
                    }
                }
                else if(Input.touches[0].position.y <= startFingerPos.y - pixelDistanceToDetect && allowDownSwipe)
                {
                    if(transform.GetChild(0).transform.position.y > SwipeLimitAnchor.position.y)
                    {
                        //Debug.Log("Touch Swipe down");
                        Move(DownTargetVector);
                    }
                    else
                    {
                        fingerDown = false;
                        swipeSpeedTemp = 0;
                    }
                }              
                else if(Input.touches[0].position.x >= startFingerPos.x + pixelDistanceToDetect && allowRightSwipe)
                {
                    if(transform.GetChild(transform.childCount - 1).transform.position.x < SwipeLimitAnchor.position.x)
                    {
                        //Debug.Log("Touch Swipe right");
                        Move(RightTargetVector);
                    }
                    else
                    {
                        fingerDown = false;
                        swipeSpeedTemp = 0;
                    }
                }
                else if(Input.touches[0].position.x <= startFingerPos.x - pixelDistanceToDetect && allowLeftSwipe)
                {
                    if(transform.GetChild(0).transform.position.x > SwipeLimitAnchor.position.x)
                    {
                        //Debug.Log("Touch Swipe left");
                        Move(LeftTargetVector);
                    }
                    else
                    {
                        fingerDown = false;
                        swipeSpeedTemp = 0;
                    }
                }
            }
        }

        // Debug Mode
        else
        {
            if(!fingerDown)
            {
                if(Input.GetMouseButton(0))
                {
                    startFingerPos = Input.mousePosition;
                    fingerDown = true;
                }
                swipeSpeedTemp -= swipeSpeedTemp > 0 ? swipeSpeedDecreaseRate : 0;
            }
            else
            {
                if(Input.GetMouseButtonUp(0))
                {
                    fingerDown = false;
                }

                else if(Input.mousePosition.y >= startFingerPos.y + pixelDistanceToDetect && allowUpSwipe)
                {
                    if(transform.GetChild(transform.childCount - 1).transform.position.y < SwipeLimitAnchor.position.y)
                    {
                        fingerDown = false;
                        Debug.Log("Mouse Swipe up");
                        Move(UpTargetVector);
                    }
                    else
                    {
                        swipeSpeedTemp = 0;
                    }
                }
                else if(Input.mousePosition.y <= startFingerPos.y - pixelDistanceToDetect && allowDownSwipe)
                {
                    if(transform.GetChild(0).transform.position.y > SwipeLimitAnchor.position.y)
                    {
                        fingerDown = false;
                        Debug.Log("Mouse Swipe down");
                        Move(DownTargetVector);
                    }
                    else
                    {
                        swipeSpeedTemp = 0;
                    }
                }
                else if(Input.mousePosition.x >= startFingerPos.x + pixelDistanceToDetect && allowRightSwipe)
                {
                    if(transform.GetChild(0).transform.position.x > SwipeLimitAnchor.position.x)
                    {
                        fingerDown = false;
                        Debug.Log("Mouse Swipe right");
                        Move(RightTargetVector);
                    }
                }
                else if(Input.mousePosition.x <= startFingerPos.x - pixelDistanceToDetect && allowLeftSwipe)
                {
                    if(transform.GetChild(transform.childCount - 1).transform.position.x < SwipeLimitAnchor.position.x )
                    {
                        fingerDown = false;
                        Debug.Log("Mouse Swipe left");
                        Move(LeftTargetVector);
                    }
                }
            }
        }
    }
    public void Move(Vector3 moveDirection)
    {
        swipeSpeedTemp = swipeSpeed;

        targetPos += moveDirection * vectorDirectionScale;
    }
    public void UpdatePosition()
    {   
        transform.position = Vector3.MoveTowards(transform.position , targetPos , swipeSpeedTemp * swipeSpeedMultiplyRate * Time.deltaTime);
    }
}

//position vs local pos performance ?