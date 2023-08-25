using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // ���콺 �巡�� ���� �ʿ�.
using UnityEngine.UI;


public class Drag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public static Vector2 m_DefaultPos;

    public LeverUptoDown Lever;
    public Sprite m_DownSprite;
    public Sprite m_EmptySprite;
    
    public bool m_EmptyOn;

    public Slider m_MissionProgressBar;

    Image m_Sprite;

    public bool GetEmptyOn() 
    { 
        return m_EmptyOn; 
    }

    void Start()
    {
        m_EmptyOn = false;

    }

    void Update()
    {
        if (!m_EmptyOn)
        {
            if (this.transform.position.y <= 410f)
            {
                Lever.GetComponent<Image>().sprite = m_DownSprite; // �巡�� �� â�� y���� 410���� ������ �����ٿ� �̹����� ��ü.        
                StartCoroutine(WaitForIt());
            }
        }
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        m_DefaultPos = this.transform.position; // Ŭ�� �� ���� ��ġ�� m_DefaultPos �� ���Ѵ�.
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector2 CurrentPos = eventData.position; // �巡�� ���� ��ġ�� ���� ������Ʈ�� ��ġ�� ���Ѵ�.
        this.transform.position = CurrentPos;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    IEnumerator WaitForIt()
    {
        m_EmptyOn = true;

        yield return new WaitForSeconds(2.0f);
        Lever.GetComponent<Image>().sprite = m_EmptySprite;

        yield return new WaitForSeconds(2.0f);
        m_MissionProgressBar.value += 25f;
    }

}
