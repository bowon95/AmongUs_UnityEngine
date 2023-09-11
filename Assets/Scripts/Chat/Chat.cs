using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Chat : MonoBehaviour
{
    public InputField m_InputField; // ���� �޽��� �Է��ϴ� ��.
    public GameObject m_Message;
    public GameObject m_Content; // ä��â�� ������� ��ǳ��.

    public void SendMessage()
    {
        GetComponent<PhotonView>().RPC("GetMessage", RpcTarget.All, m_InputField.text); // RpcTarget.All �� ��ο��� ���̰� �ϴ� ��. 
    }

    [PunRPC]
    public void GetMessage(string ReceiveMessage)
    { 
        GameObject M = Instantiate(m_Message, Vector3.zero, Quaternion.identity, m_Content.transform);
        M.GetComponent<Message>().m_MyMessage.text = ReceiveMessage;

    }

}