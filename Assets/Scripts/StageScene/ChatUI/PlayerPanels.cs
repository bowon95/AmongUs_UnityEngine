using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanels : MonoBehaviour
{
    public GameObject m_NickNameUI;
    public GameObject m_PlayerPanel;
    public GameObject m_voteIconImg;
    public GameObject m_PlayerIcon;



    int m_CountPlayer = 0; // �÷��̾� �� 
    List<GameObject>m_voteIcon = new List<GameObject>(); // ������ ����Ʈ. select, cancel Ű�� ���� �ٸ��� ��� ����.
    //Text m_NickName = m_NickNameUI; // �г��� �޾ƿ��� ��
    bool m_VoteEnd = false;

    


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
