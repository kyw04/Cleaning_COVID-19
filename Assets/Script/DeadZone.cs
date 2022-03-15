using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public Ability ability;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            ability.pain += collision.GetComponent<Ability>().damage / 2;
        }
        Destroy(collision.gameObject);
    }
}
