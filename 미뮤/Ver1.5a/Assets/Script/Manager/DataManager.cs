using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class DataManager : MonoBehaviour
{
    public void Awake()
    {
        if(!Utility.Prefs.CheckData("���л�"))
        {
            Utility.Prefs.Save<int>("�̳׶�", 5000);
            Utility.Prefs.Save<int>("���л�", 1);
            Utility.Prefs.Save<int>("���л�", 1);
            Utility.Prefs.Save<int>("useSkin", 0);
        }
    }
}
