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

    // 버튼의 이미지를 할당할 변수
    //private Image buttonImage;
    //// 마우스가 올라갔을 때의 스프라이트
    //public Sprite hoverSprite;
    //// 버튼의 기본 스프라이트
    //private Sprite defaultSprite;

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

    //// 버튼이 활성화될 때 실행되는 함수
    //void Start()
    //{
    //    // 버튼의 Image 컴포넌트 가져오기
    //    buttonImage = GetComponent<Image>();
    //    // 기본 스프라이트 설정
    //    defaultSprite = buttonImage.sprite;
    //}

    //// 버튼에 마우스가 들어왔을 때 호출될 함수
    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    // 버튼의 스프라이트를 hoverSprite로 변경
    //    buttonImage.sprite = hoverSprite;
    //}

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
                Debug.Log("종료");
                //UnityEditor.EditorApplication.isPlaying = false;
                Application.Quit();
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
        //buttonImage.sprite = defaultSprite;
    }
}
