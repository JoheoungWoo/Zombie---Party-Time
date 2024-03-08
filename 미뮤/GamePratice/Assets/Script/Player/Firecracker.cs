using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firecracker : MonoBehaviour,IWeapon
{
    public GameObject effect;
    public const int defaultLayer = 0;
    public const int attackLayer = 0;


    private void Awake()
    {
        effect = transform.GetChild(0).gameObject;
    }

    public void Attack(string attackDirect)
    {
        switch (attackDirect)
        {
            case "up":
                transform.localPosition = new Vector3(0f, 0.6f, -1f);
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                break;
            case "down":
                transform.localPosition = new Vector3(0f, -0.6f, 0f);
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                break;
            case "left":
                transform.localPosition = new Vector3(-0.75f, 0.2f, 0f);
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                break;
            case "right":
                transform.localPosition = new Vector3(0.75f,0.2f,0f);
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                break;
            default:
                break;
        }
    }
}
