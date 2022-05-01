public class SpreadFieldAttribute : System.Attribute
{
    private string _rawName;
    public string RawName => _rawName;

    public SpreadFieldAttribute(string rawName)
    {
        _rawName = rawName;
    }
}
