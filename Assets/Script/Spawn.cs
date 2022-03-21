using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject[] entity;
    public float[] time;

    void Start()
    {
        StartCoroutine("Spawner", 0);
        StartCoroutine("Spawner", 2);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            StopCoroutine("Spawner");
        if (Input.GetKeyDown(KeyCode.X))
            StartCoroutine("Spawner", 1);
    }

    IEnumerator Spawner(int index)
    {
        while (true)
        {
            Instantiate(entity[index], RandomPos(), Quaternion.Euler(0, 0, 0));
            yield return new WaitForSeconds(time[index]);
        }
    }

    Vector3 RandomPos()
    {
        Vector3 pos = GetComponent<BoxCollider>().bounds.size;
        float randX = Random.Range((pos.x / 2) * -1, pos.x / 2);
        float randY = Random.Range((pos.y / 2) * -1, pos.y / 2);

        return transform.position + new Vector3(randX, randY); 
    }
}
