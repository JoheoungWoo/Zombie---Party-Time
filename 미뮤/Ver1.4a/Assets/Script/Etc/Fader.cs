using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    public Image Fade; // ���̵�� �̹���
    float time = 0f;
    float f_time = 1f; // ���̵尡 �Ͼ �ð�

    public void FadeOut()
    {
        StartCoroutine(Fade_Out());
    }

    public void FadeIn()
    {
        StartCoroutine(Fade_In());
    }

    IEnumerator Fade_Out()
    {
        Fade.gameObject.SetActive(true);
        time = 0f;
        Color alpha = Fade.color;

        while (alpha.a < 1f)
        {
            time += Time.deltaTime / f_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Fade.color = alpha;
            yield return null;
        }

        /*
        time = 0f;
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / f_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            Fade.color = alpha;
            yield return null;
        }
        */

        //Fade.gameObject.SetActive(false);
        yield return null;
    }

    IEnumerator Fade_In()
    {
        Fade.gameObject.SetActive(true);
        time = 0f;
        Color alpha = Fade.color;

        /*
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / f_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Fade.color = alpha;
            yield return null;
        }
        */

        while (alpha.a > 0f)
        {
            time += Time.deltaTime / f_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            Fade.color = alpha;
            yield return null;
        }

        Fade.gameObject.SetActive(false);
        yield return null;
    }
}
