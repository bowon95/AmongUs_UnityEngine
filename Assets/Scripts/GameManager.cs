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

    public bool m_SpawnButton = false;

    public Collider2D m_Table;

    // m_NickNameUI
    //public GameObject m_NickNameUI; //m_NickNameUI


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

        int Impo = 0;//Random.Range(0, PhotonNetwork.PlayerList.Length - 1);

        for (int i = 0; i < PhotonNetwork.PlayerList.Length; ++i)
        {
            if (PhotonNetwork.LocalPlayer == PhotonNetwork.PlayerList[i])
            {

                // m_NickNameUI
               // PhotonNetwork.LocalPlayer.NickName = PlayerPrefs.GetString("NickName");

                // m_NickNameUI
                //GameObject M = Instantiate(m_NickNameUI, Vector3.zero, Quaternion.identity);

                if (i == Impo)
                {
                    //StartCoroutine("FadeIn");

                    //StartCoroutine("ImpostorScene");

                    m_Impo = PhotonNetwork.Instantiate(ImpostorPrefab.name, randomSpawnPos, Quaternion.identity);
                    PlayerPrefs.SetString("Impostor", "1");
                }

                else
                {
                    //StartCoroutine("FadeIn");

                    //StartCoroutine("CrewScene");

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

        if (m_SpawnButton)
        {
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; ++i)
            {
                m_PlayerList[i].Get<PhotonView>().RPC("SpawnPlayer", RpcTarget.All);
            }
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

    IEnumerator ImpostorScene()
    {
        m_ImpostorScene.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        StartCoroutine("FadeOut");

    }

    IEnumerator CrewScene()
    {
        m_CrewScene.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        StartCoroutine("FadeOut");
    }

    IEnumerator FadeIn()
    {
        m_BlackScene.gameObject.SetActive(true);

        m_BlackScene.color = new Color(0, 0, 0, 1);

        float FadeCount = 1;

        while (FadeCount > 0f)
        {
            FadeCount -= 0.01f;

            yield return new WaitForSeconds(0.01f);

            m_BlackScene.color = new Color(0, 0, 0, FadeCount);
        }
    }

    IEnumerator FadeOut()
    {
        m_BlackScene.color = new Color(0, 0, 0, 0);

        float FadeCount = 0;

        while (FadeCount <= 1f)
        {
            FadeCount += 0.01f;

            yield return new WaitForSeconds(0.01f);

            m_BlackScene.color = new Color(0, 0, 0, FadeCount);
        }

        if (FadeCount >= 1f)
        {
            m_BlackScene.gameObject.SetActive(false);
            m_ImpostorScene.gameObject.SetActive(false);
            m_CrewScene.gameObject.SetActive(false);
        }
    }


    // �÷��̾� ������ ��ġ
    // (cos(�÷��̾� ����Ʈ/360) * ������ + ������������ ��Ź���� x, sin(�÷��̾� ����Ʈ/360)*������ ������������ ��Ź���� y)
    [PunRPC]
    public void SpawnPlayer()
    {
        //playerPrefab.transform.position = new Vector2(m_Table.transform.position.x, m_Table.transform.position.y);//new Vector2(Mathf.Cos(30f) * 0.002f + m_Table.transform.position.x, Mathf.Sin(30f) * 0.002f + m_Table.transform.position.y);
        m_Crew.transform.position = new Vector2(m_Table.transform.position.x, m_Table.transform.position.y);
        

        m_SpawnButton = false;
    }

    public void Spawn()
    {
        m_SpawnButton = true;
    }

}