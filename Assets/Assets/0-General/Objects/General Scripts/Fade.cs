using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {

	IEnumerator ChangeColor()
    { 
        while(GetComponent<Image>().color.a > 0)
        {
            Color c = GetComponent<Image>().color;
            c.a -= 0.01f;
            GetComponent<Image>().color = c;
            yield return new WaitForSeconds(0.01f);
        }
        //InputManager.Instance.GlobalInput = true;
    }

    void Awake()
    {
        //InputManager.Instance.GlobalInput = false;
        GetComponent<Image>().color = Color.black;
        StartCoroutine(ChangeColor());
    }
}