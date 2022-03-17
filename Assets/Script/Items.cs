using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public enum items{ WeaponUp, Invincible, HpRecovery, PainRecovery };
    public items type;
    public float rotation_speed;
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        transform.Rotate(0, rotation_speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Ability other_ability = other.GetComponent<Ability>();
            GetComponent<Ability>().hp = 0;

            if (type == items.WeaponUp && other_ability.maxWeaponLevel > other_ability.WeaponLevel)
            {
                other_ability.WeaponLevel++;
            }
            if (type == items.Invincible)
            {
                if (other.gameObject.layer == 9)
                {
                    StopCoroutine("Invincibility");
                }
                StartCoroutine("Invincibility");
            }
            if (type == items.HpRecovery)
            {
                other_ability.hp += 30;
            }
            if (type == items.PainRecovery)
            {
                other_ability.pain -= 30;
            }

            Destroy(this.gameObject, 3.5f);
            GetComponent<Renderer>().enabled = false;
        }
    }

    IEnumerator Invincibility()
    {
        player.gameObject.layer = 9;
        player.gameObject.GetComponentInChildren<ParticleSystem>().Play();
        yield return new WaitForSeconds(3f);
        player.gameObject.layer = 3;
    }
}
