using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRat : MonoBehaviour
{
    //private Rigidbody2D rb;

    private Animator animator;
    private Vector2 moveDirection;
    public bool onNest = false;
    public bool onArea = false;


    [SerializeField] private float currentSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float walkSpeed;

    [SerializeField] private int maxFullness;
    [SerializeField] public int currentFullness;
    [SerializeField] public int thirst;

    public GameObject gameOverTitle;
    public BarScript bar;
    public NestScript nest;
    public Animator holeAnim;
    public Dubler dubler;

    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        bar.SetMaxValue(maxFullness);
    }
    void OnEnable()
    {
        dubler.Stop();
        currentFullness = dubler.currentFullness;
        StartCoroutine("HungerCoro");
    }
    void OnDisable()
    {
        dubler.CopyAndRun();
    }

    void Update()
    {
        if (currentFullness <= 0)
        {
            gameOverTitle.SetActive(true);
            this.gameObject.SetActive(false);
        }
        Sprint();

        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        animator.SetFloat("Speed", Mathf.Abs(moveDirection.x * currentSpeed));
        animator.SetFloat("SpeedV", moveDirection.y * currentSpeed);
        bool flipped = moveDirection.x < 0;
        this.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f : 0f, 0f));
    }

    public void Feeding()
    {
        currentFullness -= 20;
        bar.SetValue(currentFullness);
        nest.Feed();
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
        }
        if (other.CompareTag("Hole"))
        {
            onNest = true;
            holeAnim = other.gameObject.GetComponent<Animator>();
        }
        if (other.CompareTag("Area"))
        {
            onArea = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Nest") || other.CompareTag("Hole"))
        {
            onNest = false; holeAnim = null;
        }
        if (other.CompareTag("Area"))
        {
            onArea = false;
        }
    }

    IEnumerator HungerCoro()
    {
        while (true)
        {
            Hunger();
            yield return new WaitForSeconds(1f);
        }
    }

    public void Hunger()
    {
        currentFullness -= thirst;
        bar.SetValue(currentFullness);
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
