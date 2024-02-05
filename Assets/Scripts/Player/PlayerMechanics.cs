using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using Photon.Pun;

public class PlayerMechanics : MonoBehaviour, IPunObservable
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float rayDistance = 1f;
    [SerializeField] private float jumpDuration = 0.06f;

    [SerializeField] private LayerMask platformsLayer;
    [SerializeField] private Transform point;

    private Rigidbody2D playerRb;
    private PhotonView photonView;
    private InputManager inputManager;
    private PlayerAnimations playerAnimations;
    private CapsuleCollider2D collider;

    private float jumpTimer = 0;
    private float health = 100f;
    private bool isInGround = true;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        photonView = GetComponent<PhotonView>();
        inputManager = GetComponent<InputManager>();
        playerAnimations = GetComponent<PlayerAnimations>();
        collider = GetComponent<CapsuleCollider2D>();
    }

    void FixedUpdate()
    {
        if (!photonView.IsMine || !inputManager) return;

        ProcessMovement();
        ProcessJump();
        ProcessAttack();
    }

    private void ProcessMovement()
    {
        playerRb.velocity = new Vector2(inputManager.HorizontalAxis * moveSpeed, playerRb.velocity.y);
        playerAnimations.PlayRunAnim(inputManager.HorizontalAxis != 0);
        if (inputManager.HorizontalAxis != 0)
            transform.localScale = new Vector3(-Mathf.Sign(inputManager.HorizontalAxis), transform.localScale.y, transform.localScale.z);
    }

    private void ProcessJump()
    {
        if (inputManager.IsJumpPressed)
        {
            if (isInGround)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
                isInGround = false;
                playerAnimations.PlayJumpAnim();
            }
            else
                if (jumpTimer < jumpDuration)
                playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
        }

        if (IsGrounded())
        {
            isInGround = true;
            jumpTimer = 0;
        }
        else
        {
            jumpTimer += Time.deltaTime;
        }
    }

    private void ProcessAttack()
    {
        if (inputManager.IsAttackClicked)
            playerAnimations.PlayAttackAnim();
    }

    public void OnAttackAnimComplete()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(point.position, new Vector2(-transform.localScale.x, 0), rayDistance);
        if (hitInfo)
        {
            Debug.Log($"Name {PhotonNetwork.NickName}: {hitInfo.transform.gameObject.name}");
            photonView.RPC("DealDamage", RpcTarget.Others);
        }
    }

    [PunRPC]
    void DealDamage()
    {
        health -= 10;
        Debug.Log($"Health {photonView.name}: {health}");
    }

    private bool IsGrounded() => collider.IsTouchingLayers(platformsLayer);

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            byte[] velocityBytes = VectorSerialization.Vector2ToBytes(playerRb.velocity);
            stream.SendNext(velocityBytes);
        }
        else if (stream.IsReading)
        {
            byte[] velocityBytes = (byte[])stream.ReceiveNext();
            Vector2 decompressedVelocity = VectorSerialization.BytesToVector2(velocityBytes);
            if (playerRb)   
                playerRb.velocity = decompressedVelocity;
        }
    }
}

