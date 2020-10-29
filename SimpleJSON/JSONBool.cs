// Decompiled with JetBrains decompiler
// Type: SimpleJSON.JSONBool
// Assembly: GameLauncherUpdate, Version=1.0.0.3, Culture=neutral, PublicKeyToken=null
// MVID: E73B9648-11CF-4587-946F-7EF774FE7E27
// Assembly location: F:\Soapbox Race World\Launcher\GameLauncherUpdater.exe

using System.Text;

namespace SimpleJSON
{
  public class JSONBool : JSONNode
  {
    private bool m_Data;

    public override JSONNodeType Tag
    {
      get
      {
        return JSONNodeType.Boolean;
      }
    }

    public override bool IsBoolean
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
        return this.m_Data.ToString();
      }
      set
      {
        bool result;
        if (!bool.TryParse(value, out result))
          return;
        this.m_Data = result;
      }
    }

    public override bool AsBool
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

    public JSONBool(bool aData)
    {
      this.m_Data = aData;
    }

    public JSONBool(string aData)
    {
      this.Value = aData;
    }

    internal override void WriteToStringBuilder(
      StringBuilder aSB,
      int aIndent,
      int aIndentInc,
      JSONTextMode aMode)
    {
      aSB.Append(this.m_Data ? "true" : "false");
    }

    public override bool Equals(object obj)
    {
      return obj != null && obj is bool flag && this.m_Data == flag;
    }

    public override int GetHashCode()
    {
      return this.m_Data.GetHashCode();
    }
  }
}
