using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerMoveSpeed = 5f;
    [SerializeField] private float paddingLeft;
    [SerializeField] private float paddingRight;
    [SerializeField] private float paddingTop;
    [SerializeField] private float paddingBottom;

    private Vector2 rawInput;
    private Vector2 minScreenBounds;
    private Vector2 maxScreenBounds;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
