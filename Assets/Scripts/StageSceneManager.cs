using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Realtime;
using TMPro;
using Photon.Pun.UtilityScripts;
using static UnityEngine.Rendering.DebugUI;

public class StageSceneManager : MonoBehaviourPunCallbacks
{
    // �ܺο��� �̱��� ������Ʈ�� �����ö� ����� ������Ƽ
    public static StageSceneManager instance
    {
        get
        {
            // ���� �̱��� ������ ���� ������Ʈ�� �Ҵ���� �ʾҴٸ�
            if (m_instance == null)
            {
                // ������ GameManager ������Ʈ�� ã�� �Ҵ�
                m_instance = FindObjectOfType<StageSceneManager>();
            }

            // �̱��� ������Ʈ�� ��ȯ
            return m_instance;
        }
    }

    private static StageSceneManager m_instance; // �̱����� �Ҵ�� static ����

    // �ش� ĳ���� ������ �뵵
    // public GameObject m_Crew;
    // public bool m_SpawnButton = false;
    // public Collider2D m_Table;

    public GameObject m_ImpostorScene;
    public GameObject m_CrewScene;
    public Image m_BlackScene;

    public bool m_SpawnButton = false;
    public Collider2D m_Table;

    public GameObject m_Impo;
    public GameObject m_Crew;

    private int m_ImpoKey;

    private int score = 0; // ���� ���� ����
    public bool isGameover { get; private set; } // ���� ���� ����

    private void Awake()
    {
        // ���� �̱��� ������Ʈ�� �� �ٸ� GameManager ������Ʈ�� �ִٸ�
        if (instance != this)
        {
            // �ڽ��� �ı�
            Destroy(gameObject);
        }

        m_ImpoKey = GameManager.FindAnyObjectByType<GameManager>().m_Random;
    }

    private void Start()
    {
        StartCoroutine("FadeIn");

        if (PlayerPrefs.HasKey("Impostor" + m_ImpoKey.ToString()))
        {
            StartCoroutine("ImpostorScene");
        }

        else
        {
            StartCoroutine("CrewScene");
        }

        // m_Crew = FindObjectOfType<GameManager>().playerPrefab;
        //m_CrewPhotonView.StartCoroutine("CrewScene");

        //if (PhotonNetwork.LocalPlayer)
        //{
        //    StartCoroutine("CrewScene");
        //}

        //if (m_Crew.GetPhotonView().IsMine)
        //{
        //    StartCoroutine("CrewScene");
        //}

        //else if (m_Impo.GetPhotonView().IsMine)
        //{
        //    StartCoroutine("ImpostorScene");
        //}
    }

    public void EndGame()
    {
        // ���� ���� ���¸� ������ ����
        isGameover = true;
        // ���� ���� UI�� Ȱ��ȭ
    }

    // Ű���� �Է��� �����ϰ� ���� ������ ��
    private void Update()
    {
        if (m_SpawnButton)
        {
            //photonView.RPC("SpawnPlayer", RpcTarget.All);
            SpawnPlayer();
        }
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
    public void SpawnPlayer()
    {
        //playerPrefab.transform.position = new Vector2(m_Table.transform.position.x, m_Table.transform.position.y);//new Vector2(Mathf.Cos(30f) * 0.002f + m_Table.transform.position.x, Mathf.Sin(30f) * 0.002f + m_Table.transform.position.y);
        //m_Crew.transform.position = new Vector2(m_Table.transform.position.x, m_Table.transform.position.y);

        for (int i = 0; i < PhotonNetwork.PlayerList.Length; ++i)
        {

            if (PhotonNetwork.LocalPlayer == PhotonNetwork.PlayerList[i])
            {
                if (PlayerPrefs.HasKey("Impostor1"))
                {
                    GameObject Impo = GameObject.FindGameObjectWithTag("Impostor");
                    Impo.transform.position = new Vector3(Mathf.Cos(30f * i) * 0.001f + m_Table.transform.position.x, Mathf.Sin(30f * i) * 0.001f + m_Table.transform.position.y);
                }

                else
                {
                    GameObject Crew = GameObject.FindGameObjectWithTag("Player");
                    Crew.transform.position = new Vector2(Mathf.Cos(30f * i) * 0.001f + m_Table.transform.position.x, Mathf.Sin(30f * i) * 0.001f + m_Table.transform.position.y);
                }
            }
        }

        m_SpawnButton = false;
    }

    public void Spawn()
    {
        m_SpawnButton = true;
    }
}
