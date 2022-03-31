using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject bullet;
    public GameObject unit;
    public Image unit_magazine;
    public Text unit_txt;
    private Ability ability;
    private float Xmove;
    private float Ymove;
    private bool reload;

    public int unit_count;
    void Start()
    {
        Time.timeScale = 1;
        ability = GetComponent<Ability>();
        reload = false;
        unit_count = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();

        if (ability.hp <= 0 || ability.pain >= 100)
            StartCoroutine(Die());

        Move();
        Unit();

        if (Input.GetKey(KeyCode.Space) && !reload)
        {
            StartCoroutine(SummonBullet());
        }
    }

    void Move()
    {
        Xmove = Input.GetAxis("Horizontal");
        Ymove = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(Xmove, Ymove, 0) * ability.speed * Time.deltaTime;
        transform.position = transform.TransformDirection(transform.position + dir);
    }

    void Unit()
    {
        Unit unitCp = unit.GetComponent<Unit>();

        unit_txt.text = unit_count.ToString();

        if (Input.GetKeyDown(KeyCode.F) && unit_count > 0 && unitCp.unitBullet != unitCp.bulletMax)
        {
            unit_count--;
            unitCp.unitBullet = unitCp.bulletMax;
            unit.SetActive(true);
        }

        if (unit)
        {
            unit_magazine.fillAmount = (float)unitCp.unitBullet / unitCp.bulletMax;
        }
        else return;
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
            if (collision.GetComponent<Items>().type == Items.items.Shield)
            {
                StopCoroutine("Invincible");
                GetComponent<MeshRenderer>().material.color = new Color32(0, 0, 0, 255);
            }
    }

    IEnumerator Die()
    {
        PlayerPrefs.SetInt("PlayerScore", ability.score);
        Time.timeScale = 0.25f;
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene(2);
    }

    public IEnumerator Invincible()
    {
        gameObject.layer = 9;
        GetComponent<MeshRenderer>().material.color -= new Color32(0, 0, 0, 254);
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.25f);
            GetComponent<MeshRenderer>().material.color += new Color32(0, 0, 0, 254);
            yield return new WaitForSeconds(0.25f);
            GetComponent<MeshRenderer>().material.color -= new Color32(0, 0, 0, 254);
        }
        gameObject.layer = 3;
        GetComponent<MeshRenderer>().material.color = new Color32(0, 0, 0, 255);
    }
    IEnumerator SummonBullet()
    {
        reload = true;
        Instantiate(bullet, transform.position + Vector3.up * 0.1f, transform.rotation);
        yield return new WaitForSeconds(ability.attack_speed);
        reload = false;
    }
}
