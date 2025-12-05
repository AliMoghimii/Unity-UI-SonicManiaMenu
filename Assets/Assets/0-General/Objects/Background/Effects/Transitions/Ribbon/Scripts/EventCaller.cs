using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventCaller : MonoBehaviour
{
    public GameObject Target { get; set; }
    public GameObject Current { get; set; }
    public void MenuSwapEvent()
    {
        Target.SetActive(true);
        Current.SetActive(false);
    }
    public void ResetAnimation()
    {
        gameObject.GetComponent<Animator>().SetBool("Active",false);
        InputManager.Instance.GlobalInput = true;
    }
}
