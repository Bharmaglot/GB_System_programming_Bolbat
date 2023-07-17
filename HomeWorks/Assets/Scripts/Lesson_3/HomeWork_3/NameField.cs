using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameField : MonoBehaviour
{
    [SerializeField] internal TextMeshProUGUI TextName;
    [SerializeField] private TMP_InputField _inputNameField;
    [SerializeField] private Button _buttonUpdateClientName;

    private void Start()
    {
        _buttonUpdateClientName.onClick.AddListener(() => ChangeName());
    }

    private void ChangeName()
    {
        TextName.text = _inputNameField.text;
    }
}
