using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject player;
    public GameObject nest;
    private PlayerRat playerScript;
    private Animator holeAnim;
    public List<GameObject> areas = new List<GameObject>();
    private bool activeB = false;

    void Start()
    {
        playerScript = player.GetComponent<PlayerRat>();
    }

    void Update()
    {

    }

    void OnGUI()
    {
        if (Event.current.Equals(Event.KeyboardEvent("e")))
        {
            if (player.activeSelf && playerScript.onNest)
            {
                if (playerScript.holeAnim != null)
                {
                    holeAnim = playerScript.holeAnim;
                    holeAnim.SetBool("Full", true);
                }
                player.SetActive(false);
                Debug.Log("hide");
            }
            else if (!player.activeSelf)
            {
                player.SetActive(true);
                if (holeAnim != null) holeAnim.SetBool("Full", false);
                Debug.Log("exit");
            }
        }

        if (Event.current.Equals(Event.KeyboardEvent("f")))
        {
            if (player.activeSelf && playerScript.onNest)
            {
                playerScript.Feeding();
                Debug.Log("Feed 20");
            }
        }

        if (Event.current.Equals(Event.KeyboardEvent("b")))
        {
            if (activeB)
            {
                activeB = false;
                Debug.Log("off");
                foreach (var area in areas)
                {
                    area.SetActive(false);
                }
            }
            else if (!activeB)
            {
                Debug.Log(areas.Count);
                activeB = true;
                foreach (var area in areas)
                {
                    area.SetActive(true);
                }
            }
        }
        if (Event.current.Equals(Event.KeyboardEvent("h")))
        {
            if (activeB)
            {
                //Instantiate(Resources.Load("Hole"), player.transform.position, Quaternion.identity);
            }
        }
    }
}