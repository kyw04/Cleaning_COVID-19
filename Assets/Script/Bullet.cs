using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Ability player_ability;
    public float speed = 1;
    void Start()
    {
        player_ability = GameObject.Find("Player").GetComponent<Ability>();
    }

    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;

        Destroy(this.gameObject, 5);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            GetComponentInChildren<ParticleSystem>().Play();
            gameObject.layer = 8;
            gameObject.GetComponent<MeshRenderer>().material.color = new Color32(255, 255, 255, 0);
            Destroy(this.gameObject, 1);
            collision.GetComponent<Ability>().hp -= player_ability.damage;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
            Destroy(this.gameObject);
    }
}
