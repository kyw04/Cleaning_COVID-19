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

    private void Update()
    {
        if (maxHp < hp)
            hp = maxHp;
    }
}
