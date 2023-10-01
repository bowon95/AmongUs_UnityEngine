using NUnit.Framework;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

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

    // m_NickNameUI
    public GameObject m_NickNameUI; //m_NickNameUI


    //0907
    public PhotonView m_PhotonView; 
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
    }

    // ���� ���۰� ���ÿ� �÷��̾ �� ���� ������Ʈ�� ����
    private void Start()
    {
        // ������ ���� ��ġ ����
        Vector3 randomSpawnPos = Random.insideUnitSphere * 5f;
        // ��ġ y���� 0���� ����
        randomSpawnPos.y = 0f;

        int Impo = Random.Range(0, PhotonNetwork.PlayerList.Length - 1);

        for (int i = 0; i < PhotonNetwork.PlayerList.Length; ++i)
        {
            if (PhotonNetwork.LocalPlayer == PhotonNetwork.PlayerList[i])
            {

                // m_NickNameUI
                PhotonNetwork.LocalPlayer.NickName = PlayerPrefs.GetString("NickName");

                // m_NickNameUI
                GameObject M = Instantiate(m_NickNameUI, Vector3.zero, Quaternion.identity);

                if (i == Impo)
                {
                    PhotonNetwork.Instantiate(ImpostorPrefab.name, randomSpawnPos, Quaternion.identity);
                }

                else
                {
                    PhotonNetwork.Instantiate(playerPrefab.name, randomSpawnPos, Quaternion.identity);
                }
            }
        }

    }

    // ������ �߰��ϰ� UI ����
    //public void AddScore(int newScore)
    //{
    //    // ���� ������ �ƴ� ���¿����� ���� ���� ����
    //    if (!isGameover)
    //    {
    //        // ���� �߰�
    //        score += newScore;
    //        // ���� UI �ؽ�Ʈ ����
    //        UIManager.instance.UpdateScoreText(score);
    //    }
    //}

    // ���� ���� ó��
    public void EndGame()
    {
        // ���� ���� ���¸� ������ ����
        isGameover = true;
        // ���� ���� UI�� Ȱ��ȭ
        //UIManager.instance.SetActiveGameoverUI(true);
    }

    // Ű���� �Է��� �����ϰ� ���� ������ ��
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PhotonNetwork.LeaveRoom();
        }

        // 0907

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

    //public void SelectImpostor()
    //{
    //    //Destroy(gameObject);

    //    // ������ ���� ��ġ ����
    //    Vector3 randomSpawnPos = Random.insideUnitSphere * 5f;
    //    // ��ġ y���� 0���� ����
    //    randomSpawnPos.y = 0f;

    //    PhotonNetwork.Instantiate(playerPrefab.name, randomSpawnPos, Quaternion.identity);
    //}

}