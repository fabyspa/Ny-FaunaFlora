using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;

public static class Convert_coordinates
{
    public static string coordinates;

    public static double xfrom1 = 6.362218;
    public static double xto1 = 18.785160;
    public static double xfrom2 = -2.37;
    public static double xto2 = 2.31;

    public static double yfrom1 = 37.027325;
    public static double yto1 = 46.574408;
    public static double yfrom2 = -0.65;
    public static double yto2 = 4.46;

    public static int decimalp = 5;

    public static float[] remapLatLng(string coord)
    {
        string[] subs = coord.Split(',');
        float[] xy = new float[2];
        double v;
        for (int i = 0; i < subs.Length; i++)
            if (double.TryParse(subs[i], out v))
            {
                {
                    v = double.Parse(subs[i], System.Globalization.CultureInfo.InvariantCulture);

                    if (i == 0)
                    {
                        xy[i] = ExtensionMethods.Remap(v, yfrom1, yto1, yfrom2, yto2, decimalp);
                        // Debug.Log(v);
                    }
                    else
                    {
                        xy[i] = ExtensionMethods.Remap(v, xfrom1, xto1, xfrom2, xto2, decimalp);
                    }
                }
     
        }
        return xy;
    }
}



public static class ExtensionMethods
{

    public static float Remap(double sourceNumber, double fromA, double fromB, double toA, double toB, int decimalPrecision)
    {
        double deltaA = (fromB - fromA);
        double deltaB = (toB - toA);
        double scale = deltaB / deltaA;
        double negA = -1 * fromA;
        double offset = (negA * scale) + toA;
        double finalNumber = (sourceNumber * scale) + offset;
        int calcScale = (int)Math.Pow(10, decimalPrecision);
        return (float)Math.Round(finalNumber * calcScale) / calcScale;
    }
}

