using UnityEngine;

[CreateAssetMenu(fileName = "AttributeData", menuName = "Scriptable Objects/AttributeData")]
public class AttributeData : ScriptableObject
{
    [Header("General Infor")]
    [TextArea, SerializeField] string Description;
    public Sprite Thumbnail;

}
