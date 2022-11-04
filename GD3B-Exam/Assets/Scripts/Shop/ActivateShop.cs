using UnityEngine;

public class ActivateShop : MonoBehaviour
{
    [SerializeField] private ShopManager shopManagerUI;
    public void OpenShop()
    {
        shopManagerUI.OpenShop();
    }
}
