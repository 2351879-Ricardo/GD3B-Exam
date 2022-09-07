using UnityEngine;

[CreateAssetMenu(menuName = "SO's/Resources", fileName = "NewResourceSO")]
public class ResourceSO : ScriptableObject
{
    [SerializeField] private string itemName;
    [TextArea(5, 15)] [SerializeField] private string itemDescription;
    [SerializeField] private Sprite itemSprite;
    
    public string ItemName => itemName;
    public string ItemDescription => itemDescription;
    public Sprite ItemSprite => itemSprite;
}
