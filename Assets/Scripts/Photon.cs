using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonTest : MonoBehaviourPunCallbacks
{
    void Start()
    {
        Screen.SetResolution(1024, 768, false); // PC ���� �� �ػ� ����
        PhotonNetwork.ConnectUsingSettings(); // ���� ���ἳ��
    }

    public override void OnConnectedToMaster()
    {
        RoomOptions options = new RoomOptions(); // ��ɼǼ���
        options.MaxPlayers = 6; // �ִ��ο� ����
        PhotonNetwork.JoinOrCreateRoom("Room1", options, null); // ���� ������ �����ϰ� 
                                                                // ���ٸ� ���� ����� �����մϴ�.
    }

}