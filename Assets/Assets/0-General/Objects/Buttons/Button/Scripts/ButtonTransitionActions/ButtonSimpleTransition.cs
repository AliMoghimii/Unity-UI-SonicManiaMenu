using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSimpleTransition : MonoBehaviour
{   
    public KeyCode Action = KeyCode.Space;
    public GameObject CurrentMenu;
    public GameObject TargetMenu;
    [SerializeField] private ButtonAnim buttonIsActive;
    [SerializeField] private Animator TransitionAnimator; 
    [SerializeField] private EventCaller TransitionScript; 
    public bool allowInput = true;
    public void Transit()
    {
        gameObject.GetComponent<Animator>().SetBool("Active",false);
        
        TransitionScript.Target = TargetMenu;
        TransitionScript.Current = CurrentMenu;

        TransitionAnimator.SetBool("Active",true);

        InputManager.Instance.GlobalInput = false;
        
        SoundManager.Instance.PlayButtonAccept();
    }
    void Update()
    {
        if(InputManager.Instance.GlobalInput)
        {
            if(buttonIsActive.active)
            {
                if(allowInput)
                {
                    if(Input.GetKeyDown(Action))
                    {
                        Transit();
                    }
                }
            }
        }
    }
}
       
