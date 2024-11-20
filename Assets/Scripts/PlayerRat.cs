using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRat : MonoBehaviour
{
    //private Rigidbody2D rb;

    private Animator animator;
    private Vector2 moveDirection;
    private bool onNest = false;
    private NestScript nest;

    [SerializeField] private float currentSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float walkSpeed;

    [SerializeField] private int maxFullness;
    [SerializeField] private int currentFullness;
    [SerializeField] private int thirst;

    public GameObject gameOverTitle;
    public BarScript bar;

    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        bar.SetMaxValue(maxFullness);
        //StartCoroutine("Hunger");
    }
    void OnEnable()
    {
        StartCoroutine("Hunger");
    }

    void Update()
    {
        if (currentFullness <= 0)
        {
            gameOverTitle.SetActive(true);
            this.gameObject.SetActive(false);
        }
        Sprint();
        if (onNest)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                currentFullness -= 20;
                bar.SetValue(currentFullness);
                nest.Feed();
            }
            if(Input.GetKeyDown(KeyCode.E))
            {
                this.gameObject.SetActive(false);
            }
        }

        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        animator.SetFloat("Speed", Mathf.Abs(moveDirection.x * currentSpeed));
        animator.SetFloat("SpeedV", moveDirection.y * currentSpeed);
        bool flipped = moveDirection.x < 0;
        //bool flipHorizontal = moveDirection.x < 0;
        //bool flipVertical = moveDirection.y < 0;
        this.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f : 0f, 0f));
    }

    void FixedUpdate()
    {
        if (moveDirection != Vector2.zero)
        {
            var xMove = moveDirection.x * currentSpeed * Time.deltaTime;
            var xMoveVertical = moveDirection.y * currentSpeed * Time.deltaTime;

            if (xMoveVertical != 0 && xMove != 0)
                this.transform.Translate(new Vector3(xMove, xMoveVertical) * 0.75f, Space.World);
            else
                this.transform.Translate(new Vector3(xMove, xMoveVertical), Space.World);
        }
        //rb.velocity = moveDirection * realSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            currentFullness += 25;
            if (currentFullness > 100)
                currentFullness = maxFullness;
            bar.SetValue(currentFullness);
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("Nest"))
        {
            onNest = true;
            nest = other.gameObject.GetComponent<NestScript>();
            //currentFullness -= 20;
            //bar.SetValue(currentFullness);
            //other.gameObject.GetComponent<NestScript>().Feed();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Nest"))
        {
            onNest = false;
        }
    }
    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    if (other.CompareTag("Nest"))
    //    {
    //        Debug.Log("bef");
    //        if (Input.GetKeyDown(KeyCode.F))
    //        {
    //            Debug.Log("f");
    //            currentFullness -= 20;
    //            bar.SetValue(currentFullness);
    //            other.gameObject.GetComponent<NestScript>().Feed();
    //        }
    //    }
    //}

    IEnumerator Hunger()
    {
        while (true)
        {
            currentFullness -= thirst;
            bar.SetValue(currentFullness);
            yield return new WaitForSeconds(1f);
        }
    }

    private void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
            thirst = 3;
        }
        else
        {
            currentSpeed = walkSpeed;
            thirst = 1;
        }
    }
}
