// Decompiled with JetBrains decompiler
// Type: SimpleJSON.JSONNumber
// Assembly: GameLauncherUpdate, Version=1.0.0.3, Culture=neutral, PublicKeyToken=null
// MVID: E73B9648-11CF-4587-946F-7EF774FE7E27
// Assembly location: F:\Soapbox Race World\Launcher\GameLauncherUpdater.exe

using System;
using System.Globalization;
using System.Text;

namespace SimpleJSON
{
  public class JSONNumber : JSONNode
  {
    private double m_Data;

    public override JSONNodeType Tag
    {
      get
      {
        return JSONNodeType.Number;
      }
    }

    public override bool IsNumber
    {
      get
      {
        return true;
      }
    }

    public override JSONNode.Enumerator GetEnumerator()
    {
      return new JSONNode.Enumerator();
    }

    public override string Value
    {
      get
      {
        return this.m_Data.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      }
      set
      {
        double result;
        if (!double.TryParse(value, NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result))
          return;
        this.m_Data = result;
      }
    }

    public override double AsDouble
    {
      get
      {
        return this.m_Data;
      }
      set
      {
        this.m_Data = value;
      }
    }

    public override long AsLong
    {
      get
      {
        return (long) this.m_Data;
      }
      set
      {
        this.m_Data = (double) value;
      }
    }

    public JSONNumber(double aData)
    {
      this.m_Data = aData;
    }

    public JSONNumber(string aData)
    {
      this.Value = aData;
    }

    internal override void WriteToStringBuilder(
      StringBuilder aSB,
      int aIndent,
      int aIndentInc,
      JSONTextMode aMode)
    {
      aSB.Append(this.Value);
    }

    private static bool IsNumeric(object value)
    {
      switch (value)
      {
        case int _:
        case uint _:
        case float _:
        case double _:
        case Decimal _:
        case long _:
        case ulong _:
        case short _:
        case ushort _:
        case sbyte _:
          return true;
        default:
          return value is byte;
      }
    }

    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;
      if (base.Equals(obj))
        return true;
      JSONNumber jsonNumber = obj as JSONNumber;
      if ((JSONNode) jsonNumber != (object) null)
        return this.m_Data == jsonNumber.m_Data;
      return JSONNumber.IsNumeric(obj) && Convert.ToDouble(obj) == this.m_Data;
    }

    public override int GetHashCode()
    {
      return this.m_Data.GetHashCode();
    }
  }
}
