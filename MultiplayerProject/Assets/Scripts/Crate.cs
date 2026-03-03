using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Crate : MonoBehaviour
{
    private Rigidbody2D rb;
    public PlayerMovement m_player;
    [SerializeField] private float crateSpeed = 3f;

  
    public Vector3 playerPos;
    public Vector3 moveDirection;

    public enum eState : int
    {
        kIdle,
        kMoving
    }

    public eState m_nState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_nState = eState.kIdle;
        rb = GetComponent<Rigidbody2D>();
        //m_player = GameObject.FindObjectOfType(typeof(PlayerMovement)) as PlayerMovement;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //playerPos = m_player.transform.position;
        //moveDirection = (transform.position - playerPos).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameObject.Find("Player"))
        {
            if (m_player.IsMoving())
            {
                rb.linearVelocity = m_player.transform.position * crateSpeed;
            }
        }
    }
}
