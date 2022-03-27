using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject[] entity;
    public float[] time;
    public GameObject[] wave;
    private float play_time;

    void Start()
    {
        play_time = 0;
        
    }

    void Update()
    {
        play_time += Time.deltaTime;

        //if (Input.GetKeyDown(KeyCode.E))
        //    StopCoroutine("SpawnEntity");
        //if (Input.GetKeyDown(KeyCode.X))
        //    StartCoroutine("SpawnEntity", 1);

        switch ((int)play_time)
        {
            case 3:
                play_time++;
                StartCoroutine("SpawnEntity", 0);
                StartCoroutine("SpawnEntity", 2);
                break;
            case 40:
                play_time++;
                StopCoroutine("SpawnEntity");
                break;
            case 45:
                play_time++;
                Instantiate(wave[0], transform.position, transform.rotation);
                break;
            case 53:
                play_time++;
                StartCoroutine("SpawnEntity", 0);
                StartCoroutine("SpawnEntity", 2);
                break;
            case 75:
                play_time++;
                Instantiate(entity[1], RandomPos(), transform.rotation);
                StartCoroutine("SpawnEntity", 3);
                break;
        }
    }

    IEnumerator SpawnEntity(int index)
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
        float randX = Random.Range(-(pos.x / 2), pos.x / 2);
        float randY = Random.Range(-(pos.y / 2), pos.y / 2);

        return transform.position + new Vector3(randX, randY); 
    }
}
