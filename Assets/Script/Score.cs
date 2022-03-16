using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Score instance;
    public Text score_board;
    private Ability player_ability;
    private string start_txt;

    private void Awake()
    {
        if (instance == null) instance = this.GetComponent<Score>();
    }

    private void Start()
    {
        player_ability = GameObject.Find("Player").GetComponent<Ability>();
        start_txt = score_board.text;
        AddScore(player_ability.score);
    }

    public void AddScore(int data)
    {
        player_ability.score += data;
        score_board.text = start_txt + player_ability.score;
    }
}
