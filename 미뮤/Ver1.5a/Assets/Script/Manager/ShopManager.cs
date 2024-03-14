using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ShopManager : MonoBehaviour
{
    public int useSkin;             //������� ��Ų �ε���


    public GameObject[] buyButtons; // ���� ��ư��
    public GameObject[] useButtons; // ���� ��ư��
    public GameObject[] usingButtons; // ���� ��ư��

    public GameObject alarm01Obj;   //���� Ȯ��
    public GameObject alarm02Obj;   //�̳׶� ����

    public string hopeSkinName;     //���� ��� ��Ų

    public TMP_Text MineralText; // ���� ������ �ִ� �̳׶� ǥ��

    private void Start()
    {
        if(Utility.Prefs.CheckData("useSkin"))
        {
            useSkin = (int)Utility.Prefs.Load<int>("useSkin");
        }
    }

    private void Update()
    {
        LoadSkin();
        MineralText.text = Utility.Prefs.Load<int>("�̳׶�").ToString();
    }

    public void BuyClick(GameObject clickedObject)
    {
        string charName = clickedObject.GetComponent<TMP_Text>().text;
        Buy(charName);
    }

    public void Buy(string charName)
    {
        if (Enum.TryParse(charName, out Utility.CharList result))
        {
            hopeSkinName = result.ToString();
            Alarmcheck();
        }
    }

    public void BuyChar()
    {
        if (Utility.Prefs.CheckData(hopeSkinName))
        {
            //�̹� ���� ���
            var data = Utility.Prefs.Load<int>(hopeSkinName);
            Debug.Log("�ε��� ������" + data);
        }
        else
        {
            //�̳׶� ������������
            var minaral = (int)Utility.Prefs.Load<int>("�̳׶�");

            //�����ϱ�
            if (minaral >= 1000)
            {
                //����
                Utility.Prefs.Save<int>(hopeSkinName, 1);
                Utility.Prefs.Save<int>("�̳׶�", minaral - 1000);

                AlarmClose();
            } else
            {
                AlarmOpen();
            }
        }
    }

    public void LoadSkin()
    {
        foreach (Utility.CharList character in Enum.GetValues(typeof(Utility.CharList)))
        {
            if (Utility.Prefs.CheckData(character.ToString()))
            {
                if (useSkin == (int)character) //���� ������� ���
                {
                    buyButtons[(int)character].SetActive(false);
                    useButtons[(int)character].SetActive(false);
                    usingButtons[(int)character].SetActive(true);
                }
                else //���� �ߴµ� ��������� ���� ���
                {
                    buyButtons[(int)character].SetActive(false);
                    useButtons[(int)character].SetActive(true);
                    usingButtons[(int)character].SetActive(false);
                }
            }
            else //���� ������ ���
            {
                buyButtons[(int)character].SetActive(true);
                useButtons[(int)character].SetActive(false);
                usingButtons[(int)character].SetActive(false);
            }
        }
    }

    public void Use(GameObject clickedObject)
    {
        string charName = clickedObject.GetComponent<TMP_Text>().text;
        if (Enum.TryParse(charName, out Utility.CharList result))
        {
            UseSkin((int)result);
        }
    }

    public void UseSkin(int skinIndex)
    {
        useSkin = skinIndex;
        Utility.Prefs.Save<int>("useSkin", skinIndex);
    }

    public void AlarmClose()
    {
        alarm01Obj.SetActive(false);
        alarm02Obj.SetActive(false);
    }

    public void Alarmcheck()
    {
        alarm01Obj.SetActive(true);
    }

    public void AlarmOpen()
    {
        alarm02Obj.SetActive(true);
    }

}
