using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatScript : MonoBehaviour
{
    
    [SerializeField] private float currentSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float walkSpeed;
    private Vector2 moveDirection;
    private Rigidbody2D rb;


    public GameObject gameOverTitle;
    public GameObject player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        moveDirection = player.transform.position;
    }

    void FixedUpdate()
    {
        //if (moveDirection != Vector2.zero)
        //{
        //    var xMove = moveDirection.x * currentSpeed * Time.deltaTime;
        //    var xMoveVertical = moveDirection.y * currentSpeed * Time.deltaTime;

        //    if (xMoveVertical != 0 && xMove != 0)
        //        this.transform.Translate(new Vector3(xMove, xMoveVertical) * 0.75f, Space.World);
        //    else
        //        this.transform.Translate(new Vector3(xMove, xMoveVertical), Space.World);
        //}
        rb.velocity = moveDirection * walkSpeed;
    }

    IEnumerator Hunt()
    {
        while (true)
        {
            
            yield return new WaitForSeconds(10f);
        }
    }
}
