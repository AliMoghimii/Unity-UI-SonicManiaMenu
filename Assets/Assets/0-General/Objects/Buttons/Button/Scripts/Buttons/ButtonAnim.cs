using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonAnim : ParentButton
{
    [Header("Animation Controllers")]
    [Range(2,5)]
    public float PopSpeed1 = 2.0f;
    [Range(2,5)]
    public float PopSpeed2 = 2.0f;
    public float maxPopValue1 = 30.0f;
    public float maxPopValue2 = 20.0f;
    
    [Space(10)]
    public bool animationPhase1 = false;
    public bool active = false;

    private float _tempPopValue = 0;
    private Color _backColor;
    private GameObject _back;
    private GameObject _front;
    private GameObject _text;
    private bool swapSoundPlayed;
    private Image backImage;
    private Image BackImage => backImage ? backImage : backImage = _back.GetComponent<Image>(); 
    
    void OnEnable()
    {
        gameObject.GetComponent<Animator>().SetBool("Active",false);
    }

    private void OnDisable()
    {
        GetComponent<Animator>().Play("normalState",0,0f);
    }

    void Start()
    {
        _back = this.gameObject.transform.GetChild(0).gameObject;
        _front = this.gameObject.transform.GetChild(1).gameObject;
        _text = this.gameObject.transform.GetChild(2).gameObject;

        GetComponent<Animator>().keepAnimatorControllerStateOnDisable = true;
    }

    public override void Active()
    {
        gameObject.GetComponent<Animator>().SetBool("Active",true);
        ActiveMenuButton();
    }
    public override void Deactive()
    {
        gameObject.GetComponent<Animator>().SetBool("Active",false);
        DeactiveMenuButton();
    }
    
    
    
    void Update()
    {
        if(active)
            BackImage.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, 1));
    }

    public void ActiveMenuButton()
    {
        BackImage.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, 1));

        if(_tempPopValue < maxPopValue1 && animationPhase1 == false)
        {
            _front.transform.position += new Vector3(-1 * PopSpeed1 , PopSpeed1 ,0);
            _text.transform.position += new Vector3(-1 * PopSpeed1 , PopSpeed1 ,0);
            _tempPopValue += PopSpeed1;
        }
        else
        {
            animationPhase1 = true;
        }

        if(_tempPopValue > maxPopValue2 && animationPhase1)
        {
            _front.transform.position += new Vector3(PopSpeed2 , -1 * PopSpeed2 ,0);
            _text.transform.position += new Vector3(PopSpeed2 , -1 * PopSpeed2 ,0);
            _tempPopValue -= PopSpeed2;
        }
        active = true;

        if(!swapSoundPlayed)
        {
            SoundManager.Instance.PlayButtonSwap();
            swapSoundPlayed = true;
        }
    }

    public void DeactiveMenuButton()
    {
        _back.GetComponent<Image>().color = Color.white;

        _front.transform.position += new Vector3(_tempPopValue , -1 * _tempPopValue ,0);
        _text.transform.position += new Vector3(_tempPopValue , -1 * _tempPopValue ,0);
        _tempPopValue = 0;

        swapSoundPlayed = false;
        animationPhase1 = false;
        active = false;
    }
}
