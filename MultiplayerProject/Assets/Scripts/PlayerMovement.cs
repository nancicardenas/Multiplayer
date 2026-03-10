using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{

    //public Crate m_crate;

    [SerializeField] private float moveSpeed = 5f;

    public float z = 0;
    public float dashSpeed = 10f;
    public float dashDuraton = 1f;
    public float dashCoolDown = 1f; 
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator anim; 
    public eState m_nState;
    bool isDashing;


    public enum eState : int
    {
        kMove,
        kAttack,
        kDash,
        kRecovering,
        kNumStates
    }

    //enable input if is owner
    public override void OnNetworkSpawn()
    {
        PlayerInput input = GetComponent<PlayerInput>();

        if(IsOwner)
        {
            input.enabled = true;
        }

        else
        {
            input.enabled = false;
        }
    }

    //move state
    public bool IsMoving()
    {
        return (m_nState == eState.kMove);
    }

    //Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        m_nState = eState.kMove;
    }

    void FixedUpdate()
    {
        if (!IsOwner)
        {
            return;
        }

        //Moves player if in move state
        if (m_nState == eState.kMove)
        {
            rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
        }

        SubmitPositionRequestServerRpc(transform.position);
        
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (!IsOwner) return;

        Debug.Log("Move called");

        anim.SetBool("isWalking", true);
        m_nState = eState.kMove;

        //player stops walking
        if(context.canceled)
        {
            anim.SetBool("isWalking", false);
            anim.SetFloat("LastInputX", moveInput.x);
            anim.SetFloat("LastInputY", moveInput.y);
        }

        moveInput = context.ReadValue<Vector2>();

        //Sets current input x and y 
        anim.SetFloat("InputX", moveInput.x);
        anim.SetFloat("InputY", moveInput.y);

    }

    [Rpc(SendTo.Server)]
    void SubmitPositionRequestServerRpc(Vector2 newPos)
    {
        transform.position = newPos;
    }

    //players reach end of level, win if collected more than 5 gems 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidedWith = collision.gameObject;
        if (collidedWith.CompareTag("End"))
        {
            if (GemManager.Instance.totalGems >= 5)
            {
                WinManager.Instance.WinGame();
            }
        }
    }
}
