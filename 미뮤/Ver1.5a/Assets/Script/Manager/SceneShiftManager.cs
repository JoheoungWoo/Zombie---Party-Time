using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneShiftManager : MonoBehaviour
{
    public int Con; // 맵 컨트롤 변수

    public void Awake()
    {
        Mapshift();
    }

    public void Mapshift()
    {
        switch (Con)
        {
            case 1:
                Invoke("Title", 3f);
                break;

            case 2:
                Invoke("Stage0", 5f);
                break;

        }
    }

    public void Title()
    {
        SceneManager.LoadScene("Title");
    }

    public void Shop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void Emblem()
    {
        SceneManager.LoadScene("Emblem");
    }

    public void Ranking()
    {
        SceneManager.LoadScene("Ranking");
    }

    public void GameStart01()
    {
        Invoke("GameStart02", 1.5f);
    }

    public void GameStart02()
    {
        SceneManager.LoadScene("Stage0Ready");
    }
    public void Stage0()
    {
        SceneManager.LoadScene("Stage0");
    }
}
