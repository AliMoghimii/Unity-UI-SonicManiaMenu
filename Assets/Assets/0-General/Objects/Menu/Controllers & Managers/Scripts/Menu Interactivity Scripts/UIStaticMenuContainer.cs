using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStaticMenuContainer : MonoBehaviour
{
    [Space(10)] 
    [Header("Menu Objects :")]
    public GameObject defaultButton;
    private GameObject activeButton;
    private StaticButtonAdjacentHolder activeButtonScript;

    [Space(10)]
    [Header("Menu Properties :")]
    public float coolDownUntilNextSwitch = 0.25f;
    
    [Space(10)]
    public bool canSwitchTarget = true;
    public bool allowInput = true;

    void OnEnable()
    {
        activeButton = defaultButton;

        activeButton.GetComponent<ButtonAnim>().Active();
        activeButtonScript = activeButton.GetComponent<StaticButtonAdjacentHolder>();

        canSwitchTarget = true;
        allowInput = true;
    }

    void OnDisable()
    {
        activeButton = defaultButton;
    }

    void Update()
    {   
        if(Input.GetKeyDown(activeButtonScript.UpAction) && activeButtonScript.UpButton!=null)
        {
            nextButtonActivateProcedure(activeButtonScript.UpButton);
        }
        if(Input.GetKeyDown(activeButtonScript.DownAction) && activeButtonScript.DownButton!=null)
        {
            nextButtonActivateProcedure(activeButtonScript.DownButton);
        }
        if(Input.GetKeyDown(activeButtonScript.RightAction) && activeButtonScript.RightButton!=null)
        {
            nextButtonActivateProcedure(activeButtonScript.RightButton);
        }
        if(Input.GetKeyDown(activeButtonScript.LeftAction) && activeButtonScript.LeftButton!=null)
        {
            nextButtonActivateProcedure(activeButtonScript.LeftButton);
        }
    }

    private void nextButtonActivateProcedure(GameObject nextButton)
    {
        activeButton.GetComponent<ButtonAnim>().Deactive();

        activeButton = nextButton;
        activeButtonScript = nextButton.GetComponent<StaticButtonAdjacentHolder>();
        activeButton.GetComponent<ButtonAnim>().Active();

        StartCoroutine(SwitchTargetRoutine(coolDownUntilNextSwitch)); 
    }

    IEnumerator SwitchTargetRoutine(float duration)
    {
        if(canSwitchTarget)
        {
            canSwitchTarget = false;
            yield return new WaitForSeconds(duration);
            canSwitchTarget = true;
        }
    }
}
