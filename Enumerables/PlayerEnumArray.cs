using System;
using System.Collections;

public class PlayerEnumArray
{

    public int varIndex;
    public object variable;

    public PlayerEnumArray(int varIndex, object variable)
    {

        this.varIndex = varIndex;
        this.variable = variable;

    }

}

public class PlayerEnumerable : IEnumerable
{

    private PlayerEnumArray[] variables;

    public PlayerEnumerable(PlayerEnumArray[] pArray)
    {

        variables = new PlayerEnumArray[pArray.Length];

        for (int i = 0; i < pArray.Length; i++)
        {

            variables[i] = pArray[i];

        }

    }

    IEnumerator IEnumerable.GetEnumerator()
    {

        return (IEnumerator)GetEnumerator();

    }

    public PlayerEnumerator GetEnumerator()
    {

        return new PlayerEnumerator(variables);

    }

}

public class PlayerEnumerator : IEnumerator
{

    public PlayerEnumArray[] variables;
    public int position = -1;

    public PlayerEnumerator(PlayerEnumArray[] list)
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

    public PlayerEnumArray Current
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