using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public float maxHp = 100;
    public float hp = 100;
    public float pain = 0;
    public float speed = 2f;
    public float damage = 1;
    public float attack_speed = 0.5f;
    public int score = 0;
    public int maxWeaponLevel = 5;
    public int WeaponLevel = 0;
    private int Weapon = 0;

    private void FixedUpdate()
    {
        if (maxHp < hp)
            hp = maxHp;
        if (0 > pain)
            pain = 0;

        if (WeaponLevel > maxWeaponLevel)
            WeaponLevel = maxWeaponLevel;
        if (Weapon != WeaponLevel)
        {
            for (; Weapon < WeaponLevel; Weapon++)
            {
                damage *= 1.25f;
                attack_speed /= 1.25f;
            }
            for (; Weapon > WeaponLevel; Weapon--)
            {
                damage /= 1.25f;
                attack_speed *= 1.25f;
            }
        }
    }
}
