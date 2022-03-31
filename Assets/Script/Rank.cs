using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rank : MonoBehaviour
{
    public GameObject input_name;
    public Text[] player_name;
    public Text[] player_score;
    public string[] names;
    public string[] scores;
    private int min_score, n = 5;
    private int score;
    private string newPlayer_name;

    void Start()
    {
        score = PlayerPrefs.GetInt("PlayerScore");
        if (PlayerPrefs.HasKey("Name"))
            names = PlayerPrefs.GetString("Name").Split(',');
        if (PlayerPrefs.HasKey("Score"))
            scores = PlayerPrefs.GetString("Score").Split(',');
        else
            PlayerPrefs.SetString("Score", "0,0,0,0,0");
        scores = PlayerPrefs.GetString("Score").Split(',');

        int.TryParse(scores[scores.Length - 1], out min_score);
        input_name.SetActive(min_score < score);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        if (Input.GetKeyDown(KeyCode.K))
            PlayerPrefs.DeleteAll();
        for (int i = 0; i < n; i++)
        {
            if (scores[i] != null)
            {
                player_name[i].text = names[i];
                player_score[i].text = scores[i];
            }
            else
            {
                player_name[i].text = "-";
                player_score[i].text = "-";
            }
        }
    }
    public void rank()
    {
        newPlayer_name = input_name.GetComponent<InputField>().text;
        input_name.SetActive(false);
        int temp, index = n;
        for (int i = n - 1; i >= 0; i--)
        {
            int.TryParse(scores[i], out temp);
            if (temp < score)
                index--;
        }
        for (int i = n - 2; i >= index; i--)
        {
            scores[i + 1] = scores[i];
            names[i + 1] = names[i];
        }
        scores[index] = score.ToString();
        names[index] = newPlayer_name;

        data_load();
    }

    void data_load()
    {
        string data = "";
        for (int i = 0; i < n; i++)
        {
            data += scores[i];
            if (n - 1 > i)
            {
                data += ",";
            }
        }
        PlayerPrefs.SetString("Score", data);
        data = "";
        for (int i = 0; i < n; i++)
        {
            data += names[i];
            if (n - 1 > i)
            {
                data += ",";
            }
        }
        PlayerPrefs.SetString("Name", data);
        Debug.Log(PlayerPrefs.GetString("Name"));
        Debug.Log(PlayerPrefs.GetString("Score"));
    }
}
