public class SpreadSheetAttribute : System.Attribute
{
    private string _spreadName;
    public string SpreadName => _spreadName;
    public SpreadSheetAttribute(string spreadName)
    {
        _spreadName = spreadName;
    }
}
