using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuSwipeDetection : MonoBehaviour
{
    [Header("Swipe Movement Variables :")]
    [SerializeField] private int pixelDistanceToDetect = 20;
    [SerializeField] private float swipeSpeed;
    [SerializeField] private float maxSwipeSpeed = 100f;
    [SerializeField] private float swipeSpeedDecreaseRate;
    [SerializeField] private float elapsedTime;

    [Header("Swipe Limitations Variables :")]
    public Transform SwipeLimitAnchor;

    [Header("Vectors :")]
    public bool allowUpSwipe = true;
    public Vector3 UpTargetVector = Vector3.up;
    public bool allowDownSwipe = true;
    public Vector3 DownTargetVector = Vector3.down;
    public bool allowRightSwipe = true;
    public Vector3 RightTargetVector = Vector3.right;
    public bool allowLeftSwipe = true;
    public Vector3 LeftTargetVector = Vector3.left;
    private Vector3 PreviousSwipe;

    [Header("State Booleans :")]
    [SerializeField] private bool fingerDown;
    [SerializeField] private bool fingerReleased;
    private Vector3 targetPos;
    private Vector3 startFingerPos;

//--------------------------------------------------------------------------------------------------------------------------------------------

    void Start()
    {
        targetPos = transform.position;
    }

    private void speedControl()
    {
        if(swipeSpeed >= maxSwipeSpeed)
        {
            swipeSpeed = maxSwipeSpeed;
        }
        else if(swipeSpeed <= maxSwipeSpeed * -1)
        {
            swipeSpeed = maxSwipeSpeed * -1;
        }
    }

    private bool speedCheck()
    {
        if(swipeSpeed < maxSwipeSpeed || swipeSpeed > maxSwipeSpeed * -1)
        {
            return true;
        }
        return true;
    }

    private bool VerticalCheck()
    {
        if(allowUpSwipe && allowDownSwipe)
        {
            return true;
        }
        return false;
    }
    private bool HorizontalCheck()
    {
        if(allowRightSwipe && allowLeftSwipe)
        {
            return true;
        }
        return false;
    }

//--------------------------------------------------------------------------------------------------------------------------------------------    

    void Update()
    {
        speedControl();

        if(!fingerDown)
        {
            if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                startFingerPos = Input.touches[0].position;
                fingerDown = true;

                elapsedTime = 0;
                swipeSpeed = 0;
            }

            if(fingerReleased)
            {
                updateSpeed(PreviousSwipe);

                if(swipeSpeed <= 0)
                    fingerReleased = false;
            }
               
        }
        else //finger is held
        {
            if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
            {
                fingerDown = false;
                fingerReleased = true;
            }

            if(Input.touches[0].position.y >= startFingerPos.y + pixelDistanceToDetect && allowUpSwipe)
            {
                if(transform.GetChild(transform.childCount - 1).transform.position.y < SwipeLimitAnchor.position.y)
                {
                    elapsedTime = 0;
                    swipeSpeed = 0;
                    Debug.Log("Touch Swipe up");
                    Move(UpTargetVector);
                    PreviousSwipe = UpTargetVector;
                }
                else
                {
                    fingerDown = false;
                    swipeSpeed = 0;
                }
            }
            else if(Input.touches[0].position.y <= startFingerPos.y - pixelDistanceToDetect && allowDownSwipe)
            {
                if(transform.GetChild(0).transform.position.y > SwipeLimitAnchor.position.y)
                {
                    elapsedTime = 0;
                    swipeSpeed = 0;
                    Debug.Log("Touch Swipe down");
                    Move(DownTargetVector);
                    PreviousSwipe = DownTargetVector;
                }
                else
                {
                    fingerDown = false;
                    swipeSpeed = 0;
                }
            }              
            else if(Input.touches[0].position.x >= startFingerPos.x + pixelDistanceToDetect && allowRightSwipe)
            {
                if(transform.GetChild(transform.childCount - 1).transform.position.x < SwipeLimitAnchor.position.x)
                {
                    elapsedTime = 0;
                    swipeSpeed = 0;
                    Debug.Log("Touch Swipe right");
                    Move(RightTargetVector);
                    PreviousSwipe = RightTargetVector;
                }
                else
                {
                    fingerDown = false;
                    swipeSpeed = 0;
                }
            }
            else if(Input.touches[0].position.x <= startFingerPos.x - pixelDistanceToDetect && allowLeftSwipe)
            {
                if(transform.GetChild(0).transform.position.x > SwipeLimitAnchor.position.x)
                {
                    elapsedTime = 0;
                    swipeSpeed = 0;
                    Debug.Log("Touch Swipe left");
                    Move(LeftTargetVector);
                    PreviousSwipe = LeftTargetVector;
                }
                else
                {
                    fingerDown = false;
                    swipeSpeed = 0;
                }
            }
        }
    }
