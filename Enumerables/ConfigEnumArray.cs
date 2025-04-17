using System;
using System.Collections;

public class ConfigEnumArray
{

    public string variableName;
    public object variable;

    public ConfigEnumArray(string variableName, object variable)
    {

        this.variableName = variableName;
        this.variable = variable;

    }

}

public class ConfigEnumerable : IEnumerable
{

    private ConfigEnumArray[] variables;

    public ConfigEnumerable(ConfigEnumArray[] pArray)
    {

        variables = new ConfigEnumArray[pArray.Length];

        for (int i = 0; i < pArray.Length; i++)
        {

            variables[i] = pArray[i];

        }

    }

    IEnumerator IEnumerable.GetEnumerator()
    {

        return (IEnumerator)GetEnumerator();

    }

    public ConfigEnumerator GetEnumerator()
    {

        return new ConfigEnumerator(variables);

    }

}

public class ConfigEnumerator : IEnumerator
{

    public ConfigEnumArray[] variables;
    public int position = -1;

    public ConfigEnumerator(ConfigEnumArray[] list)
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

    public ConfigEnumArray Current
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