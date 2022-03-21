using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leukocyte : MonoBehaviour
{
    public GameObject[] item;
    private int index;
    void Start()
    {
        index = Random.Range(0, item.Length);
    }

    private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.tag == "Player" || other.gameObject.tag == "Bullet")
       {
            Instantiate(item[index], transform.position, transform.rotation);
            GetComponent<Renderer>().enabled = false;
            Destroy(this.gameObject);
       }
    }
}
