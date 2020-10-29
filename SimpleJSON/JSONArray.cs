// Decompiled with JetBrains decompiler
// Type: SimpleJSON.JSONArray
// Assembly: GameLauncherUpdate, Version=1.0.0.3, Culture=neutral, PublicKeyToken=null
// MVID: E73B9648-11CF-4587-946F-7EF774FE7E27
// Assembly location: F:\Soapbox Race World\Launcher\GameLauncherUpdater.exe

using System.Collections.Generic;
using System.Text;

namespace SimpleJSON
{
  public class JSONArray : JSONNode
  {
    private List<JSONNode> m_List = new List<JSONNode>();
    private bool inline;

    public override bool Inline
    {
      get
      {
        return this.inline;
      }
      set
      {
        this.inline = value;
      }
    }

    public override JSONNodeType Tag
    {
      get
      {
        return JSONNodeType.Array;
      }
    }

    public override bool IsArray
    {
      get
      {
        return true;
      }
    }

    public override JSONNode.Enumerator GetEnumerator()
    {
      return new JSONNode.Enumerator(this.m_List.GetEnumerator());
    }

    public override JSONNode this[int aIndex]
    {
      get
      {
        return aIndex < 0 || aIndex >= this.m_List.Count ? (JSONNode) new JSONLazyCreator((JSONNode) this) : this.m_List[aIndex];
      }
      set
      {
        if (value == (object) null)
          value = (JSONNode) JSONNull.CreateOrGet();
        if (aIndex < 0 || aIndex >= this.m_List.Count)
          this.m_List.Add(value);
        else
          this.m_List[aIndex] = value;
      }
    }

    public override JSONNode this[string aKey]
    {
      get
      {
        return (JSONNode) new JSONLazyCreator((JSONNode) this);
      }
      set
      {
        if (value == (object) null)
          value = (JSONNode) JSONNull.CreateOrGet();
        this.m_List.Add(value);
      }
    }

    public override int Count
    {
      get
      {
        return this.m_List.Count;
      }
    }

    public override void Add(string aKey, JSONNode aItem)
    {
      if (aItem == (object) null)
        aItem = (JSONNode) JSONNull.CreateOrGet();
      this.m_List.Add(aItem);
    }

    public override JSONNode Remove(int aIndex)
    {
      if (aIndex < 0 || aIndex >= this.m_List.Count)
        return (JSONNode) null;
      JSONNode jsonNode = this.m_List[aIndex];
      this.m_List.RemoveAt(aIndex);
      return jsonNode;
    }

    public override JSONNode Remove(JSONNode aNode)
    {
      this.m_List.Remove(aNode);
      return aNode;
    }

    public override IEnumerable<JSONNode> Children
    {
      get
      {
        foreach (JSONNode jsonNode in this.m_List)
          yield return jsonNode;
      }
    }

    internal override void WriteToStringBuilder(
      StringBuilder aSB,
      int aIndent,
      int aIndentInc,
      JSONTextMode aMode)
    {
      aSB.Append('[');
      int count = this.m_List.Count;
      if (this.inline)
        aMode = JSONTextMode.Compact;
      for (int index = 0; index < count; ++index)
      {
        if (index > 0)
          aSB.Append(',');
        if (aMode == JSONTextMode.Indent)
          aSB.AppendLine();
        if (aMode == JSONTextMode.Indent)
          aSB.Append(' ', aIndent + aIndentInc);
        this.m_List[index].WriteToStringBuilder(aSB, aIndent + aIndentInc, aIndentInc, aMode);
      }
      if (aMode == JSONTextMode.Indent)
        aSB.AppendLine().Append(' ', aIndent);
      aSB.Append(']');
    }
  }
}
