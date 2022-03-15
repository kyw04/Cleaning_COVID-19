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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bullet")
        {
            player_ability.pain += ability.damage;
            ability.hp = 0;
        }
    }
}
