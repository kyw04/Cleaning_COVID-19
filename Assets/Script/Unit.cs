using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public GameObject bullet;
    public int unitBullet = 0;
    public int bulletMax = 25;
    private GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        transform.position = player.transform.position;

        if (unitBullet <= 0)
        {
            gameObject.SetActive(false);
            unitBullet = 0;
        }
    }
    public void SummonBullet()
    {
        unitBullet--;
        GameObject newBullet1 = Instantiate(bullet, transform.GetChild(0).position, transform.rotation);
        newBullet1.transform.localScale /= 2;
        GameObject newBullet2 = Instantiate(bullet, transform.GetChild(1).position, transform.rotation);
        newBullet2.transform.localScale /= 2;
    }
}
