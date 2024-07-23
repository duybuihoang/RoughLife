using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D PlayerRB;
    [SerializeField] Animator animator;

    [SerializeField] protected float speed = 10f;
    [SerializeField] Vector2 direction = new Vector2(1,0);
    [SerializeField] protected float smoothFlip = 0.05f;

    [SerializeField] protected bool rollAble = false;
    [SerializeField] protected float rollingSpeed = 20f;
    protected float rollTimer;
    [SerializeField] protected float RollTime;


    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        PlayerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 axis = InputManager.Instance.Axis;
        if (axis.x > 0)
            direction = new Vector2(1, 0);
        else if(axis.x < 0)
            direction = new Vector2(-1, 0);

        roll(direction);
        flipToCurrentDirection(direction);
        movePlayer(axis);
    }

    private void flipToCurrentDirection(Vector2 direction)
    {
        double currentScale = transform.localScale.x;
        
        if (direction == Vector2.right)
        {
            if(currentScale < 0.9999f)
            {
                this.transform.localScale += new Vector3(smoothFlip, 0, 0);
            }
        }
        else if(direction == Vector2.left)
        {
            if(currentScale > -0.9999f)
            {
                this.transform.localScale += new Vector3(-smoothFlip, 0, 0);
            }
        }
    }

    private void roll(Vector2 direction)
    {
        if (InputManager.Instance.IsRolling && rollTimer <= 0)
        {
            animator.SetBool("isRolling", true);
            speed += rollingSpeed;
            rollTimer = RollTime;
            rollAble = true;
        }

        if (rollTimer <= 0 && rollAble)
        {
            speed -= rollingSpeed;
            rollAble = false;
            animator.SetBool("isRolling", false);
        }
        else
        {
            rollTimer -= Time.deltaTime;
        }
    }

    private void movePlayer(Vector2 axis)
    {
        float speedParam = Mathf.Abs(axis.x) == 0 ? Mathf.Abs(axis.y) : Mathf.Abs(axis.x);
        animator.SetFloat("Speed", speedParam);
        PlayerRB.MovePosition(PlayerRB.position + axis * speed * Time.fixedDeltaTime );
    }
}
