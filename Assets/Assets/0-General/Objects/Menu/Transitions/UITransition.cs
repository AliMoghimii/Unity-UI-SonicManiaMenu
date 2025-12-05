using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITransition : MonoBehaviour
{
    public KeyCode Action = KeyCode.Escape;
    public GameObject currentMenu;
    public GameObject TargetMenu;
    [SerializeField] private Animator TransitionAnimator; 
    [SerializeField] private EventCaller TransitionScript; 
    public bool allowInput = true;

    void Update()
    {
        if(InputManager.Instance.GlobalInput)
        {
            if(allowInput)
            {
                if(Input.GetKeyDown(Action))
                {   
                    TransitionScript.Target = TargetMenu;
                    TransitionScript.Current = currentMenu;

                    //TargetMenu.GetComponent<MenuObjectManager>().DefaultSettings();

                    TransitionAnimator.SetBool("Active",true);
                    InputManager.Instance.GlobalInput = false;
                    
                    SoundManager.Instance.PlayButtonAccept();
                }
            }
        }
    }
}
