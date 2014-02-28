namespace Newtonsoft.Json.Utilities
{
    using System.IO;
    using System.Web.UI;

    public static class JavaScriptUtils
  {
    public static string GetCallbackEventFunction(Page page, Control control)
    {
      string script = page.ClientScript.GetCallbackEventReference(control, "eventArgument", "eventCallback", "context", "errorCallback", true);

      script = "function(eventArgument,eventCallback,context,errorCallback){" + script + "}";

      return script;
    }

    public static string GetCallbackEventFunction(Page page, Control control, string argument)
    {
      string script = page.ClientScript.GetCallbackEventReference(control, "'" + argument + "'", "eventCallback", "context", "errorCallback", true);

      script = "function(eventCallback,context,errorCallback){" + script + "}";

      return script;
    }

    public static void WriteEscapedJavaScriptChar(TextWriter writer, char c, char delimiter)
    {
      switch (c)
      {
        case '\t':
          writer.Write(@"\t");
          break;
        case '\n':
          writer.Write(@"\n");
          break;
        case '\r':
          writer.Write(@"\r");
          break;
        case '\f':
          writer.Write(@"\f");
          break;
        case '\b':
          writer.Write(@"\b");
          break;
        case '\\':
          writer.Write(@"\\");
          break;
        //case '<':
        //case '>':
        //case '\'':
        //  StringUtils.WriteCharAsUnicode(writer, c);
        //  break;
        case '\'':
          // only escape if this charater is being used as the delimiter
          writer.Write((delimiter == '\'') ? @"\'" : @"'");
          break;
        case '"':
          // only escape if this charater is being used as the delimiter
          writer.Write((delimiter == '"') ? "\\\"" : @"""");
          break;
        default:
          if (c > '\u001f')
            writer.Write(c);
          else
            StringUtils.WriteCharAsUnicode(writer, c);
          break;
      }
    }

    public static void WriteEscapedJavaScriptString(TextWriter writer, string value, char delimiter, bool appendDelimiters)
    {
      // leading delimiter
      if (appendDelimiters)
        writer.Write(delimiter);

      if (value != null)
      {
        for (int i = 0; i < value.Length; i++)
        {
          WriteEscapedJavaScriptChar(writer, value[i], delimiter);
        }
      }

      // trailing delimiter
      if (appendDelimiters)
        writer.Write(delimiter);
    }

    public static string ToEscapedJavaScriptString(string value)
    {
      return ToEscapedJavaScriptString(value, '"', true);
    }

    public static string ToEscapedJavaScriptString(string value, char delimiter, bool appendDelimiters)
    {
      using (StringWriter w = StringUtils.CreateStringWriter(StringUtils.GetLength(value) ?? 16))
      {
        WriteEscapedJavaScriptString(w, value, delimiter, appendDelimiters);
        return w.ToString();
      }
    }
  }
}