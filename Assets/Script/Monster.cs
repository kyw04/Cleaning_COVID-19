using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public enum Type { Monster_1, Monster_2, Monster_3, Monster_4, Nope }
    public Type type;
    public GameObject bullet;
    private GameObject taget;
    private Ability ability;
    private bool reload;
    private Vector3 rotation_speed;

    void Start()
    {
        reload = false;
        taget = GameObject.Find("Player");
        ability = GetComponent<Ability>();

        float rand = Random.Range(-2, 2);
        if (rand == 0)
            rand = 1;

        rotation_speed = Vector3.one * rand * ability.speed * 50;
    }

    void Update()
    {
        if (ability.hp > 0)
            transform.Rotate(rotation_speed * Time.deltaTime);

        switch (type)
        {
            case Type.Monster_1:
                monster_1();
                break;
            case Type.Monster_2:
                monster_2();
                break;
            case Type.Monster_3:
                monster_3();
                break;
            case Type.Monster_4:
                monster_4();
                break;
            case Type.Nope:
                break;
        }
    }

    void monster_1()
    {
        if (!reload && ability.hp > 0)
        {
            reload = true;
            StartCoroutine("spawn_bullet");
        }
        if (ability.hp <= 0)
            StopCoroutine("spawn_bullet");
    }

    void monster_2()
    {

    }

    void monster_3()
    {

    }

    void monster_4()
    {

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
