using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using static UnityEngine.GraphicsBuffer;

public class CatScript : MonoBehaviour
{

    [SerializeField] private float currentSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float maxRange;
    private Vector2 moveDirection;
    private Vector2 randomDirection;

    //private Rigidbody2D rb;


    public GameObject gameOverTitle;
    public GameObject player;

    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Hunt());
    }

    void Update()
    {
        moveDirection = player.transform.position - transform.position;
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
        //rb.velocity = moveDirection * walkSpeed;
        //var heading = player.transform.position - transform.position;
        if (moveDirection.sqrMagnitude < maxRange * maxRange)
        {
            currentSpeed = sprintSpeed;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, currentSpeed * Time.deltaTime);
        }
        else
        {
            currentSpeed = walkSpeed;
            transform.position = Vector2.MoveTowards(transform.position, randomDirection, currentSpeed * Time.deltaTime);
        }
        //transform.position = Vector2.MoveTowards(transform.position, randomDirection, currentSpeed * Time.deltaTime);
        //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, walkSpeed*Time.deltaTime);
        //transform.right = moveDirection;
    }

    IEnumerator Hunt()
    {
        int i = 0;
        while (true)
        {
            if (i == 1)
            {
                randomDirection = player.transform.position;
                i = 0;
            }
            else
            {
                randomDirection = new Vector2(Random.Range(-50, 50), Random.Range(-50, 50));
                i++;
            }
            yield return new WaitForSeconds(5f);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.SetActive(false);
            currentSpeed = 0;
            gameOverTitle.SetActive(true);
        }
    }
}
