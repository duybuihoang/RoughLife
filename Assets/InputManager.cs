using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    public static InputManager Instance { get => instance; }

    [SerializeField] protected Vector2 axis;
    public Vector2 Axis { get => axis; }

    [SerializeField] protected bool isRolling;
    public bool IsRolling { get => isRolling; }

    [SerializeField] protected bool isAttacking;
    public bool IsAttacking { get => isAttacking; }

    private void Awake()
    {
        if (InputManager.instance != null)
        {
            Debug.LogError("only 1 inputManager allow to exist!!!");
        }
        InputManager.instance = this;
    }

    private void FixedUpdate()
    {
        this.getMovingAxis();

    }

    private void Update()
    {
        this.getRolling();
        this.getAttacking();
    }

    private void getMovingAxis()
    {
        this.axis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
    private void getRolling()
    {
        this.isRolling = Input.GetKeyDown(KeyCode.Space);
    }

    private void getAttacking()
    {
        this.isAttacking = Input.GetMouseButtonDown(0);
        if (isAttacking)
            Debug.Log("attack");
    }

}
