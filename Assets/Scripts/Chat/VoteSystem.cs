using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoteSystem : MonoBehaviour
{
    [SerializeField]
    private Image m_CharacterImg; // ĳ���� �̹��� ���� ����

    [SerializeField]
    private Text m_NicknameText; // �г��� �����

    [SerializeField]
    private GameObject m_DeadPlayerBlock; // ���� �÷��̾� ǥ�ÿ�

    // [SerializeField]
    // private GameObject m_ReportSign; // �Ű��� ǥ�ÿ�

    // public IngameCharacterMover m_TargetPlayer; // � �÷��̾��� �г����� ���� �뵵.

    //public void SetPlayer(IngameCharacterMover Target)
    //{
    //    Material inst = Instantiate(m_CharacterImg.material)
    //    m_CharacterImg.material = inst;

    //    m_TargetPlayer = Target;
    //    m_CharacterImg.material.SetColor("m_PlayerColor", PlayerColor, GetColor(m_CharacterImg, PlayerColor));
    // m_NicknameText.text = Target.nickname;

    // �������Ͱ� �ٸ� �������� �� �� ���������� ǥ�õǵ���. 
    //}

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
