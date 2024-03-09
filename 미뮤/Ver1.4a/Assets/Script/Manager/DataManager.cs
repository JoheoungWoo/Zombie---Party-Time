using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class DataManager : MonoBehaviour
{
    public TMP_Text MineralText;

    private void Update()
    {
        if(!Utility.Prefs.CheckData("¹Ì³×¶ö"))
        {
            Utility.Prefs.Save<int>("¹Ì³×¶ö", 12000);
            Utility.Prefs.Save<int>("³²ÇÐ»ý", 1);
            Utility.Prefs.Save<int>("¿©ÇÐ»ý", 1);
            Utility.Prefs.Save<int>("useSkin", 0);

        }
        
        UpdateMinaral();
    }

    private void UpdateMinaral()
    {
        MineralText.text = Utility.Prefs.Load<int>("¹Ì³×¶ö").ToString();
    }
}
