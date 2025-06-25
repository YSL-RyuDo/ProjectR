using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController2 : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody rigid;
    private Vector3 moveDirection;

    public GameObject bombPrefab;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        InputManager2.Instance.moveInput += HandleMoveInput;
        InputManager2.Instance.bombInput += HandleBombInput;
    }

    private void OnDisable()
    {
        InputManager2.Instance.moveInput -= HandleMoveInput;
        InputManager2.Instance.bombInput -= HandleBombInput;
    }

    void HandleMoveInput(Vector2 input)
    {
        moveDirection = new Vector3(input.x, 0, input.y).normalized;
    }

    void HandleBombInput()
    {
        Debug.Log("ÆøÅº ¼³Ä¡");
        Instantiate(bombPrefab, this.transform.position, Quaternion.identity);
    }

    private void FixedUpdate()
    {
        rigid.velocity = moveDirection * moveSpeed;
    }

}
