using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRat : MonoBehaviour
{
    private Rigidbody2D rb;
    
    private Animator animator;
    private Vector2 moveDirection;

    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private int fullness = 100;

    public GameObject gameOverTitle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        StartCoroutine("Hunger");
    }

    void Update()
    {
        if(fullness <= 0)
        {
            gameOverTitle.SetActive(true);
            this.gameObject.SetActive(false);
        }
        
        
        
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

            if (xMoveVertical != 0 && xMove != 0)
                this.transform.Translate(new Vector3(xMove, xMoveVertical)*0.75f, Space.World);
            else
            this.transform.Translate(new Vector3(xMove, xMoveVertical),Space.World);
        }
        //rb.velocity = moveDirection * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            fullness += 25;
            other.gameObject.SetActive(false);
        }
    }

    IEnumerator Hunger()
    {
        while (true)
        {
            fullness -= 1;
            yield return new WaitForSeconds(1f);
        }
    }
}
