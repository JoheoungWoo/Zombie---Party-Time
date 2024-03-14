using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public AnimatorOverrideController[] animatorOverrideControllers;

    public void SetRuntimeAnimatorController()
    {

    }

    public AnimatorOverrideController GetRuntimeAnimatorController(int chIndex)
    {
        int index = 0;
        switch(chIndex)
        {
            case 0:
                index = 13;
                break;
            case 1:
                index = 14;
                break;
            case 2:
                index = 0;
                break;
            case 3:
                index = 12;
                break;
            case 4:
                index = 11;
                break;
            case 5:
                index = 6;
                break;
            case 6:
                index = 7;
                break;
            case 7:
                index = 8;
                break;
            case 8:
                index = 9;
                break;
            case 9:
                index = 2;
                break;
            case 10:
                index = 4;
                break;
            case 11:
                index = 3;
                break;
            case 12:
                index = 1;
                break;
            case 13:
                index = 5;
                break;
            case 14:
                index = 10;
                break;
        }
        return animatorOverrideControllers[index];
    } 
}
