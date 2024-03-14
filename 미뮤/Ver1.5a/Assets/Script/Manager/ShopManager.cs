using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ShopManager : MonoBehaviour
{
    public int useSkin;             //사용중인 스킨 인덱스


    public GameObject[] buyButtons; // 구매 버튼들
    public GameObject[] useButtons; // 구매 버튼들
    public GameObject[] usingButtons; // 구매 버튼들

    public GameObject alarm01Obj;   //구매 확인
    public GameObject alarm02Obj;   //미네랄 부족

    public string hopeSkinName;     //구매 희망 스킨

    public TMP_Text MineralText; // 현재 가지고 있는 미네랄 표시

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
        MineralText.text = Utility.Prefs.Load<int>("미네랄").ToString();
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
            //이미 있을 경우
            var data = Utility.Prefs.Load<int>(hopeSkinName);
            Debug.Log("로드한 데이터" + data);
        }
        else
        {
            //미네랄 정보가져오기
            var minaral = (int)Utility.Prefs.Load<int>("미네랄");

            //구매하기
            if (minaral >= 1000)
            {
                //구매
                Utility.Prefs.Save<int>(hopeSkinName, 1);
                Utility.Prefs.Save<int>("미네랄", minaral - 1000);

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
                if (useSkin == (int)character) //현재 사용중일 경우
                {
                    buyButtons[(int)character].SetActive(false);
                    useButtons[(int)character].SetActive(false);
                    usingButtons[(int)character].SetActive(true);
                }
                else //구매 했는데 사용중이지 않을 경우
                {
                    buyButtons[(int)character].SetActive(false);
                    useButtons[(int)character].SetActive(true);
                    usingButtons[(int)character].SetActive(false);
                }
            }
            else //구매 안했을 경우
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
