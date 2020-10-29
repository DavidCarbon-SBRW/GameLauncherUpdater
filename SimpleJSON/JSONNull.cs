// Decompiled with JetBrains decompiler
// Type: SimpleJSON.JSONNull
// Assembly: GameLauncherUpdate, Version=1.0.0.3, Culture=neutral, PublicKeyToken=null
// MVID: E73B9648-11CF-4587-946F-7EF774FE7E27
// Assembly location: F:\Soapbox Race World\Launcher\GameLauncherUpdater.exe

using System.Text;

namespace SimpleJSON
{
  public class JSONNull : JSONNode
  {
    private static JSONNull m_StaticInstance = new JSONNull();
    public static bool reuseSameInstance = true;

    public static JSONNull CreateOrGet()
    {
      return JSONNull.reuseSameInstance ? JSONNull.m_StaticInstance : new JSONNull();
    }

    private JSONNull()
    {
    }

    public override JSONNodeType Tag
    {
      get
      {
        return JSONNodeType.NullValue;
      }
    }

    public override bool IsNull
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
        return "null";
      }
      set
      {
      }
    }

    public override bool AsBool
    {
      get
      {
        return false;
      }
      set
      {
      }
    }

    public override bool Equals(object obj)
    {
      return this == obj || obj is JSONNull;
    }

    public override int GetHashCode()
    {
      return 0;
    }

    internal override void WriteToStringBuilder(
      StringBuilder aSB,
      int aIndent,
      int aIndentInc,
      JSONTextMode aMode)
    {
      aSB.Append("null");
    }
  }
}
