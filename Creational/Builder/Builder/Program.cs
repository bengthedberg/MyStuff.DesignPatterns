using System.Text;

var builder = new HtmlBuilder("ul");
builder.AddChild("li", "hello");
builder.AddChild("li", "world");
Console.WriteLine(builder.ToString());
builder.Clear();

var fluentBuilder = new HtmlFluentBuilder("ul")
    .AddChild("li", "hello")
    .AddChild("li", "world");
Console.WriteLine(fluentBuilder.ToString());
builder.Clear();


/// <summary>
/// Represents an HTML element with a name, text and a list of child elements of type HTML element.
/// </summary>
public class HTMLElement
{
    public string Name { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public List<HTMLElement> Elements { get; set; } = new List<HTMLElement>();

    private const int indentSize = 2;

    public HTMLElement()
    {
    }

    public HTMLElement(string name, string text)
    {
        Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
        Text = text ?? throw new ArgumentNullException(paramName: nameof(text)); 
    }

    /// <summary>
    /// Returns the HTML element as a string using indentation.
    /// </summary>    
    private string ToStringImpl(int indent)
    {
        var sb = new StringBuilder();
        var i = new string(' ', indentSize * indent);
        sb.AppendLine($"{i}<{Name}>");

        if (!string.IsNullOrWhiteSpace(Text))
        {
            sb.Append(new string(' ', indentSize * (indent + 1)));
            sb.AppendLine(Text);
        }

        foreach (var e in Elements)
        {
            sb.Append(e.ToStringImpl(indent + 1));
        }

        sb.AppendLine($"{i}</{Name}>");
        return sb.ToString();
    }

    public override string ToString() =>  ToStringImpl(0);
}


/// <summary>
/// Represents an HTML builder that creates a tree of HTML elements with a root name.
/// </summary>
public class HtmlBuilder
{
    private readonly string _rootName;
    HTMLElement root = new HTMLElement();

    public HtmlBuilder(string rootName)
    {
        _rootName = rootName ?? throw new ArgumentNullException(paramName: nameof(rootName));
        root.Name = rootName;
    }

    public void AddChild(string childName, string childText)
    {
        var e = new HTMLElement(childName, childText);
        root.Elements.Add(e);
    }

    override public string ToString() => root.ToString();

    public void Clear() => root = new HTMLElement { Name = _rootName };
}


/// <summary>
/// Represents an HTML fluent builder that creates a tree of HTML elements with a root name.
/// </summary>
public class HtmlFluentBuilder
{
    private readonly string _rootName;
    HTMLElement root = new HTMLElement();

    public HtmlFluentBuilder(string rootName)
    {
        _rootName = rootName ?? throw new ArgumentNullException(paramName: nameof(rootName));
        root.Name = rootName;
    }

    public HtmlFluentBuilder AddChild(string childName, string childText)
    {
        var e = new HTMLElement(childName, childText);
        root.Elements.Add(e);
        return this;
    }

    override public string ToString() => root.ToString();

    public void Clear() => root = new HTMLElement { Name = _rootName };
}
