using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankControl : MonoBehaviour
{
    private string secretKey = "MySecretKey";
    private string addScoreURL = "http://localhost/Rank/addscore.php?";
    private string highscoreURL = "http://localhost/Rank/display.php";
    public string playerName;
    public int playerScore;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
