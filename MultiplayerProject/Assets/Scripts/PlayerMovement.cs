using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
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

    public bool IsMoving()
    {
        return (m_nState == eState.kMove);
    }

    public bool IsDiving()
    {
        return (m_nState == eState.kDash);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        m_nState = eState.kMove;

    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }

        if (m_nState == eState.kMove)
        {
            rb.linearVelocity = moveInput * moveSpeed;
        }

        //if(m_nState == eState.kDash)
        //{
        //    StartCoroutine(Dash());
        //}
        
    }

    public void Move(InputAction.CallbackContext context)
    {
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

        anim.SetFloat("InputX", moveInput.x);
        anim.SetFloat("InputY", moveInput.y);

    }

    public void CheckDash(InputAction.CallbackContext context)
    {
        if(!context.performed)
        {
            return;
        }
        if(m_nState == eState.kMove)
        {
            m_nState = eState.kDash;
            StartCoroutine(Dash());
        }

       
        //Vector3 moveDir = transform.right;
        //rb.linearVelocity = new Vector2(moveInput.x * dashSpeed, moveInput.y * dashSpeed)
        //rb.linearVelocity = new Vector2(moveDir.x * )
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        rb.linearVelocity = new Vector2(moveInput.x * dashSpeed, moveInput.y * dashSpeed);
        yield return new WaitForSeconds(dashDuraton);
        m_nState = eState.kMove;
        isDashing = false;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    GameObject collidedWith = collision.gameObject;

    //    if (collidedWith.CompareTag("Crate"))
    //    {
    //        if (m_nState == eState.kMove)
    //        {
    //            collidedWith.transform.position;
    //        }
    //    }
    //}
}
