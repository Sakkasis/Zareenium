using System;
using System.Collections;

public class CustomEnumArray
{

    public string variableName;
    public object variable;

    public CustomEnumArray(string variableName, object variable)
    {

        this.variableName = variableName;
        this.variable = variable;

    }

}

public class CustomEnumerable : IEnumerable
{

    private CustomEnumArray[] variables;

    public CustomEnumerable(CustomEnumArray[] pArray)
    {

        variables = new CustomEnumArray[pArray.Length];

        for (int i = 0; i < pArray.Length; i++)
        {

            variables[i] = pArray[i];

        }

    }

    IEnumerator IEnumerable.GetEnumerator()
    {

        return (IEnumerator)GetEnumerator();

    }

    public CustomEnumerator GetEnumerator()
    {

        return new CustomEnumerator(variables);

    }

}

public class CustomEnumerator : IEnumerator
{

    public CustomEnumArray[] variables;
    public int position = -1;

    public CustomEnumerator(CustomEnumArray[] list)
    {

        variables = list;

    }

    public bool MoveNext()
    {

        position++;
        return (position < variables.Length);

    }

    public void Reset()
    {

        position = -1;

    }

    object IEnumerator.Current
    {

        get
        {

            return Current;

        }

    }

    public CustomEnumArray Current
    {

        get
        {

            try
            {

                return variables[position];

            }
            catch (IndexOutOfRangeException)
            {

                throw new InvalidOperationException();

            }

        }

    }

}