using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour
{
    public float detectionRange = 20f;
    public float speed = 2f;

    public Transform patrolPointA;
    public Transform patrolPointB;

    private Vector3 currentTarget;
    private bool playerDetected;

    private Transform targetPlayer;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentTarget = patrolPointB.position;
    }

    // Update is called once per frame
    void Update()
    {
        Transform player = GetClosestPlayer();

        //if player isn't in frame then just patrol 
        if(player == null)
        {
            Patrol();
        }

        //calculates distance between player and enemy 
        float dist = Vector2.Distance(transform.position, player.position);

        //chase if within the dtection range 
        if(dist < detectionRange)
        {
            playerDetected = true;

        }

        else if(dist> detectionRange + 1f)
        {
            playerDetected = false;
        }

        if(playerDetected)
        {
            MoveTowards(player.position);
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        MoveTowards(currentTarget);

        if(Vector2.Distance(transform.position, currentTarget) < 0.1f)
        {
            currentTarget = currentTarget == patrolPointA.position ? patrolPointB.position : patrolPointA.position;
        }
    }

    private void MoveTowards(Vector3 target)
    {
        Vector2 direction = (target - transform.position).normalized;
        //transform.position += (Vector3)direction * speed * Time.deltaTime;

        rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
    }

    //targets closest player 
    private Transform GetClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        Transform closestPlayer = null;
        float minDist = Mathf.Infinity;

        foreach(GameObject p in players)
        {
            float dist = Vector2.Distance(transform.position, p.transform.position);

            if(dist < minDist)
            {
                minDist = dist;
                closestPlayer = p.transform;
            }
        }

        return closestPlayer;
    }
}
