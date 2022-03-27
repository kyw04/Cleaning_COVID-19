using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coming : MonoBehaviour
{
    public GameObject[] items;
    public float item_percent;
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
            if (GetComponentInChildren<ParticleSystem>())
                GetComponentInChildren<ParticleSystem>().Play();
            if (GetComponent<MeshRenderer>())
                GetComponent<MeshRenderer>().material.color = new Color32(95, 95, 95, 200);
            if (gameObject.tag == "Monster" && item_percent > 0)
            {
                float rand = Random.Range(0, 100);
                if (rand <= item_percent)
                    Instantiate(items[Random.Range(0, items.Length)], transform.position, items[0].transform.rotation);
            }
            if (gameObject.tag != "Item")
                Destroy(this.gameObject, 1);
        }

        if (!die)
        {
            transform.position -= Vector3.up * ability.speed * Time.deltaTime;
        }
    }
}
