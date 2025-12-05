using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitSelect : MonoBehaviour
{
    public enum Direction
    {
        Vertical,
        Horizontal
    }

    [Space(10)] 
    [Header("Menu Objects :")]
    public List<GameObject> Button;

    [Space(10)]
    [Header("Menu Properties :")]
    public int currentButton = 0;

    [Range(100,250)]
    public float switchSpeed = 150f;
    public float coolDownUntilNextSwitch = 0.25f;
    public Direction ScrollDirection = Direction.Horizontal;
    public bool canSwitchTarget = true;

    void OnEnable()
    {
        canSwitchTarget = true;
    }

    IEnumerator SwitchTargetRoutine(float duration,double direction)
    {
        if(canSwitchTarget)
        {
            canSwitchTarget = false;
            SwitchTarget(direction);
            yield return new WaitForSeconds( duration );
            canSwitchTarget = true;
        }
        else
        {
            yield return new WaitForSeconds( 0f );
        }
    }

    public void SwitchTarget(double direction)
    {
        if(direction > 0 && currentButton != Button.Count - 1)
        {
            Button[currentButton].GetComponent<ButtonAnim>().Deactive();
            currentButton++;
        }
        if(direction < 0 && currentButton != 0)
        {
            Button[currentButton].GetComponent<ButtonAnim>().Deactive();
            currentButton--;
        }
    }

    void FixedUpdate()
    { 
        float direction = Input.GetAxis(ScrollDirection.ToString());

        if(direction != 0)
        {
            StartCoroutine(SwitchTargetRoutine(coolDownUntilNextSwitch,direction));
        }

        Button[currentButton].GetComponent<ButtonAnim>().Active();
    }
}

