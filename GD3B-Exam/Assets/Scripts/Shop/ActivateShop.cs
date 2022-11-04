using UnityEngine;

public class ActivateShop : MonoBehaviour
{
    [SerializeField] private GameObject shopCanvas;
    [SerializeField] private ShopManager shopManagerUI;
    public void OpenShop()
    {
        shopCanvas.SetActive(true);
        shopManagerUI.OpenShop();
    }

    public void CloseShop()
    {
        shopManagerUI.CloseShop();
        shopCanvas.SetActive(false);
    }
}
