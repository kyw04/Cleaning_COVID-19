using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1;
    public float damage;

    private Ability player_ability;

    void Start()
    {
        player_ability = GameObject.Find("Player").GetComponent<Ability>();
    }

    void Update()
    {
        if (gameObject.layer == 3)
            transform.position += Vector3.up * speed * Time.deltaTime;
        else if (gameObject.layer == 7)
            transform.position += transform.forward * speed * Time.deltaTime;

        Destroy(this.gameObject, 5);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag != "Item" && collision.gameObject.tag != "Bullet")
        {
            GetComponentInChildren<ParticleSystem>().Play();
            gameObject.layer = 8;
            GetComponentInChildren<Renderer>().enabled = false;
            Destroy(this.gameObject, 1);
        }

        if (collision.gameObject.tag == "Monster")
        {
            collision.GetComponent<Ability>().hp -= player_ability.damage;
        }

        if (collision.gameObject.tag == "Player")
        {
            player_ability.hp -= damage;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
            Destroy(this.gameObject);
    }
}
