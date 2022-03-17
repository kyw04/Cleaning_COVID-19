using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject bullet;
    public GameObject gameOver;
    private Ability ability;
    private float Xmove;
    private float Ymove;
    private bool reload;
    void Start()
    {
        gameOver.SetActive(false);
        Time.timeScale = 1;
        ability = GetComponent<Ability>();
        reload = false;
    }

    void Update()
    {
        if (ability.hp <= 0 || ability.pain >= 100)
        {
            Time.timeScale = 0;
            gameOver.gameObject.SetActive(true);
            if (Input.anyKeyDown)
                SceneManager.LoadScene(1);
        }

        Xmove = Input.GetAxis("Horizontal");
        Ymove = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(Xmove, Ymove, 0) * ability.speed * Time.deltaTime;
        transform.position = transform.TransformDirection(transform.position + dir);


        if (Input.GetKey(KeyCode.Space) && !reload)
        {
            StartCoroutine(SummonBullet());
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Monster" || collision.gameObject.tag == "Blood")
            StartCoroutine("Invincible");

        if (collision.gameObject.tag == "Monster")
        {
            collision.gameObject.layer = 8;
            collision.GetComponent<Ability>().hp = 0;
            ability.hp -= collision.GetComponent<Ability>().damage / 2;
        }
        if (collision.tag == "Item")
            if (collision.GetComponent<Items>().type == Items.items.Invincible)
            {
                StopCoroutine("Invincible");
                GetComponent<MeshRenderer>().material.color = new Color32(255, 255, 255, 255);
            }
    }

    public IEnumerator Invincible()
    {
        gameObject.layer = 9;
        GetComponent<MeshRenderer>().material.color -= new Color32(0, 0, 0, 150);
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.25f);
            GetComponent<MeshRenderer>().material.color += new Color32(0, 0, 0, 150);
            yield return new WaitForSeconds(0.25f);
            GetComponent<MeshRenderer>().material.color -= new Color32(0, 0, 0, 150);
        }
        gameObject.layer = 3;
        GetComponent<MeshRenderer>().material.color = new Color32(255, 255, 255, 255);
    }
    IEnumerator SummonBullet()
    {
        reload = true;
        Instantiate(bullet, transform.position, transform.rotation);
        yield return new WaitForSeconds(ability.attack_speed);
        reload = false;
    }
}
