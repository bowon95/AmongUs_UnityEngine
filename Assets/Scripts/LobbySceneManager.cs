using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbySceneManager : MonoBehaviourPunCallbacks
{
    public PhotonView m_PlayerPhotonView;

    // �ܺο��� �̱��� ������Ʈ�� �����ö� ����� ������Ƽ
    public static LobbySceneManager instance
    {
        get
        {
            // ���� �̱��� ������ ���� ������Ʈ�� �Ҵ���� �ʾҴٸ�
            if (m_instance == null)
            {
                // ������ GameManager ������Ʈ�� ã�� �Ҵ�
                m_instance = FindObjectOfType<LobbySceneManager>();
            }

            // �̱��� ������Ʈ�� ��ȯ
            return m_instance;
        }
    }

    private static LobbySceneManager m_instance; // �̱����� �Ҵ�� static ����

    public GameObject playerPrefab; // ������ �÷��̾� ĳ���� ������

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

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }
    }

    // ���� ���۰� ���ÿ� �÷��̾ �� ���� ������Ʈ�� ����
    private void Start()
    {
        // ������ ���� ��ġ ����
        Vector3 randomSpawnPos = new Vector3(0, 0, 0);

        PhotonNetwork.Instantiate(playerPrefab.name, randomSpawnPos, Quaternion.identity);

        m_PlayerPhotonView.RPC("SetPlayerRandomColor", RpcTarget.All);
       
        //playerPrefab.GetComponent<SpriteRenderer>().material.SetColor("_PlayerColor", PlayerColor.GetColor((EPlayerColor)Random.Range(0, 12)));
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
    }

    public override void OnJoinedRoom()
    {
         PhotonNetwork.LoadLevel("Stage");
    }

    // ���� ������ �ڵ� ����Ǵ� �޼���
    public override void OnLeftRoom()
    {
        // ���� ������ �κ� ������ ���ư�
        SceneManager.LoadScene("LobbyScene");
    }

    [PunRPC]
    void SetPlayerRandomColor()
    {
        playerPrefab.GetComponent<SpriteRenderer>().material.SetColor("_PlayerColor", PlayerColor.GetColor((EPlayerColor)Random.Range(0, 12)));
    }

}
