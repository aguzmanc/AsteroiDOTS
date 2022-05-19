using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpAtLimits : MonoBehaviour
{
    void Update()
    {
        float x = transform.position.x;
        float y = transform.position.y;

        if(x > ScreenLimits.rightLimit)
            x = ScreenLimits.leftLimit;

        if(x < ScreenLimits.leftLimit)
            x = ScreenLimits.rightLimit;

        if(y > ScreenLimits.upLimit)
            y = ScreenLimits.downLimit;

        if(y < ScreenLimits.downLimit)
            y = ScreenLimits.upLimit;

        transform.position = new Vector3(x,y,0f);
    }
}
