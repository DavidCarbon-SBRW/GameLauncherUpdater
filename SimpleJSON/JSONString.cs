// Decompiled with JetBrains decompiler
// Type: SimpleJSON.JSONString
// Assembly: GameLauncherUpdate, Version=1.0.0.3, Culture=neutral, PublicKeyToken=null
// MVID: E73B9648-11CF-4587-946F-7EF774FE7E27
// Assembly location: F:\Soapbox Race World\Launcher\GameLauncherUpdater.exe

using System.Text;

namespace SimpleJSON
{
  public class JSONString : JSONNode
  {
    private string m_Data;

    public override JSONNodeType Tag
    {
      get
      {
        return JSONNodeType.String;
      }
    }

    public override bool IsString
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
        return this.m_Data;
      }
      set
      {
        this.m_Data = value;
      }
    }

    public JSONString(string aData)
    {
      this.m_Data = aData;
    }

    internal override void WriteToStringBuilder(
      StringBuilder aSB,
      int aIndent,
      int aIndentInc,
      JSONTextMode aMode)
    {
      aSB.Append('"').Append(JSONNode.Escape(this.m_Data)).Append('"');
    }

    public override bool Equals(object obj)
    {
      if (base.Equals(obj))
        return true;
      if (obj is string str)
        return this.m_Data == str;
      JSONString jsonString = obj as JSONString;
      return (JSONNode) jsonString != (object) null && this.m_Data == jsonString.m_Data;
    }

    public override int GetHashCode()
    {
      return this.m_Data.GetHashCode();
    }
  }
}
