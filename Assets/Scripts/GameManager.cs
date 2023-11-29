using NUnit.Framework;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using Photon.Realtime;
using ExitGames.Client.Photon.StructWrapping;

// ������ ���� ���� ����, ���� UI�� �����ϴ� ���� �Ŵ���
public class GameManager : MonoBehaviourPunCallbacks
{
    // �ܺο��� �̱��� ������Ʈ�� �����ö� ����� ������Ƽ
    public static GameManager instance
    {
        get
        {
            // ���� �̱��� ������ ���� ������Ʈ�� �Ҵ���� �ʾҴٸ�
            if (m_instance == null)
            {
                // ������ GameManager ������Ʈ�� ã�� �Ҵ�
                m_instance = FindObjectOfType<GameManager>();
            }

            // �̱��� ������Ʈ�� ��ȯ
            return m_instance;
        }
    }

    private static GameManager m_instance; // �̱����� �Ҵ�� static ����

    public GameObject playerPrefab; // ������ �÷��̾� ĳ���� ������
    public GameObject ImpostorPrefab;

    // 1021
    private GameObject m_Crew;
    private GameObject m_Impo;


    public GameObject m_ImpostorScene;
    public GameObject m_CrewScene;
    public Image m_BlackScene;

    // m_NickNameUI
    //public GameObject m_NickNameUI; //m_NickNameUI

    public int m_Random;


    //0907
    //public PhotonView m_PhotonView; 
    List<int> m_PlayerList = new List<int>();

    private int ImpostorCount = 0;
    private int ImpostorNum = 1;

    private int score = 0; // ���� ���� ����
    public bool isGameover { get; private set; } // ���� ���� ����

    // �ֱ������� �ڵ� ����Ǵ�, ����ȭ �޼���
    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //     ���� ������Ʈ��� ���� �κ��� �����
    //    if (stream.IsWriting)
    //    {
    //         ��Ʈ��ũ�� ���� score ���� ������
    //        stream.SendNext(score);
    //    }
    //    else
    //    {
    //         ����Ʈ ������Ʈ��� �б� �κ��� �����         

    //         ��Ʈ��ũ�� ���� score �� �ޱ�
    //        score = (int)stream.ReceiveNext();
    //         ����ȭ�Ͽ� ���� ������ UI�� ǥ��
    //        UIManager.instance.UpdateScoreText(score);
    //    }
    //}

    private void Awake()
    {
        // ���� �̱��� ������Ʈ�� �� �ٸ� GameManager ������Ʈ�� �ִٸ�
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

    // ���� ���۰� ���ÿ� �÷��̾ �� ���� ������Ʈ�� ����
    private void Start()
    {
        // ������ ���� ��ġ ����
        Vector3 randomSpawnPos = Random.insideUnitSphere * 5f;

        //Vector3 SpawnTablePos = new Vector3(Mathf.Cos(30f) * 0.002f + m_Table.transform.position.x, Mathf.Sin(30f) * 0.002f + m_Table.transform.position.y);

        // ��ġ y���� 0���� ����
        randomSpawnPos.y = 0f;

        int Impo = Random.Range(0, PhotonNetwork.PlayerList.Length - 1);

        for (int i = 0; i < PhotonNetwork.PlayerList.Length; ++i)
        {
            if (PhotonNetwork.LocalPlayer == PhotonNetwork.PlayerList[i])
            {
                if (i == Impo)
                {
                    m_Impo = PhotonNetwork.Instantiate(ImpostorPrefab.name, randomSpawnPos, Quaternion.identity);

                    m_Random = Random.Range(0, 1000);

                    PlayerPrefs.SetString("Impostor" + m_Random.ToString(), "1");
                }

                else
                {
                    m_Crew = PhotonNetwork.Instantiate(playerPrefab.name, randomSpawnPos, Quaternion.identity);
                }
            }
        }

    }


    // ���� ���� ó��
    public void EndGame()
    {
        // ���� ���� ���¸� ������ ����
        isGameover = true;
        // ���� ���� UI�� Ȱ��ȭ
    }

    // Ű���� �Է��� �����ϰ� ���� ������ ��
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PhotonNetwork.LeaveRoom();
        }
    }

    public void LoadScene()
    {
        PhotonNetwork.LoadLevel("Stage");
    }

    // ���� ������ �ڵ� ����Ǵ� �޼���
    public override void OnLeftRoom()
    {
        // ���� ������ �κ� ������ ���ư�
        SceneManager.LoadScene("LobbyScene");
    }
}