using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleScript : MonoBehaviour
{
    private Animator animator;
    public GameObject player;
    public GameObject clue;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //if (player.activeSelf)
        //{
        //    animator.SetBool("Full", true);
        //    //player.SetActive(false);
        //}
        //if (!player.activeSelf && Input.GetKeyDown(KeyCode.E))
        //{
        //    animator.SetBool("Full", false);
        //    player.SetActive(true);
        //}
    }
    void OnGUI()
    {
        if (Event.current.Equals(Event.KeyboardEvent("e")))
        {
            //if (player.activeSelf)
            //{
            //    animator.SetBool("Full", true);
            //    Debug.Log("E hole enter");
            //}
            if (!player.activeSelf)
            {
                player.SetActive(true);
                animator.SetBool("Full", false);
                Debug.Log("E hole exit");
            }
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
        if (other.CompareTag("Player"))
        {
            clue.SetActive(false);
        }
    }
}