//--------------------------------------------------------------------------------------------------------------------------------------------
    public Vector3 MovementRestrictionFormula(Vector3 directionVector)
    {
        float knownVariable = 0;
        float unknownVariable = 0;
        //float m = Mathf.Tan(Vector3.Angle(directionVector, Vector3.right)); //alternative for finding the slope value (Not working?)
        float m = directionVector.y/directionVector.x;
        float c = transform.position.y - (transform.position.x * m);

        if(VerticalCheck())
        {
            float offset = Input.touches[0].position.y - startFingerPos.y; //we use an offset to find out how much the original object should move according to the location of our finger
            startFingerPos = Input.touches[0].position; //this line resets startFingerPos so the offset sets to 0 when a normal swipe is done

            knownVariable = transform.position.y + offset; //translates the movement amount of our finger to the amount the container should be moving
            
            if(directionVector.x == 0) 
               return new Vector3(transform.position.x,knownVariable,1);

            unknownVariable = (knownVariable-c)/m; //formula to snap the container to the scroll bar

            return new Vector3(unknownVariable,knownVariable,1);
        }
        else if(HorizontalCheck()) //same as the first IF but the formula and offset calculations are set to finding the x axis value
        {
            float offset = Input.touches[0].position.x - startFingerPos.x;
            startFingerPos = Input.touches[0].position;
    
            knownVariable = transform.position.x + offset;

            if(directionVector.y == 0) 
               return new Vector3(knownVariable,transform.position.y,1);

            unknownVariable = (m*knownVariable)+c;
                   
            return new Vector3(knownVariable,unknownVariable,1);
        }
        return new Vector3();
    }

//--------------------------------------------------------------------------------------------------------------------------------------------

    public void updateSpeed(Vector3 previousSwipeDirection)
    {
        speedControl();

        if(speedCheck())
        {
            //ye moshkel hast inja ke bayad vector ghabli am ro behesh midadam, vali az func i ke oono mide biroon hast
            Debug.Log(previousSwipeDirection);
            transform.position += new Vector3(swipeSpeed * previousSwipeDirection.x , swipeSpeed * previousSwipeDirection.y , 0);
            swipeSpeed -= swipeSpeedDecreaseRate;
        }
    }

//--------------------------------------------------------------------------------------------------------------------------------------------
    public void calculateSpeed(Vector3 directionVector)
    {
        if(speedCheck())
        {
            elapsedTime += Time.deltaTime;
            if(VerticalCheck())
            {
                swipeSpeed = (((transform.position.y - startFingerPos.y) / elapsedTime)/1000) * directionVector.y;
                //Debug.Log(((transform.position.y - startFingerPos.y) / elapsedTime)/1000);
            }
            else if(HorizontalCheck())
            {
                swipeSpeed = (((transform.position.x - startFingerPos.x) / elapsedTime)/1000) * directionVector.x;
            }
        }
    }

//--------------------------------------------------------------------------------------------------------------------------------------------    

    public void Move(Vector3 moveDirection)
    {
        calculateSpeed(moveDirection);
        transform.position = MovementRestrictionFormula(moveDirection);
    }
}

//time ro bayad vaghti harekat mikonam reset konam
// max speed o min speed
// func kardan if o else asli 
// speed ro 0 konam ye jahayi
