using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestScript : MonoBehaviour
{
    [SerializeField] private int maxFullness;
    [SerializeField] private int currentFullness;
    public BarScript bar;
    public GameObject gameOverTitle;
    public GameObject win;
    public GameObject player;

    void Start()
    {
        bar.SetMaxValue(maxFullness);
        //currentFullness = 100;
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
    }

    IEnumerator Hunger()
    {
        while (true)
        {
            currentFullness -= 1;
            bar.SetValue(currentFullness);
            yield return new WaitForSeconds(1f);
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
        //currentFullness = maxFullness;
        bar.SetValue(currentFullness);
    }
}
