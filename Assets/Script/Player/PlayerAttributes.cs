
[System.Serializable]
public class PlayerAttributes
{
    public AttributeData attributeData;
    public int amount;

    public PlayerAttributes(AttributeData attributeData, int amount)
    {
        this.attributeData = attributeData;
        this.amount = amount;
    }
}
