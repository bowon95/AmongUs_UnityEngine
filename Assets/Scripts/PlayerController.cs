using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float m_MoveSpeed = 4.0f;
    private bool m_IsDead = false;

    float m_HoritontalInput = Input.GetAxisRaw("Horizontal");
    float m_VerticalInput = Input.GetAxisRaw("Vertical");

    Vector2 m_PlayerMove = new Vector2();

    Rigidbody2D m_Rigidbody;
    Animator m_Animator;


    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
    }

    void Update() 
    {
        if(m_IsDead) 
            return;

        FlipCheck(); // ���� �ٲ� �� ��������Ʈ ������.
    }

    private void FixedUpdate() // ��Ģ���� ȣ��� ���� ��� � ����.
    {
        if (m_IsDead)
            return;

        Move();

        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
            m_Animator.SetBool("Move", false);

        else
            m_Animator.SetBool("Move", true);
    }

    void Move()
    {
        m_PlayerMove.x = Input.GetAxisRaw("Horizontal");

        m_PlayerMove.y = Input.GetAxisRaw("Vertical");

        m_PlayerMove.Normalize(); // �밢�� ���� �̵� �� �ӵ� �������� �ʵ���.

        m_Rigidbody.velocity = m_PlayerMove * m_MoveSpeed;
    }

    private void FlipCheck()
    {
        if (m_HoritontalInput < 0)
        {
            transform.localScale = new Vector3(-0.2f, 0.2f, 0.2f); // ���� �̵��� ��������Ʈ ���� ����.
        }

        if (m_VerticalInput > 0)
        {
            transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }
    }

    private void Die()
    {
        m_Animator.SetTrigger("Dead");
        m_IsDead = true;
    }
}
