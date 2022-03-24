using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public GameObject bullet;
    private GameObject taget;
    private Ability ability;
    private bool reload;
    void Start()
    {
        reload = false;
        taget = GameObject.Find("Player");
        ability = GetComponent<Ability>();
    }

    void Update()
    {
        if (!reload && ability.hp > 0)
        {
            reload = true;
            StartCoroutine("spawn_bullet");
        }
        if (ability.hp <= 0)
            StopCoroutine("spawn_bullet");
    }

    IEnumerator spawn_bullet()
    {
        yield return new WaitForSeconds(ability.attack_speed);
        reload = false;
        GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 0));
        newBullet.transform.LookAt(taget.transform);
        newBullet.GetComponent<Bullet>().damage = ability.damage;
    }
}
