using System;
using UnityEngine;

public static class LineRendererExtensions
{
    public static void Fill(this LineRenderer line, Color color)
    {
        line.startColor = color;
        line.endColor = color;
    }
}
