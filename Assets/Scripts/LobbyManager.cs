using Photon.Pun; // ����Ƽ�� ���� ������Ʈ��
using Photon.Realtime; // ���� ���� ���� ���̺귯��
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;
using Photon.Pun.Demo.PunBasics;
using UnityEngine.SocialPlatforms.Impl;

// ������(��ġ ����ŷ) ������ �� ������ ���
public class LobbyManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1"; // ���� ����

    public Text connectionInfoText; // ��Ʈ��ũ ������ ǥ���� �ؽ�Ʈ
    public Button joinButton; // �� ���� ��ư

    public static int m_ImpostorCount = 1;

    public Text m_CitizenCountUI;

    LobbySceneManager m_LobbySceneManager;

    [SerializeField]
    private Button m_ImpostorButton;


    // ���� ����� ���ÿ� ������ ���� ���� �õ�
    private void Start()
    {
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();

        joinButton.interactable = false;
        connectionInfoText.text = "1.������ ������ ���� ��...";

       // m_LobbySceneManager = new LobbySceneManager();
    }

    // ������ ���� ���� ������ �ڵ� ����
    public override void OnConnectedToMaster()
    {
        // PublicScene->LobbyScene
        joinButton.interactable = true;

        connectionInfoText.text = "2.�¶��� : ������ ������ �����";

      // Debug.Log(PhotonNetwork.Room.Name);
    }

    // ������ ���� ���� ���н� �ڵ� ����
    public override void OnDisconnected(DisconnectCause cause)
    {
        joinButton.interactable = false;

        connectionInfoText.text = "�������� : ������ ������ ������� ����\n���� ��õ� ��...";

        PhotonNetwork.ConnectUsingSettings();

    }

    // �� ���� �õ�
    public void Connect()
    {
        joinButton.interactable = false;

        if (PhotonNetwork.IsConnected)
        {
            connectionInfoText.text = "3.�뿡 ����...";
            PhotonNetwork.JoinRandomRoom();
            //PhotonNetwork.LoadLevel("LobbyScene");
        }

        else
        {
            connectionInfoText.text = "�������� : ������ ������ ������� ����\n���� ��õ� ��...";

            PhotonNetwork.ConnectUsingSettings();
        }

    }

    // (�� ���� ����)���� �� ������ ������ ��� �ڵ� ����
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        connectionInfoText.text = "�� ���� ����, ���ο� �� ����...";

        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 6 });
    }

    // �뿡 ���� �Ϸ�� ��� �ڵ� ����
    public override void OnJoinedRoom()
    {
        // LobbyScene->Game
        connectionInfoText.text = "4. �� ���� ����";

        PhotonNetwork.LoadLevel("LobbyScene");

        //PhotonNetwork.Instantiate("Player", new Vector3(0, 0, 0), Quaternion.identity, 0); // B

        //PhotonNetwork.Instantiate("Player", new Vector3(0, 0, 0), Quaternion.identity, 0); // B

        //StartCoroutine(CreatePlayer());

        //StartCoroutine(this.CreatePlayer());
    }

    //    void CreatePlayer()
    //    {
    ////        PhotonNetwork.Instantiate("Player", new Vector3(0, 1, 0), Quaternion.identity, 0);
    //        PhotonNetwork.Instantiate("Player", new Vector3(0, 0, 0), Quaternion.identity).GetComponent<PlayerController>(); // B
    //    }

    //IEnumerator CreatePlayer()
    //{
    //    Debug.Log("�޷�");

    //    //PhotonNetwork.Instantiate("Player", new Vector3(0, 1, 0), Quaternion.identity, 0);
    //    yield return null;
    //}

    private void Update()
    {
        //m_CitizenCountUI.text = m_LobbySceneManager.m_PlayerCount.ToString();
    }

}