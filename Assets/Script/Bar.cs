using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    public enum Type{ Hp, Pain };

    public Type type;
    public Ability player_ability;
    public Image ber;
    public Text txt;
    public string start_txt;
    private float set;
    private float txt_set;

    void Start()
    {
        
    }

    void Update()
    {
        if (type == Type.Hp)
        {
            set = player_ability.hp / 100;
            txt_set = player_ability.hp;
        }
        else if (type == Type.Pain)
        {
            set = player_ability.pain / 100;
            txt_set = player_ability.pain;
        }
        ber.fillAmount = set;
        txt.text = txt_set.ToString() + start_txt;
    }
}
