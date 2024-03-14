using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class DataManager : MonoBehaviour
{
    public void Awake()
    {
        if(!Utility.Prefs.CheckData("남학생"))
        {
            Utility.Prefs.Save<int>("미네랄", 5000);
            Utility.Prefs.Save<int>("남학생", 1);
            Utility.Prefs.Save<int>("여학생", 1);
            Utility.Prefs.Save<int>("useSkin", 0);
        }
    }
}
