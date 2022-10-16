using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textUI;
    [SerializeField] private float popupLifetime = 1f;
    [SerializeField] private float distance = 0.5f;
    
    private Camera _playerCamera;
    private Vector3 _targetPos;
    private float _timer;

    private void Start()
    {
        _timer = 0f;
        transform.localScale = Vector3.zero;
        _playerCamera = FindObjectOfType<LineOfSight>().gameObject.GetComponent<Camera>();
        var popupPos = transform.position;
        transform.LookAt(2 * popupPos - _playerCamera.transform.position);
        _targetPos = transform.position - (_playerCamera.transform.forward * distance);
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        var fraction = popupLifetime / 2f;
        if (_timer > popupLifetime) Destroy(gameObject);
        else if (_timer > fraction) textUI.color = Color.Lerp(textUI.color, Color.clear, (_timer - fraction) / (popupLifetime - fraction));
        transform.localPosition = Vector3.Lerp(transform.position, _targetPos, Mathf.Sin(_timer / popupLifetime));
        transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, Mathf.Sin(_timer / popupLifetime));
    }

    public void SetDamageText(int damage)
    {
        textUI.text = damage.ToString();
    }
}
