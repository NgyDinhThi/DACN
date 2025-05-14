using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class NPCmovement : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float moveSpeed;

    private readonly int moveX = Animator.StringToHash("MoveX");
    private readonly int moveY = Animator.StringToHash("MoveY");

    private Waypoint wayPoint;
    private Animator animator;
    private Vector3 prePosition;
    private int currentPointIndex;
    private void Awake()
    {
        wayPoint = GetComponent<Waypoint>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 nextPos = wayPoint.Layvitri(currentPointIndex);
        UpdateMoveValue(nextPos);
        transform.position = Vector3.MoveTowards(transform.position, nextPos, moveSpeed*Time.deltaTime);
        if (Vector3.Distance(transform.position, nextPos) <= 0.2f)
        {
            prePosition = nextPos;
            currentPointIndex = (currentPointIndex + 1) % wayPoint.Diadiem.Length;
        }
    }
    private void UpdateMoveValue(Vector3 nextPos)
    {
        Vector2 dir = Vector2.zero;
        if (prePosition.x < nextPos.x)dir = new Vector2(1f, 0f);
        if (prePosition.x > nextPos.x)dir = new Vector2(-1f, 0f);
        if (prePosition.y < nextPos.y)dir = new Vector2(0f, 1f);
        if (prePosition.y > nextPos.y)dir = new Vector2(0f, -1f);

        animator.SetFloat(moveX, dir.x);
        animator.SetFloat(moveY, dir.y);
    }    

}
