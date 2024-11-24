using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleScript : MonoBehaviour
{
    private Animator animator;
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject clue;
    [SerializeField] public Controller controller;


    void Start()
    {
        animator = GetComponent<Animator>();
        controller.areas.Add(transform.GetChild(0).gameObject);
    }

    void Update()
    {

    }
    void OnMouseDown()
    {
        if (!player.activeSelf)
        {
            controller.holeAnim.SetBool("Full", false);
            controller.direction = transform.position;
            controller.isMove = true;
            controller.holeAnim = animator;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            clue.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && clue != null)
        {
            clue.SetActive(false);
        }
    }
}
