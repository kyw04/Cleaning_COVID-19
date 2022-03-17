using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public static Shield player_shield;

    void Awake()
    {
        if (player_shield == null) player_shield = GameObject.Find("Player").GetComponent<Shield>();
    }
    IEnumerator shield()
    {
        gameObject.layer = 9;
        GetComponentInChildren<ParticleSystem>().Play();
        yield return new WaitForSeconds(3f);
        gameObject.layer = 3;
    }
}