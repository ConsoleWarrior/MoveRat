using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Controller : MonoBehaviour
{
    public GameObject player;
    public GameObject nest;
    private PlayerRat playerScript;
    public Animator holeAnim;
    public List<GameObject> areas = new List<GameObject>();
    private bool activeB = false;
    public GameObject holes;
    public Dubler dubler;
    public bool isMove = false;
    public Vector3 direction;
    public BarScript bar;
    private Vector3 newHoleDirection;

    void Start()
    {
        playerScript = player.GetComponent<PlayerRat>();
    }

    void Update()
    {
        if (isMove && player.transform.position == direction)
        {
            isMove = false;
            holeAnim.SetBool("Full", true);
        }
        if(player.transform.position != newHoleDirection)
        {
            StopAllCoroutines();
            bar.SetValue(0);
        }
    }

    void FixedUpdate()
    {
        if (isMove)
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, direction, Time.deltaTime);
        }
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
            else if (!player.activeSelf && !isMove)
            {
                Debug.Log(holeAnim.gameObject.name);
                holeAnim.SetBool("Full", false);
                player.SetActive(true);
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
            if (!player.activeSelf)
            {
                dubler.Eating();
                Debug.Log("Eat 20");
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
            if (activeB && playerScript.onArea)
            {
                newHoleDirection = player.transform.position;
                StartCoroutine(WaitForBuild());
                //pausa na bar
                //foreach (Transform hole in holes.transform)
                //{
                //    if (!hole.gameObject.activeSelf)
                //    {
                //        hole.position = player.transform.position;
                //        hole.gameObject.SetActive(true);
                //        hole.GetChild(0).gameObject.SetActive(true);
                //        break;
                //    }
                //}
                //Instantiate(Resources.Load("Hole"), player.transform.position, Quaternion.identity);
            }
        }

    }
    IEnumerator WaitForBuild()
    {
        Debug.Log("Hello!");
        bar.SetMaxValue(30);
        for (int i = 0; i < 30; i++)
        {
            bar.SetValue(i);
            yield return new WaitForSeconds(0.1f);
        }
        bar.SetValue(0);
        foreach (Transform hole in holes.transform)
        {
            if (!hole.gameObject.activeSelf)
            {
                hole.position = player.transform.position;
                hole.gameObject.SetActive(true);
                hole.GetChild(0).gameObject.SetActive(true);
                break;
            }
        }
        Debug.Log("by!");
    }

}
