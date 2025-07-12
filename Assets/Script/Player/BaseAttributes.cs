
[System.Serializable]
public class BaseAttributes
{
    public AttributeData attributeData;
    public int amount;

    public BaseAttributes(AttributeData attributeData, int amount)
    {
        this.attributeData = attributeData;
        this.amount = amount;
    }
}
