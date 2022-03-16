using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coming : MonoBehaviour
{
    private Ability ability;
    private bool die;
    public bool random = false;
    public float rand_min;
    public float rand_max;
    void Start()
    {
        die = false;
        ability = GetComponent<Ability>();
        if (random)
            ability.speed = Random.Range(rand_min, rand_max);
    }

    void Update()
    {
        if (ability.hp <= 0 && !die)
        {
            gameObject.layer = 8;
            die = true;
            Score.instance.AddScore(ability.score);
            GetComponentInChildren<ParticleSystem>().Play();
            GetComponent<MeshRenderer>().material.color = new Color32(95, 95, 95, 200);
            Destroy(this.gameObject, 1);
        }

        if (!die)
        {
            transform.position -= Vector3.up * ability.speed * Time.deltaTime;
        }
    }
}