using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public ResourceSO resourceSo;
    private int _resourceAmount = 1;

    public int ResourceAmount
    {
        get => _resourceAmount;
        set => _resourceAmount = value;
    }
}
