using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestScript : MonoBehaviour
{
    [SerializeField] private int maxFullness;
    [SerializeField] private int currentFullness;
    [SerializeField] private int thirst;

    public BarScript bar;
    public GameObject gameOverTitle;
    public GameObject win;
    public GameObject player;
    public GameObject clue;
    public Controller controller;

    void Start()
    {
        bar.SetMaxValue(maxFullness);
        StartCoroutine("Hunger");
    }

    void Update()
    {
        if (currentFullness <= 0)
        {
            gameOverTitle.SetActive(true);
            player.SetActive(false);
            StopAllCoroutines();
        }

        //if (!player.activeSelf && Input.GetKeyDown(KeyCode.E))
        //{ player.SetActive(true); }
        //if (player.activeSelf && Input.GetKeyDown(KeyCode.E))
        //{ player.SetActive(false); }
    }
    void OnMouseDown()
    {
        if (!player.activeSelf)
        {
            controller.holeAnim.SetBool("Full", false);
            player.transform.position = transform.position;
        }
    }
    IEnumerator Hunger()
    {
        while (true)
        {
            currentFullness -= thirst;
            bar.SetValue(currentFullness);
            yield return new WaitForSeconds(2f);
        }
    }

    public void Feed()
    {
        currentFullness += 20;
        if (currentFullness > maxFullness)
        {
            player.SetActive(false);
            win.SetActive(true);
            StopAllCoroutines();
        }
        bar.SetValue(currentFullness);
    }
    public void Eat()
    {
        currentFullness -= 20;
        bar.SetValue(currentFullness);
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
