using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    private GameObject player;
    private Ability player_ability;
    private Ability ability;
    public float rotation_speed;

    private void Start()
    {
        player = GameObject.Find("Player");
        player_ability = player.GetComponent<Ability>();
        ability = GetComponent<Ability>();
    }
    void Update()
    {
        if (ability.hp > 0)
            transform.Rotate(0, rotation_speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ability.hp = 0;
        }

        if (other.gameObject.tag == "Bullet")
        {
            ability.hp--;
            ability.speed /= 2;
            GetComponent<MeshRenderer>().material.color -= new Color32(50, 0, 0 , 0);
            if (ability.hp > 0)
                StartCoroutine(invincible());
        }

        if (ability.hp <= 0)
        {
            player_ability.pain += ability.damage;
        }
    }

    IEnumerator invincible()
    {
        gameObject.layer = 9;
        yield return new WaitForSeconds(0.5f);
        gameObject.layer = 10;
    }
}
