using System;

[Serializable]
public struct RangeFloat
{
    public float Min;
    public float Max;

    public static RangeFloat operator+(RangeFloat a, RangeFloat b)
    {
        RangeFloat result = new RangeFloat
        {
            Min = a.Min + b.Min,
            Max = a.Max + b.Max
        };
        return result;
    }
}