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
    private Vector2[] vectors = { new Vector2(0, 50),
    new Vector2(0,-50),new Vector2(50,0),new Vector2(-50,0),
    new Vector2(50,50),new Vector2(-50,-50),new Vector2(-50,50),
    new Vector2(50,-50)};

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
        if (moveDirection.sqrMagnitude < maxRange * maxRange && player.activeSelf)
        {
            currentSpeed = sprintSpeed;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, currentSpeed * Time.deltaTime);
        }
        else
        {
            currentSpeed = walkSpeed;
            transform.position = Vector2.MoveTowards(transform.position, randomDirection, currentSpeed * Time.deltaTime);
        }
        //transform.right = moveDirection;
    }

    IEnumerator Hunt()
    {
        int i = 0;
        while (true)
        {
            if (i > 0 && player.activeSelf)
            {
                randomDirection = transform.position;
                yield return new WaitForSeconds(2f);

                randomDirection = player.transform.position;
                i = 0;
            }
            else
            {
                //randomDirection = new Vector2(Random.Range(-50, 50), Random.Range(-50, 50));
                randomDirection = vectors[Random.Range(0, 8)];
                i++;
            }
            yield return new WaitForSeconds(Random.Range(3, 6));
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
