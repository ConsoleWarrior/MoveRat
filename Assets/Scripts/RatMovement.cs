using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    private Animator animator;
    private Vector2 moveDirection;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        animator.SetFloat("Speed", Mathf.Abs(moveDirection.x * moveSpeed));
        animator.SetFloat("SpeedV", moveDirection.y * moveSpeed);
        bool flipped = moveDirection.x < 0;
        //bool flipHorizontal = moveDirection.x < 0;
        //bool flipVertical = moveDirection.y < 0;
        this.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f: 0f, 0f));
    }

    void FixedUpdate()
    {
        if (moveDirection != Vector2.zero)
        {
            var xMove = moveDirection.x * moveSpeed * Time.deltaTime;
            var xMoveVertical = moveDirection.y * moveSpeed * Time.deltaTime;
            this.transform.Translate(new Vector3(xMove, xMoveVertical),Space.World);
        }
        //rb.velocity = moveDirection * moveSpeed;
    }
}
