using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CheckDataTypeClass
/// </summary>
public class CheckDataTypeClass
{
	public CheckDataTypeClass()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public Int64 CheckStringtoInt64Function(string oldString)
    {
        Int64 returnValue = 0;
        if (oldString != null && oldString.Length > 0)
        {
            returnValue = Int64.Parse(oldString);
        }
        else
        {
            returnValue = 0;
        }
        return returnValue;
    }
    public int CheckStringtoIntFunction(string oldString)
    {
        int returnValue = 0;
        if (oldString != null && oldString.Length > 0)
        {
            returnValue = int.Parse(oldString);
        }
        else
        {
            returnValue = 0;
        }
        return returnValue;
    }
    public char CheckStringtoCharFunction(string oldString)
    {
        char returnValue = new char();
        if (oldString != null)
        {
            returnValue = char.Parse(oldString);
        }
        return returnValue;
    }
    public int CheckIntFunction(int oldString)
    {
        int returnValue = 0;
        if (oldString != null)
        {
            returnValue = oldString;
        }
        return returnValue;
    }
    public float CheckFloatFunction(string oldString)
    {
        float returnValue = 0;
        if (oldString != null)
        {
            returnValue = float.Parse(oldString);
        }
        return returnValue;
    }
    public string CheckStringFunction(string oldString)
    {
        string returnValue = "";
        if (oldString != null)
        {
            returnValue = oldString;
        }
        return returnValue;
    }
    public DateTime CheckStringtoDateFunction(string oldString)
    {
        DateTime returnValue = new DateTime(1900, 01, 01);
        if (oldString != null && oldString.Length > 0)
        {
            returnValue = DateTime.Parse(oldString);
        }
        return returnValue;
    }

    public Decimal CheckStringtoDecimalFunction(string oldString)
    {
        Decimal returnValue = 0;
        if (oldString != null && oldString.Length > 0)
        {
            returnValue = Decimal.Parse(oldString);
        }
        else
        {
            returnValue = 0;
        }
        return returnValue;
    }
}
