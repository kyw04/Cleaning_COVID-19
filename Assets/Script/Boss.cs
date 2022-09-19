using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public float hp = 100;
    public float damage = 10;
    public float speed = 0.5f;
    public float reload_speed = 3;
    public int random_max;
    public GameObject[] monsters;
    public GameObject boom;
    public GameObject bullet;
    private Image boss_bar_right;
    private Image boss_bar_left;
    private Image image;
    private GameObject spawn;
    private GameObject background_2;
    private GameObject player;
    private Ability player_ability;
    private Vector3 rotate;
    private bool isDie = false;
    private bool isChange = false;
    private float start_hp;
    private int skill_number;

    void Start()
    {
        start_hp = hp;
        boss_bar_right = GameObject.Find("BossBar_Right").GetComponent<Image>();
        boss_bar_left = GameObject.Find("BossBar_Left").GetComponent<Image>();
        background_2 = GameObject.Find("Background_2");
        image = GameObject.Find("Black_Panel").GetComponent<Image>();
        rotate = new Vector3(25, -25, 25);
        player = GameObject.Find("Player");
        player_ability = player.GetComponent<Ability>();
        spawn = GameObject.Find("Spawn");
        StartCoroutine("skill");
    }

    void Update()
    {
        boss_bar_left.fillAmount = hp / start_hp;
        boss_bar_right.fillAmount = hp / start_hp;
        if (hp > 0)
        {
            transform.Rotate(rotate * Time.deltaTime);
        }
        else if (!isDie)
            die();

        if (transform.position.y >= 2 || transform.position.y >= 1.75f)
        {
            transform.position -= Vector3.up * speed * Time.deltaTime;
        }
    }

    void die()
    {
        isDie = true;
        StopCoroutine("skill");
        gameObject.layer = 8;
        Destroy(this.gameObject, 5);
        Score.instance.AddScore(150);
        spawn.GetComponent<Spawn>().level++;
        spawn.GetComponent<Spawn>()._time = 0;
        GetComponent<MeshRenderer>().material.color = Color.gray;
        GetComponentInChildren<ParticleSystem>().Play();
        Instantiate(boom, player.transform.position, player.transform.rotation);
        if (!isChange)
            StartCoroutine(change_scene());
    }

    void skill_1()
    {
        StartCoroutine(sommon_bullet_2(0.1f, 20));
    }
    void skill_2()
    {
        StartCoroutine(sommon_bullet(0.1f, 10, Quaternion.Euler(90, 0, 0)));
        StartCoroutine(sommon_bullet(0.1f, 10, Quaternion.Euler(135, -90, -90)));
        StartCoroutine(sommon_bullet(0.1f, 10, Quaternion.Euler(45, -90, -90)));
    }
    void skill_3()
    {
        int rand = Random.Range(0, monsters.Length);
        int randnum = Random.Range(0, random_max);
        for (int i = 0; i < randnum; i++)
        {
            Instantiate(monsters[rand], RandomPos(), transform.rotation);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            collision.gameObject.layer = 8;
            collision.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            collision.gameObject.GetComponentInChildren<ParticleSystem>().Play();
            Destroy(collision.gameObject, 0.25f);
            hp -= player_ability.damage;
        }
    }

    IEnumerator change_scene()
    {
        isChange = true;
        //while (image.color.a <= 1)
        //{
        //    yield return new WaitForSeconds(0.001f);
        //    image.color += new Color32(0, 0, 0, 1);
        //}
        
        yield return new WaitForSeconds(1.5f);

        if (spawn.GetComponent<Spawn>().level == 3)
            SceneManager.LoadScene(2);
        background_2.transform.position -= new Vector3(0, 0, 0.2f);
        player.transform.position = new Vector3(0, -0.5f, 0);
        gameObject.GetComponent<MeshRenderer>().enabled = false;

       // while (image.color.a >= 0)
       // {
       //     yield return new WaitForSeconds(0.001f);
       //     image.color -= new Color32(0, 0, 0, 1);
       //}
        player.GetComponent<Player>().Start();
        isChange = false;
    }
    IEnumerator skill()
    {
        yield return new WaitForSeconds(2);
        while (true)
        {
            yield return new WaitForSeconds(reload_speed);
            skill_number = Random.Range(0, 3);
            Debug.Log(skill_number);

            switch (skill_number)
            {
                case 0:
                    skill_1();
                    break;
                case 1:
                    skill_2();
                    break;
                case 2:
                    skill_3();
                    break;
            }
        }
    }
    IEnumerator sommon_bullet(float value, int num, Quaternion bullet_rotate)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject newBullet = Instantiate(bullet, transform.position, bullet_rotate);
            newBullet.GetComponent<Bullet>().damage = damage;
            yield return new WaitForSeconds(value);
        }
    }

    IEnumerator sommon_bullet_2(float value, int num)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 0));
            newBullet.transform.LookAt(player.transform.position);
            newBullet.GetComponent<Bullet>().damage = damage;
            yield return new WaitForSeconds(value);
        }
    }
    Vector3 RandomPos()
    {
        Vector3 pos = spawn.GetComponent<BoxCollider>().bounds.size;
        float randX = Random.Range(-(pos.x / 2), pos.x / 2);
        float randY = Random.Range(-(pos.y / 2), pos.y / 2);

        return spawn.transform.position + new Vector3(randX, randY, 0);
    }
}
