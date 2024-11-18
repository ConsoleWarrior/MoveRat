using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestScript : MonoBehaviour
{
    [SerializeField] private int maxFullness = 100;
    [SerializeField] private int currentFullness;
    public BarScript bar;
    public GameObject gameOverTitle;
    public GameObject player;

    void Start()
    {
        bar.SetMaxValue(maxFullness);
        currentFullness = maxFullness;
        StartCoroutine("Hunger");
    }

    void Update()
    {
        if (currentFullness <= 0)
        {
            gameOverTitle.SetActive(true);
            player.SetActive(false);
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
        if (currentFullness > 100)
            currentFullness = maxFullness;
        bar.SetValue(currentFullness);
    }
}
