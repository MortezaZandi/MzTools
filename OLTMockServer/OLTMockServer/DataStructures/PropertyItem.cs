namespace OLTMockServer.UI
{
    public class PropertyItem
    {
        public PropertyItem(string propertyName, string propertyType)
        {
            PropertyName = propertyName;
            PropertyType = propertyType;
        }

        public string PropertyName { get; set; }
        public string PropertyType { get; set; }
    }
}
