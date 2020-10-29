// Decompiled with JetBrains decompiler
// Type: SimpleJSON.JSONNode
// Assembly: GameLauncherUpdate, Version=1.0.0.3, Culture=neutral, PublicKeyToken=null
// MVID: E73B9648-11CF-4587-946F-7EF774FE7E27
// Assembly location: F:\Soapbox Race World\Launcher\GameLauncherUpdater.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SimpleJSON
{
  public abstract class JSONNode
  {
    public static bool forceASCII;
    public static bool longAsString;
    [ThreadStatic]
    private static StringBuilder m_EscapeBuilder;

    public abstract JSONNodeType Tag { get; }

    public virtual JSONNode this[int aIndex]
    {
      get
      {
        return (JSONNode) null;
      }
      set
      {
      }
    }

    public virtual JSONNode this[string aKey]
    {
      get
      {
        return (JSONNode) null;
      }
      set
      {
      }
    }

    public virtual string Value
    {
      get
      {
        return "";
      }
      set
      {
      }
    }

    public virtual int Count
    {
      get
      {
        return 0;
      }
    }

    public virtual bool IsNumber
    {
      get
      {
        return false;
      }
    }

    public virtual bool IsString
    {
      get
      {
        return false;
      }
    }

    public virtual bool IsBoolean
    {
      get
      {
        return false;
      }
    }

    public virtual bool IsNull
    {
      get
      {
        return false;
      }
    }

    public virtual bool IsArray
    {
      get
      {
        return false;
      }
    }

    public virtual bool IsObject
    {
      get
      {
        return false;
      }
    }

    public virtual bool Inline
    {
      get
      {
        return false;
      }
      set
      {
      }
    }

    public virtual void Add(string aKey, JSONNode aItem)
    {
    }

    public virtual void Add(JSONNode aItem)
    {
      this.Add("", aItem);
    }

    public virtual JSONNode Remove(string aKey)
    {
      return (JSONNode) null;
    }

    public virtual JSONNode Remove(int aIndex)
    {
      return (JSONNode) null;
    }

    public virtual JSONNode Remove(JSONNode aNode)
    {
      return aNode;
    }

    public virtual IEnumerable<JSONNode> Children
    {
      get
      {
        yield break;
      }
    }

    public IEnumerable<JSONNode> DeepChildren
    {
      get
      {
        foreach (JSONNode child in this.Children)
        {
          foreach (JSONNode deepChild in child.DeepChildren)
            yield return deepChild;
        }
      }
    }

    public override string ToString()
    {
      StringBuilder aSB = new StringBuilder();
      this.WriteToStringBuilder(aSB, 0, 0, JSONTextMode.Compact);
      return aSB.ToString();
    }

    public virtual string ToString(int aIndent)
    {
      StringBuilder aSB = new StringBuilder();
      this.WriteToStringBuilder(aSB, 0, aIndent, JSONTextMode.Indent);
      return aSB.ToString();
    }

    internal abstract void WriteToStringBuilder(
      StringBuilder aSB,
      int aIndent,
      int aIndentInc,
      JSONTextMode aMode);

    public abstract JSONNode.Enumerator GetEnumerator();

    public IEnumerable<KeyValuePair<string, JSONNode>> Linq
    {
      get
      {
        return (IEnumerable<KeyValuePair<string, JSONNode>>) new JSONNode.LinqEnumerator(this);
      }
    }

    public JSONNode.KeyEnumerator Keys
    {
      get
      {
        return new JSONNode.KeyEnumerator(this.GetEnumerator());
      }
    }

    public JSONNode.ValueEnumerator Values
    {
      get
      {
        return new JSONNode.ValueEnumerator(this.GetEnumerator());
      }
    }

    public virtual double AsDouble
    {
      get
      {
        double result = 0.0;
        return double.TryParse(this.Value, NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result) ? result : 0.0;
      }
      set
      {
        this.Value = value.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      }
    }

    public virtual int AsInt
    {
      get
      {
        return (int) this.AsDouble;
      }
      set
      {
        this.AsDouble = (double) value;
      }
    }

    public virtual float AsFloat
    {
      get
      {
        return (float) this.AsDouble;
      }
      set
      {
        this.AsDouble = (double) value;
      }
    }

    public virtual bool AsBool
    {
      get
      {
        bool result = false;
        return bool.TryParse(this.Value, out result) ? result : !string.IsNullOrEmpty(this.Value);
      }
      set
      {
        this.Value = value ? "true" : "false";
      }
    }

    public virtual long AsLong
    {
      get
      {
        long result = 0;
        return long.TryParse(this.Value, out result) ? result : 0L;
      }
      set
      {
        this.Value = value.ToString();
      }
    }

    public virtual JSONArray AsArray
    {
      get
      {
        return this as JSONArray;
      }
    }

    public virtual JSONObject AsObject
    {
      get
      {
        return this as JSONObject;
      }
    }

    public static implicit operator JSONNode(string s)
    {
      return (JSONNode) new JSONString(s);
    }

    public static implicit operator string(JSONNode d)
    {
      return !(d == (object) null) ? d.Value : (string) null;
    }

    public static implicit operator JSONNode(double n)
    {
      return (JSONNode) new JSONNumber(n);
    }

    public static implicit operator double(JSONNode d)
    {
      return !(d == (object) null) ? d.AsDouble : 0.0;
    }

    public static implicit operator JSONNode(float n)
    {
      return (JSONNode) new JSONNumber((double) n);
    }

    public static implicit operator float(JSONNode d)
    {
      return !(d == (object) null) ? d.AsFloat : 0.0f;
    }

    public static implicit operator JSONNode(int n)
    {
      return (JSONNode) new JSONNumber((double) n);
    }

    public static implicit operator int(JSONNode d)
    {
      return !(d == (object) null) ? d.AsInt : 0;
    }

    public static implicit operator JSONNode(long n)
    {
      return JSONNode.longAsString ? (JSONNode) new JSONString(n.ToString()) : (JSONNode) new JSONNumber((double) n);
    }

    public static implicit operator long(JSONNode d)
    {
      return !(d == (object) null) ? d.AsLong : 0L;
    }

    public static implicit operator JSONNode(bool b)
    {
      return (JSONNode) new JSONBool(b);
    }

    public static implicit operator bool(JSONNode d)
    {
      return !(d == (object) null) && d.AsBool;
    }

    public static implicit operator JSONNode(KeyValuePair<string, JSONNode> aKeyValue)
    {
      return aKeyValue.Value;
    }

    public static bool operator ==(JSONNode a, object b)
    {
      if ((object) a == b)
        return true;
      bool flag1 = a is JSONNull || (object) a == null || a is JSONLazyCreator;
      int num;
      switch (b)
      {
        case JSONNull _:
        case null:
          num = 1;
          break;
        default:
          num = b is JSONLazyCreator ? 1 : 0;
          break;
      }
      bool flag2 = num != 0;
      if (flag1 & flag2)
        return true;
      return !flag1 && a.Equals(b);
    }

    public static bool operator !=(JSONNode a, object b)
    {
      return !(a == b);
    }

    public override bool Equals(object obj)
    {
      return (object) this == obj;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    internal static StringBuilder EscapeBuilder
    {
      get
      {
        if (JSONNode.m_EscapeBuilder == null)
          JSONNode.m_EscapeBuilder = new StringBuilder();
        return JSONNode.m_EscapeBuilder;
      }
    }

    internal static string Escape(string aText)
    {
      StringBuilder escapeBuilder = JSONNode.EscapeBuilder;
      escapeBuilder.Length = 0;
      if (escapeBuilder.Capacity < aText.Length + aText.Length / 10)
        escapeBuilder.Capacity = aText.Length + aText.Length / 10;
      foreach (char ch in aText)
      {
        switch (ch)
        {
          case '\b':
            escapeBuilder.Append("\\b");
            break;
          case '\t':
            escapeBuilder.Append("\\t");
            break;
          case '\n':
            escapeBuilder.Append("\\n");
            break;
          case '\f':
            escapeBuilder.Append("\\f");
            break;
          case '\r':
            escapeBuilder.Append("\\r");
            break;
          case '"':
            escapeBuilder.Append("\\\"");
            break;
          case '\\':
            escapeBuilder.Append("\\\\");
            break;
          default:
            if (ch < ' ' || JSONNode.forceASCII && ch > '\x007F')
            {
              ushort num = (ushort) ch;
              escapeBuilder.Append("\\u").Append(num.ToString("X4"));
              break;
            }
            escapeBuilder.Append(ch);
            break;
        }
      }
      string str = escapeBuilder.ToString();
      escapeBuilder.Length = 0;
      return str;
    }

    private static JSONNode ParseElement(string token, bool quoted)
    {
      if (quoted)
        return (JSONNode) token;
      string lower = token.ToLower();
      if (lower == "false" || lower == "true")
        return (JSONNode) (lower == "true");
      if (lower == "null")
        return (JSONNode) JSONNull.CreateOrGet();
      double result;
      return double.TryParse(token, NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result) ? (JSONNode) result : (JSONNode) token;
    }

    public static JSONNode Parse(string aJSON)
    {
      Stack<JSONNode> jsonNodeStack = new Stack<JSONNode>();
      JSONNode jsonNode = (JSONNode) null;
      int index = 0;
      StringBuilder stringBuilder = new StringBuilder();
      string aKey = "";
      bool flag = false;
      bool quoted = false;
      for (; index < aJSON.Length; ++index)
      {
        switch (aJSON[index])
        {
          case '\t':
          case ' ':
            if (flag)
            {
              stringBuilder.Append(aJSON[index]);
              continue;
            }
            continue;
          case '\n':
          case '\r':
            continue;
          case '"':
            flag = !flag;
            quoted |= flag;
            continue;
          case ',':
            if (flag)
            {
              stringBuilder.Append(aJSON[index]);
              continue;
            }
            if (stringBuilder.Length > 0 | quoted)
              jsonNode.Add(aKey, JSONNode.ParseElement(stringBuilder.ToString(), quoted));
            aKey = "";
            stringBuilder.Length = 0;
            quoted = false;
            continue;
          case ':':
            if (flag)
            {
              stringBuilder.Append(aJSON[index]);
              continue;
            }
            aKey = stringBuilder.ToString();
            stringBuilder.Length = 0;
            quoted = false;
            continue;
          case '[':
            if (flag)
            {
              stringBuilder.Append(aJSON[index]);
              continue;
            }
            jsonNodeStack.Push((JSONNode) new JSONArray());
            if (jsonNode != (object) null)
              jsonNode.Add(aKey, jsonNodeStack.Peek());
            aKey = "";
            stringBuilder.Length = 0;
            jsonNode = jsonNodeStack.Peek();
            continue;
          case '\\':
            ++index;
            if (flag)
            {
              char ch = aJSON[index];
              switch (ch)
              {
                case 'b':
                  stringBuilder.Append('\b');
                  continue;
                case 'f':
                  stringBuilder.Append('\f');
                  continue;
                case 'n':
                  stringBuilder.Append('\n');
                  continue;
                case 'r':
                  stringBuilder.Append('\r');
                  continue;
                case 't':
                  stringBuilder.Append('\t');
                  continue;
                case 'u':
                  string s = aJSON.Substring(index + 1, 4);
                  stringBuilder.Append((char) int.Parse(s, NumberStyles.AllowHexSpecifier));
                  index += 4;
                  continue;
                default:
                  stringBuilder.Append(ch);
                  continue;
              }
            }
            else
              continue;
          case ']':
          case '}':
            if (flag)
            {
              stringBuilder.Append(aJSON[index]);
              continue;
            }
            if (jsonNodeStack.Count == 0)
              throw new Exception("JSON Parse: Too many closing brackets");
            jsonNodeStack.Pop();
            if (stringBuilder.Length > 0 | quoted)
              jsonNode.Add(aKey, JSONNode.ParseElement(stringBuilder.ToString(), quoted));
            quoted = false;
            aKey = "";
            stringBuilder.Length = 0;
            if (jsonNodeStack.Count > 0)
            {
              jsonNode = jsonNodeStack.Peek();
              continue;
            }
            continue;
          case '{':
            if (flag)
            {
              stringBuilder.Append(aJSON[index]);
              continue;
            }
            jsonNodeStack.Push((JSONNode) new JSONObject());
            if (jsonNode != (object) null)
              jsonNode.Add(aKey, jsonNodeStack.Peek());
            aKey = "";
            stringBuilder.Length = 0;
            jsonNode = jsonNodeStack.Peek();
            continue;
          default:
            stringBuilder.Append(aJSON[index]);
            continue;
        }
      }
      if (flag)
        throw new Exception("JSON Parse: Quotation marks seems to be messed up.");
      return jsonNode == (object) null ? JSONNode.ParseElement(stringBuilder.ToString(), quoted) : jsonNode;
    }

    public struct Enumerator
    {
      private JSONNode.Enumerator.Type type;
      private Dictionary<string, JSONNode>.Enumerator m_Object;
      private List<JSONNode>.Enumerator m_Array;

      public bool IsValid
      {
        get
        {
          return (uint) this.type > 0U;
        }
      }

      public Enumerator(List<JSONNode>.Enumerator aArrayEnum)
      {
        this.type = JSONNode.Enumerator.Type.Array;
        this.m_Object = new Dictionary<string, JSONNode>.Enumerator();
        this.m_Array = aArrayEnum;
      }

      public Enumerator(Dictionary<string, JSONNode>.Enumerator aDictEnum)
      {
        this.type = JSONNode.Enumerator.Type.Object;
        this.m_Object = aDictEnum;
        this.m_Array = new List<JSONNode>.Enumerator();
      }

      public KeyValuePair<string, JSONNode> Current
      {
        get
        {
          if (this.type == JSONNode.Enumerator.Type.Array)
            return new KeyValuePair<string, JSONNode>(string.Empty, this.m_Array.Current);
          return this.type == JSONNode.Enumerator.Type.Object ? this.m_Object.Current : new KeyValuePair<string, JSONNode>(string.Empty, (JSONNode) null);
        }
      }

      public bool MoveNext()
      {
        if (this.type == JSONNode.Enumerator.Type.Array)
          return this.m_Array.MoveNext();
        return this.type == JSONNode.Enumerator.Type.Object && this.m_Object.MoveNext();
      }

      private enum Type
      {
        None,
        Array,
        Object,
      }
    }

    public struct ValueEnumerator
    {
      private JSONNode.Enumerator m_Enumerator;

      public ValueEnumerator(List<JSONNode>.Enumerator aArrayEnum)
        : this(new JSONNode.Enumerator(aArrayEnum))
      {
      }

      public ValueEnumerator(Dictionary<string, JSONNode>.Enumerator aDictEnum)
        : this(new JSONNode.Enumerator(aDictEnum))
      {
      }

      public ValueEnumerator(JSONNode.Enumerator aEnumerator)
      {
        this.m_Enumerator = aEnumerator;
      }

      public JSONNode Current
      {
        get
        {
          return this.m_Enumerator.Current.Value;
        }
      }

      public bool MoveNext()
      {
        return this.m_Enumerator.MoveNext();
      }

      public JSONNode.ValueEnumerator GetEnumerator()
      {
        return this;
      }
    }

    public struct KeyEnumerator
    {
      private JSONNode.Enumerator m_Enumerator;

      public KeyEnumerator(List<JSONNode>.Enumerator aArrayEnum)
        : this(new JSONNode.Enumerator(aArrayEnum))
      {
      }

      public KeyEnumerator(Dictionary<string, JSONNode>.Enumerator aDictEnum)
        : this(new JSONNode.Enumerator(aDictEnum))
      {
      }

      public KeyEnumerator(JSONNode.Enumerator aEnumerator)
      {
        this.m_Enumerator = aEnumerator;
      }

      public JSONNode Current
      {
        get
        {
          return (JSONNode) this.m_Enumerator.Current.Key;
        }
      }

      public bool MoveNext()
      {
        return this.m_Enumerator.MoveNext();
      }

      public JSONNode.KeyEnumerator GetEnumerator()
      {
        return this;
      }
    }

    public class LinqEnumerator : IEnumerator<KeyValuePair<string, JSONNode>>, IDisposable, IEnumerator, IEnumerable<KeyValuePair<string, JSONNode>>, IEnumerable
    {
      private JSONNode m_Node;
      private JSONNode.Enumerator m_Enumerator;

      internal LinqEnumerator(JSONNode aNode)
      {
        this.m_Node = aNode;
        if (!(this.m_Node != (object) null))
          return;
        this.m_Enumerator = this.m_Node.GetEnumerator();
      }

      public KeyValuePair<string, JSONNode> Current
      {
        get
        {
          return this.m_Enumerator.Current;
        }
      }

      object IEnumerator.Current
      {
        get
        {
          return (object) this.m_Enumerator.Current;
        }
      }

      public bool MoveNext()
      {
        return this.m_Enumerator.MoveNext();
      }

      public void Dispose()
      {
        this.m_Node = (JSONNode) null;
        this.m_Enumerator = new JSONNode.Enumerator();
      }

      public IEnumerator<KeyValuePair<string, JSONNode>> GetEnumerator()
      {
        return (IEnumerator<KeyValuePair<string, JSONNode>>) new JSONNode.LinqEnumerator(this.m_Node);
      }

      public void Reset()
      {
        if (!(this.m_Node != (object) null))
          return;
        this.m_Enumerator = this.m_Node.GetEnumerator();
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        return (IEnumerator) new JSONNode.LinqEnumerator(this.m_Node);
      }
    }
  }
}
