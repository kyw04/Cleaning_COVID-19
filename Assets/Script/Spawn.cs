using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject[] entity;
    public float[] time;
    public GameObject[] wave;
    public GameObject[] boss;
    public float _time;
    public int level;

    void Start()
    {
        level = 1;
        _time = 0;
    }

    void Update()
    {
        _time += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.E))
            StopCoroutine("SpawnEntity");
        if (Input.GetKeyDown(KeyCode.X))
            StartCoroutine("SpawnEntity", 1);

        if (level == 1)
        switch ((int)_time)
        {
            case 3:
                _time++;
                StartCoroutine("SpawnEntity", 0);
                StartCoroutine("SpawnEntity", 2);
                break;
            case 40:
                _time++;
                StopCoroutine("SpawnEntity");
                Instantiate(entity[1], RandomPos(), transform.rotation);
                break;
            case 45:
                 _time++;
                Instantiate(wave[0], transform.position, transform.rotation);
                break;
            case 53:
                 _time++;
                StartCoroutine("SpawnEntity", 0);
                StartCoroutine("SpawnEntity", 2);
                break;
            case 75:
                 _time++;
                Instantiate(entity[1], RandomPos(), transform.rotation);
                StartCoroutine("SpawnEntity", 3);
                break;
            case 135:
                 _time++;
                Instantiate(entity[1], RandomPos(), transform.rotation);
                StartCoroutine("SpawnEntity", 4);
                break;
            case 200:
                _time++;
                StopCoroutine("SpawnEntity");
                Instantiate(boss[0], transform.position, transform.rotation);
                break;
        }
        if (level == 2)
        {
            switch ((int)_time)
            {
                case 3:
                    _time++;
                    Instantiate(entity[1], RandomPos(), transform.rotation);
                    Instantiate(entity[1], RandomPos(), transform.rotation);
                    StartCoroutine("SpawnEntity", 0);
                    StartCoroutine("SpawnEntity", 2);
                    StartCoroutine("SpawnEntity", 3);
                    StartCoroutine("SpawnEntity", 4);
                    break;
                case 50:
                    _time++;
                    Instantiate(entity[1], RandomPos(), transform.rotation);
                    break;
                case 100:
                    _time++;
                    StartCoroutine("SpawnEntity", 5);
                    break;
                case 150:
                    _time++;
                    Instantiate(entity[1], RandomPos(), transform.rotation);
                    StopCoroutine("SpawnEntity");
                    break;
                case 155:
                    _time++;
                    Instantiate(boss[1], transform.position, transform.rotation);
                    boss[1].GetComponent<MeshRenderer>().material.color = new Color32(255, 255, 0, 255);
                    break;
            }
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
