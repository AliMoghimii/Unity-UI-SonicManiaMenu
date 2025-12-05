using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIMenuScrollController : MonoBehaviour
{
    public enum Direction
    {
        Vertical,
        Horizontal
    }

    [Space(10)] 
    [Header("Menu Objects :")]

    public Transform ButtonController;
    public List<GameObject> Button;

    [Space(10)]
    [Header("Menu Properties :")]
    public int currentButton = 0;
    public float coolDownUntilNextSwitch = 0.25f;
    public Vector3 movementDirection;
    public Direction ScrollDirection = Direction.Vertical;

    [Range(100,600)]
    public float switchSpeed = 150f;
    
    [Space(10)]
    public bool canSwitchTarget = true;
    public bool allowInput = true;

    void OnEnable()
    {
        canSwitchTarget = true;
        allowInput = true;
    }

    void Start()
    {
        if(ScrollDirection == Direction.Vertical)
        {
            if(Screen.width != 1920)
            {
                float scaleRatio = 1920 / switchSpeed;
                switchSpeed = Screen.width / scaleRatio‬;
                //Debug.Log("Screen Width : " + Screen.width);
            }
        }
        if(ScrollDirection == Direction.Horizontal)
        {
            if(Screen.height != 1080)
            {
                float scaleRatio = 1080 / switchSpeed;
                switchSpeed = Screen.height / scaleRatio‬;
                //Debug.Log("Screen Height : " + Screen.height);
            }
        }
    }

    void Update()
    { 
        float direction = Input.GetAxis(ScrollDirection.ToString());

        if(allowInput)
        {
            if(direction != 0)
            {
                StartCoroutine(SwitchTargetRoutine(coolDownUntilNextSwitch,direction));
            }
        }
        Button[currentButton].GetComponent<ParentButton>().Active();
    }

    public IEnumerator SwitchTargetRoutine(float duration,double direction)
    {
        if(canSwitchTarget)
        {
            canSwitchTarget = false;
            Scroll(direction);
            yield return new WaitForSeconds(duration);
            canSwitchTarget = true;
        }
    }

    public void Scroll(double direction)
    {
        if(direction > 0 && currentButton != 0)
        {
            ButtonController.position += new Vector3((-1 * movementDirection.x) * switchSpeed , (-1 * movementDirection.y) * switchSpeed, (-1 * movementDirection.z) * switchSpeed);
            Button[currentButton].GetComponent<ParentButton>().Deactive();
            currentButton--;
        }
        if(direction < 0 && currentButton != Button.Count - 1)
        {
            ButtonController.position += new Vector3(movementDirection.x * switchSpeed , movementDirection.y * switchSpeed, movementDirection.z * switchSpeed);
            Button[currentButton].GetComponent<ParentButton>().Deactive();
            currentButton++;
        }
    }
}
