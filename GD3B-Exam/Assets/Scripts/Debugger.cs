using UnityEngine;

public class Debugger : MonoBehaviour
{
    [SerializeField] private ShopManager shopManager;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            shopManager.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            shopManager.NavigateDown();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            shopManager.NavigateUp();
        }
    }
}
