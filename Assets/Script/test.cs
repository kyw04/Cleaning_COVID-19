using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject taget;
    public float speed = 1;
    void Start()
    {
        
    }

    void Update()
    {
        transform.LookAt(taget.transform.position);

        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 force = transform.position - collision.transform.position;
            GetComponent<Rigidbody>().AddForce(force * 100);
        }
    }
}
