using System;
using System.Globalization;
using UnityEngine;

public class MathService : IMathService
{

    public float RoundFloatToDecimalPoint(float roundedFloat, int decimalPoints)
    {

        try
        {

            CultureInfo cultureInfo = CultureInfo.CurrentCulture;

            string roundingMultiplier = "1";
            for (int i = 1; i <= decimalPoints; i++)
            {

                roundingMultiplier = roundingMultiplier + "0";

            }

            float result = float.Parse(roundingMultiplier);
            roundedFloat *= result;
            int roundedInt = Mathf.RoundToInt(roundedFloat);
            roundedFloat = roundedInt;

            roundingMultiplier = "0" + cultureInfo.NumberFormat.NumberDecimalSeparator;
            for (int i = 1; i < decimalPoints; i++)
            {

                roundingMultiplier = roundingMultiplier + "0";

            }
            roundingMultiplier = roundingMultiplier + "1";

            result = float.Parse(roundingMultiplier, NumberStyles.AllowDecimalPoint);
            roundedFloat *= result;

            return roundedFloat;

        }
        catch (Exception e)
        {

            Debug.LogError("!ERROR! " + e.Message + " | " + e.StackTrace);
            throw e;

        }

    }

    public float MakeFloatPositive(float f)
    {

        if (f < 0f)
        {

            return f * -1f;

        }
        else
        {

            return f;

        }

    }

    public float MakeFloatNegative(float f)
    {

        if (f > 0f)
        {

            return f * -1f;

        }
        else
        {

            return f;

        }

    }

    public int MakeIntPositive(int i)
    {

        if (i < 0)
        {

            return i * -1;

        }
        else
        {

            return i;

        }

    }

    public int MakeIntNegative(int i)
    {

        if (i > 0)
        {

            return i * -1;

        }
        else
        {

            return i;

        }

    }

    public Vector3 MakeVectorPositive(Vector3 v)
    {

        Vector3 proxy;

        if (v.x < 0f)
        {

            proxy.x = v.x * -1f;

        }
        else
        {

            proxy.x = v.x;

        }

        if (v.y < 0f)
        {

            proxy.y = v.y * -1f;

        }
        else
        {

            proxy.y = v.y;

        }

        if (v.z < 0f)
        {

            proxy.z = v.z * -1f;

        }
        else
        {

            proxy.z = v.z;

        }

        return proxy;

    }

    public Vector3 MakeVectorNegative(Vector3 v)
    {

        Vector3 proxy;

        if (v.x > 0f)
        {

            proxy.x = v.x * -1f;

        }
        else
        {

            proxy.x = v.x;

        }

        if (v.y > 0f)
        {

            proxy.y = v.y * -1f;

        }
        else
        {

            proxy.y = v.y;

        }

        if (v.z > 0f)
        {

            proxy.z = v.z * -1f;

        }
        else
        {

            proxy.z = v.z;

        }

        return proxy;

    }

}