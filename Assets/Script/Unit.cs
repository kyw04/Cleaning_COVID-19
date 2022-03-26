using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public GameObject bullet;
    public int unitBullet = 0;
    public int bulletMax = 25;
    private GameObject player;
    private bool reload;
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        transform.position = player.transform.position;
        if (Input.GetKey(KeyCode.Space) && !reload)
        {
            unitBullet--;
            StartCoroutine(SummonBullet());
        }

        if (unitBullet <= 0)
        {
            gameObject.SetActive(false);
            reload = false;
            unitBullet = 0;
        }
    }
    IEnumerator SummonBullet()
    {
        reload = true;
        GameObject newBullet1 = Instantiate(bullet, transform.GetChild(0).position, transform.rotation);
        newBullet1.transform.localScale /= 2;
        GameObject newBullet2 = Instantiate(bullet, transform.GetChild(1).position, transform.rotation);
        newBullet2.transform.localScale /= 2;
        yield return new WaitForSeconds(player.GetComponent<Ability>().attack_speed);
        reload = false;
    }
}
