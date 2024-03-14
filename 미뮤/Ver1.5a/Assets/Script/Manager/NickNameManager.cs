using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NickNameManager : MonoBehaviour
{
    public bool isName; // 닉네임이 존재하는가?
    public string NickName; // 닉네임
    public Text InputNickName; // 입력한 닉네임

    public GameObject Alarm_Name; // 닉네임 알람
    public GameObject Alarm_Name_False; // 닉네임 설정 불가 알람

    public void Awake()
    { 
        isName = Utility.Prefs.CheckData("NickName");
        if (isName == false)
        {
            Alarm_Name.gameObject.SetActive(true);
        }
        else
        {
            NickName = Utility.Prefs.Load<string>("NickName").ToString();
        }
    }

    public void SaveName()
    {
        NickName = InputNickName.text;

        if (NickName.Replace(" ", "&").Contains("&") || string.IsNullOrWhiteSpace(NickName)) // 공백이거나 입력하지 않았을 경우
        {
            Alarm_Name.gameObject.SetActive(false);
            Alarm_Name_False.gameObject.SetActive(true);
        }
        else
        {
            Utility.Prefs.Save<string>("NickName", NickName);
            Alarm_Name.gameObject.SetActive(false);
        }
    }

    public void ReturnSetName()
    {
        Alarm_Name_False.gameObject.SetActive(false);
        Alarm_Name.gameObject.SetActive(true);
    }
}
