using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private float horizontal;

    private bool isJumpPressed;
    private bool isJumpHeld;

    private bool isCrouchPressed;
    private bool isCrouchHeld;

    private bool isAttackClicked;

    public float HorizontalAxis => horizontal;

    public bool IsJumpPressed => isJumpPressed;
    public bool IsJumpHeld => isJumpHeld;

    public bool IsCrouchPressed => isCrouchPressed;
    public bool IsCrouchHeld => isCrouchHeld;

    public bool IsAttackClicked => isAttackClicked;

    private bool isReadyToClear;
    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();

        horizontal = 0;

        isJumpPressed = false;
        isJumpHeld = false;

        isCrouchPressed = false;
        isCrouchHeld = false;
    }

    void FixedUpdate()
    {
        isReadyToClear = true;
    }

    void Update()
    {
        //if (!PlayerHealth.IsAlive) return;

        ResetInputValues();
        ProcessInputs();
    }

    private void ResetInputValues()
    {
        if (!isReadyToClear) return;

        isReadyToClear = false;
        horizontal = 0;

        isJumpPressed = false;
        isJumpHeld = false;

        isCrouchPressed = false;
        isCrouchHeld = false;

        isAttackClicked = false;
    }

    private void ProcessInputs()
    {
        horizontal = Input.GetAxis("Horizontal");

        isJumpHeld = isJumpHeld || Input.GetKey(KeyCode.Space);
        isJumpPressed = isJumpPressed || Input.GetKeyDown(KeyCode.Space);

        isCrouchHeld = isCrouchHeld || Input.GetKey(KeyCode.LeftControl);
        isCrouchPressed = isCrouchPressed || Input.GetKeyDown(KeyCode.LeftControl);

        isAttackClicked = isAttackClicked || Input.GetMouseButtonDown(0);
    }
}
