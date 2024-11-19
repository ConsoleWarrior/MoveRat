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
}
