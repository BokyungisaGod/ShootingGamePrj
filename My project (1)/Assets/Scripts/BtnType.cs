using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static mainManager;
using UnityEngine.SceneManagement;

public class BtnType : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    public BTNType curBtnType;
    public Transform buttonScale;
    public CanvasGroup mainGroup;
    public CanvasGroup stageGroup;
    public CanvasGroup soundGroup;
    AudioManager audiomanager;
    public int stage;
    Vector3 defaultScale;   
    bool isSound;
    public void Awake()
    {
        audiomanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        defaultScale = buttonScale.localScale;
        Debug.Log(GameManager.start);
        if (GameManager.start == false)
        {
            PlayerPrefs.SetInt("Clearstage", 0);
        }
        Debug.Log(PlayerPrefs.GetInt("Clearstage"));
        GameManager.start = true;
        if(stage>PlayerPrefs.GetInt("Clearstage")+1) 
        {
            gameObject.SetActive(false);
        }
        else
            gameObject.SetActive(true);
    }
    public void OnBtnClick() 
    {
        //SoundManager.instance.SFXPlay("Click", clip);
        audiomanager.PlaySFX(audiomanager.click);
        switch (curBtnType) 
        {
            case BTNType.New:
                PlayerPrefs.SetInt("stage", 1);
                PlayerPrefs.SetInt("Clearstage", 0);
                SceneManager.LoadScene("Select");
                break;
            case BTNType.Continue:
                CanvasGroupOn(stageGroup);
                CanvasGroupOff(mainGroup);
                break;
            case BTNType.Option:
                CanvasGroupOn(soundGroup);
                CanvasGroupOff(mainGroup);
                break;
            case BTNType.Quit:
                Debug.Log("Á¾·á");
                UnityEditor.EditorApplication.isPlaying = false;
                //Application.Quit();
                break;
            case BTNType.Back:
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(stageGroup);
                CanvasGroupOff(soundGroup);
                break;
            case BTNType.Stage1:
                PlayerPrefs.SetInt("stage", 1);
                SceneManager.LoadScene("Select");
                break;
            case BTNType.Stage2:
                PlayerPrefs.SetInt("stage", 2);
                SceneManager.LoadScene("Select");
                break;
            case BTNType.Stage3:
                PlayerPrefs.SetInt("stage", 3);
                SceneManager.LoadScene("Select");
                break;
            case BTNType.C1:
                PlayerPrefs.SetInt("Character", 1);
                SceneManager.LoadScene("Loading");
                break;
            case BTNType.C2:
                PlayerPrefs.SetInt("Character", 2);
                SceneManager.LoadScene("Loading");
                break;

        }
    }
    
    public void CanvasGroupOn(CanvasGroup cg) 
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }
    public void CanvasGroupOff(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {

        buttonScale.localScale = defaultScale * 1.2f;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale; 
    }
}
