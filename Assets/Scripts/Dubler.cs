using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dubler : MonoBehaviour
{
    public GameObject player;
    public NestScript nest;
    private PlayerRat playerScript;
    public BarScript bar;
    [SerializeField] public int maxFullness;
    [SerializeField] public int currentFullness;
    [SerializeField] public int thirst;

    void Start()
    {
        playerScript = player.GetComponent<PlayerRat>();
    }

    void Update()
    {
        
    }

    public void CopyAndRun()
    {
        currentFullness = playerScript.currentFullness;
        thirst = playerScript.thirst;
        StartCoroutine("Hunger");
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    public void Eating()
    {
        currentFullness += 20;
        if (currentFullness > 100)
            currentFullness = maxFullness;
        bar.SetValue(currentFullness);
        nest.Eat();
    }

    IEnumerator Hunger()
    {
        while (true)
        {
            currentFullness -= thirst;
            bar.SetValue(currentFullness);
            yield return new WaitForSeconds(1f);
        }
    }
}
