using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rank : MonoBehaviour
{
    public Text[] player_name;
    public Text[] player_score;

    void Start()
    {
        if (!PlayerPrefs.HasKey("name"))
            PlayerPrefs.SetString("name", "-");
    }

    void Update()
    {
        
    }
}
