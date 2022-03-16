using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public enum items{ WeaponUp, Invincible, HpRecovery, PainRecovery };
    public items type;
    public float rotation_speed;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(0, rotation_speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Bullet")
        {
            Ability other_ability = other.GetComponent<Ability>();

            if (type == items.WeaponUp && other_ability.maxWeaponLevel > other_ability.WeaponLevel)
            {
                other_ability.WeaponLevel++;
            }
            if (type == items.Invincible)
            {
                StartCoroutine(Invincibility(other));
            }
            if (type == items.HpRecovery)
            {
                other_ability.hp += 30;
            }
            if (type == items.PainRecovery)
            {
                other_ability.pain -= 30;
            }
            Destroy(this.gameObject); // ¹ö±×
        }
    }

    IEnumerator Invincibility(Collider collider)
    {
        collider.gameObject.layer = 9;
        collider.gameObject.GetComponentInChildren<ParticleSystem>().Play();
        yield return new WaitForSeconds(3f);
        collider.gameObject.layer = 3;
    }
}
