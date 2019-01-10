using UnityEngine;
using UnityEngine.UI;

public class NoSpace : MonoBehaviour
{
    InputField inputField;

    private void Start()
    {
        inputField = GetComponent<InputField>();
    }

    public void EscapeSpace(string s)
    {
        inputField.text = s.Replace(" ", "");
    }

}
