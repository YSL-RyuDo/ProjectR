using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager2 : MonoBehaviour
{
    public static InputManager2 Instance;

    public event Action<Vector2> moveInput;
    public event Action bombInput;

    private void Awake()
    {
        if (Instance == null) Instance = this; 
        else Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveInput?.Invoke(move);

        if (Input.GetKeyDown(KeyCode.Space))
            bombInput?.Invoke();

    }
}
