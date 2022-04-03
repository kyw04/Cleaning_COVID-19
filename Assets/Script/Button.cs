using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour
{
    public Image image;
    private bool starting = false;

    private void Start()
    {
        StartCoroutine(start());
    }
    void Update()
    {
        if (starting)
            transform.position += transform.up * 2 * Time.deltaTime;
    }

    public void menu_click()
    {
        SceneManager.LoadScene(0);
    }

    public void start_click()
    {
        GameObject button =  EventSystem.current.currentSelectedGameObject;
        button.SetActive(false);
        StartCoroutine(change_scene());
    }

    IEnumerator change_scene()
    {
        starting = true;
        while (image.color.a <= 1)
        {
            yield return new WaitForSeconds(0.001f);
            image.color += new Color32(0, 0, 0, 10);
        }

        SceneManager.LoadScene(1);
    }
    IEnumerator start()
    {
        starting = true;
        yield return new WaitForSeconds(0.75f);
        starting = false;
    }
}
