using UnityEngine;

public interface IMathService
{

    public float RoundFloatToDecimalPoint(float roundedFloat, int decimalPoints);

    public float MakeFloatPositive(float f);

    public float MakeFloatNegative(float f);

    public int MakeIntPositive(int i);

    public int MakeIntNegative(int i);

    public Vector3 MakeVectorPositive(Vector3 v);

    public Vector3 MakeVectorNegative(Vector3 v);

}