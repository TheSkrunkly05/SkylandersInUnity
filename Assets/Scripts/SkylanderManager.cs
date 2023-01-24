using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SkylanderManager : MonoBehaviour
{
    public Portal portal;

    [Header("Skylander Controls")]
    public int characterCount = 0;

    [Header("Portal Controls")]
    public bool solidColor;

    [Header("Portal Material")]
    public MeshRenderer portalMat;
    public Color[] colors;
    public Color lerpedColor;
    public float colorTime;
    public float colorChangeTime;
    public Color currColor;
    public Color nextColor;
    public int colorNum;


    private void Start()
    {
        portal = Portal.Instance;
        portal.Ready();
        portal.Activate();

        currColor = colors[colors.Length-1];
        nextColor = colors[0];
    }

    private void Update()
    {
        if(!solidColor)
        {
            portalColorChange();
        }
    }

    private void portalColorChange()
    {
        // PORTAL CHANGE COLOR //
        portal.SetColor((byte)(lerpedColor.r * 255f), (byte)(lerpedColor.g * 255f), (byte)(lerpedColor.b * 255f));

        // MODEL CHANGE COLOR //
        portalMat.material.color = lerpedColor;

        // COLOR CHANGE CODE
        colorTime += Time.deltaTime/colorChangeTime;
        lerpedColor = Color.Lerp(currColor, nextColor, Mathf.PingPong(colorTime, 1));
        if(colorTime >= 1)
        {
            colorTime = 0;
            colorNum++;
            if(colorNum >= colors.Length)
            {
                colorNum = 0;
            }
            currColor = nextColor;
            nextColor = colors[colorNum];
        }
    }
}
