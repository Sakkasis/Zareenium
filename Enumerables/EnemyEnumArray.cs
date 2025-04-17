using System;
using System.Collections;

public class EnemyEnumArray
{

    public int index;
    public int varIndex;
    public object variable;

    public EnemyEnumArray(int index, int varIndex, object variable)
    {

        this.index = index;
        this.varIndex = varIndex;
        this.variable = variable;

    }

}

public class EnemyEnumerable : IEnumerable
{

    private EnemyEnumArray[] variables;

    public EnemyEnumerable(EnemyEnumArray[] pArray)
    {

        variables = new EnemyEnumArray[pArray.Length];

        for (int i = 0; i < pArray.Length; i++)
        {

            variables[i] = pArray[i];

        }

    }

    IEnumerator IEnumerable.GetEnumerator()
    {

        return (IEnumerator)GetEnumerator();

    }

    public EnemyEnumerator GetEnumerator()
    {

        return new EnemyEnumerator(variables);

    }

}

public class EnemyEnumerator : IEnumerator
{

    public EnemyEnumArray[] variables;
    public int position = -1;

    public EnemyEnumerator(EnemyEnumArray[] list)
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

    public EnemyEnumArray Current
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