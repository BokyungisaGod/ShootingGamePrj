using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Letsgo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // ��ư�� �̹��� ������Ʈ�� �Ҵ��� ����
    private Image buttonImage;
    // ���� ��ư ��������Ʈ
    public Sprite startSprite;
    // ȣ�� ��������Ʈ
    public Sprite hoverSprite;


    void Start()
    {
        // ��ư�� Image ������Ʈ ��������
        buttonImage = GetComponent<Image>();
        // ó������ ���� ��������Ʈ�� ����
        buttonImage.sprite = startSprite;
    }

    // ���콺�� ��ư ���� �ö��� �� ȣ��� �Լ�
    public void OnPointerEnter(PointerEventData eventData)
    {
        // ��ư�� ��������Ʈ�� hoverSprite�� ����
        buttonImage.sprite = hoverSprite;
    }

    // ���콺�� ��ư���� ������ �� ȣ��� �Լ�
    public void OnPointerExit(PointerEventData eventData)
    {
        // ��ư�� ��������Ʈ�� startSprite�� ����
        buttonImage.sprite = startSprite;
    }

    // ���� ���� �Լ�
    public void StartGame()
    {
        SceneManager.LoadScene("main");
    }
}
