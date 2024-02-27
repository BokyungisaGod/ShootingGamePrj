using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Letsgo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // 버튼의 이미지 컴포넌트를 할당할 변수
    private Image buttonImage;
    // 시작 버튼 스프라이트
    public Sprite startSprite;
    // 호버 스프라이트
    public Sprite hoverSprite;


    void Start()
    {
        // 버튼의 Image 컴포넌트 가져오기
        buttonImage = GetComponent<Image>();
        // 처음에는 시작 스프라이트로 설정
        buttonImage.sprite = startSprite;
    }

    // 마우스가 버튼 위에 올라갔을 때 호출될 함수
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 버튼의 스프라이트를 hoverSprite로 변경
        buttonImage.sprite = hoverSprite;
    }

    // 마우스가 버튼에서 나갔을 때 호출될 함수
    public void OnPointerExit(PointerEventData eventData)
    {
        // 버튼의 스프라이트를 startSprite로 변경
        buttonImage.sprite = startSprite;
    }

    // 게임 시작 함수
    public void StartGame()
    {
        SceneManager.LoadScene("main");
    }
}
