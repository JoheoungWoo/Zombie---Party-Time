using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class DataManager : MonoBehaviour
{
    public TMP_Text MineralText;

    private void Update()
    {
        if(!Utility.Prefs.CheckData("�̳׶�"))
        {
            Utility.Prefs.Save<int>("�̳׶�", 12000);
            Utility.Prefs.Save<int>("���л�", 1);
            Utility.Prefs.Save<int>("���л�", 1);
            Utility.Prefs.Save<int>("useSkin", 0);

        }
        
        UpdateMinaral();
    }

    private void UpdateMinaral()
    {
        MineralText.text = Utility.Prefs.Load<int>("�̳׶�").ToString();
    }
}
