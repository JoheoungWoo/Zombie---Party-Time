using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NickNameManager : MonoBehaviour
{
    public bool isName; // �г����� �����ϴ°�?
    public string NickName; // �г���
    public Text InputNickName; // �Է��� �г���

    public GameObject Alarm_Name; // �г��� �˶�
    public GameObject Alarm_Name_False; // �г��� ���� �Ұ� �˶�

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

        if (NickName.Replace(" ", "&").Contains("&") || string.IsNullOrWhiteSpace(NickName)) // �����̰ų� �Է����� �ʾ��� ���
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
