using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public enum Type { Monster_1, Monster_2, Monster_3, Nope }
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
            case Type.Nope:
                break;
        }
    }

    void monster_1()
    {
        if (!reload && ability.hp > 0)
        {
            reload = true;
            StartCoroutine("monster1_bullet");
        }
        if (ability.hp <= 0)
            StopCoroutine("monster1_bullet");
    }

    void monster_2()
    {
        Vector3 distance = transform.position - GameObject.Find("Player").transform.position;
        if (distance.x < 0)
            distance.x *= -1;
        if (distance.y < 0)
            distance.y *= -1;

        if (distance.x <= 0.4f && distance.y <= 0.4f)
        {
            transform.localScale = Vector3.one * 1.25f;
        }
    }

    void monster_3()
    {
        if (!reload && ability.hp > 0)
        {
            reload = true;
            StartCoroutine("monster2_bullet");
        }
        if (ability.hp <= 0)
            StopCoroutine("monster2_bullet");
    }

    IEnumerator monster1_bullet()
    {
        yield return new WaitForSeconds(ability.attack_speed);
        reload = false;
        GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 0));
        newBullet.transform.LookAt(taget.transform);
        newBullet.GetComponent<Bullet>().damage = ability.damage;
    }

    IEnumerator monster2_bullet()
    {
        float rotate = 0;
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.1f);
            GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.Euler(rotate, -90, 0));
            newBullet.GetComponent<Bullet>().damage = ability.damage;
            newBullet.GetComponent<MeshRenderer>().material.color = Color.red;
            rotate += 36;
        }

        yield return new WaitForSeconds(ability.attack_speed);
        reload = false;
    }
}
