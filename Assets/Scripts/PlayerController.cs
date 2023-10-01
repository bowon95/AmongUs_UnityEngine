using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviourPunCallbacks
{
    public PhotonView m_PhotonView;

    public float m_MoveSpeed = 4.0f;

    public Text m_Nick;

    Vector2 m_PlayerMove = new Vector2();

    Rigidbody2D    m_Rigidbody;
    Animator       m_Animator;
    SpriteRenderer m_Sprite;

    // ��������
    bool m_Dead;

    [PunRPC]
    void Start()
    {
        if (m_PhotonView.IsMine)
        {
            m_Nick.text = PhotonNetwork.LocalPlayer.NickName;
        }

        else
        {
            m_Nick.text = m_PhotonView.Owner.NickName;
        }

        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        m_Sprite = GetComponent<SpriteRenderer>();

        //m_Nick.GetComponent<Text>().text = PhotonNetwork.LocalPlayer.NickName;
        //m_Nick.GetComponent<Text>().text = PlayerPrefs.GetString("NickName");
        //m_PhotonView.RPC("SetNIckname", RpcTarget.AllBuffered);


        // null reference
        //m_Dead = GetComponent<ImpostorController>().GetDead();

        float m_HoritontalInput = Input.GetAxisRaw("Horizontal");
        float m_VerticalInput = Input.GetAxisRaw("Vertical");
    }

    void Update() 
    {
        if (!m_PhotonView.IsMine)
            return;

        //if(m_IsDead) 
        //    return;

        if (m_Dead == true)
        {
            m_Animator.SetBool("Dead", true);
        }

        if (m_Animator.GetBool("Dead"))
        {
            m_MoveSpeed = 0f;
            m_PlayerMove = new Vector2(0f, 0f);
            
        }

        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
            m_Animator.SetBool("Move", false); // �ִϸ����� �Ķ���Ϳ� ����.

        else
        {
            m_Animator.SetBool("Move", true);

            // ���� üũ.
            if (Input.GetAxisRaw("Horizontal") == -1)
                m_Sprite.flipX = true;

            else
                m_Sprite.flipX = false;
        }

        m_Nick.transform.position = Camera.main.WorldToScreenPoint(new Vector3(this.transform.position.x + 0.8f, this.transform.position.y + 0.02f));
    }

    private void FixedUpdate() // ��Ģ���� ȣ��� ���� ��� � ����.
    {
        if (!m_PhotonView.IsMine)
            return;

        Move();
    }

    [PunRPC]
    void Move()
    {
        m_PlayerMove.x = Input.GetAxisRaw("Horizontal");

        m_PlayerMove.y = Input.GetAxisRaw("Vertical");

        m_PlayerMove.Normalize(); // �밢�� ���� �̵� �� �ӵ� �������� �ʵ���.

        m_Rigidbody.velocity = m_PlayerMove * m_MoveSpeed;

    }

    [PunRPC]
    void SetNIckname()
    {
        m_Nick.GetComponent<Text>().text = PlayerPrefs.GetString("NickName");

    }
}
