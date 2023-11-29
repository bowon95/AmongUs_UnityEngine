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
public class NetworkManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1"; // ���� ����

    public Text connectionInfoText; // ��Ʈ��ũ ������ ǥ���� �ؽ�Ʈ
    public Button joinButton; // �� ���� ��ư

    public static int m_ImpostorCount = 1;

    public Text m_CitizenCountUI;

    [SerializeField]
    private Button m_ImpostorButton;


    // �ܺο��� �̱��� ������Ʈ�� �����ö� ����� ������Ƽ
    public static NetworkManager instance
    {
        get
        {
            // ���� �̱��� ������ ���� ������Ʈ�� �Ҵ���� �ʾҴٸ�
            if (m_instance == null)
            {
                // ������ NetworkManager ������Ʈ�� ã�� �Ҵ�
                m_instance = FindObjectOfType<NetworkManager>();
            }

            // �̱��� ������Ʈ�� ��ȯ
            return m_instance;
        }
    }

    private static NetworkManager m_instance; // �̱����� �Ҵ�� static ����

    private void Awake()
    {
        // ���� �̱��� ������Ʈ�� �� �ٸ� NetworkManager ������Ʈ�� �ִٸ�
        if (instance != this)
        {
            // �ڽ��� �ı�
            Destroy(gameObject);
        }

        else
        {
            DontDestroyOnLoad(this);
        }
    }

    // ���� ����� ���ÿ� ������ ���� ���� �õ�
    private void Start()
    {
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();

        joinButton.interactable = false;
        connectionInfoText.text = "1.������ ������ ���� ��...";
    }

    // ������ ���� ���� ������ �ڵ� ����
    public override void OnConnectedToMaster()
    {
        // PublicScene->LobbyScene
        joinButton.interactable = true;

        connectionInfoText.text = "2.�¶��� : ������ ������ �����";
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
    }

    private void Update()
    {
        //m_CitizenCountUI.text = m_LobbySceneManager.m_PlayerCount.ToString();
    }

}