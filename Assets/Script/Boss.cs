using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float hp = 100;
    public float damage = 10;
    public float speed = 0.5f;
    public float reload_speed = 3;
    public GameObject[] monsters;
    private GameObject player;
    private Ability player_ability;
    Vector3 rotate;
    private bool isDie = false;
    private int skill_number;

    void Start()
    {
        rotate = new Vector3(25, -25, 25);
        player = GameObject.Find("Player");
        player_ability = player.GetComponent<Ability>();
        StartCoroutine("skill");
    }

    void Update()
    {
        if (hp > 0)
        {
            transform.Rotate(rotate * Time.deltaTime);
        }
        else if (!isDie)
            die();

        if (transform.position.y >= 2 || transform.position.y >= 1.75f)
        {
            transform.position -= Vector3.up * speed * Time.deltaTime;
        }
    }

    void die()
    {
        isDie = true;
        gameObject.layer = 8;
        Destroy(this.gameObject, 2);
        Score.instance.AddScore(150);
        GetComponent<MeshRenderer>().material.color = Color.gray;
        GetComponentInChildren<ParticleSystem>().Play();
    }

    void skill_1()
    {

    }
    void skill_2()
    {

    }
    void skill_3()
    {
        int rand = Random.Range(0, monsters.Length);
        for (int i = 0; i < rand; i++)
        {
            Instantiate(monsters[rand], transform.position, transform.rotation);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            collision.gameObject.layer = 8;
            collision.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            collision.gameObject.GetComponentInChildren<ParticleSystem>().Play();
            Destroy(collision.gameObject, 0.25f);
            hp -= player_ability.damage;
        }
    }

    IEnumerator skill()
    {
        yield return new WaitForSeconds(5);
        while (true)
        {
            yield return new WaitForSeconds(reload_speed);
            skill_number = Random.Range(0, 3);
            Debug.Log(skill_number);

            switch (skill_number)
            {
                case 0:
                    skill_1();
                    break;
                case 1:
                    skill_2();
                    break;
                case 2:
                    skill_3();
                    break;
            }
        }
    }
}
