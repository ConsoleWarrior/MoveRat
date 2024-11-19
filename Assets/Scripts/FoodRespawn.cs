using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodRespawn : MonoBehaviour
{
    private List<GameObject> foods = new List<GameObject>();
    void Start()
    {
        StartCoroutine("CheckChild");
    }

    IEnumerator CheckChild()
    {
        while (true)
        {
            foreach(Transform child in transform)
            {
                if (!foods.Contains(child.gameObject) && child.gameObject.activeSelf == false)
                {
                    foods.Add(child.gameObject);
                    StartCoroutine(RespawnChild(child.gameObject));
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }
    IEnumerator RespawnChild(GameObject obj)
    {
        yield return new WaitForSeconds(60f);
        
        obj.SetActive(true);
        foods.Remove(obj);
    }
}
