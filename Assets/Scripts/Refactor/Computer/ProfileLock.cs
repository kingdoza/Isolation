using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileLock : MonoBehaviour
{
    [SerializeField] private TMP_InputField pinInputField;
    [SerializeField] private string pin;
    [SerializeField] private GameObject wrongPinPhrase;
    [SerializeField] private GameObject mainCanvas;



    private void Awake()
    {
        mainCanvas.SetActive(false);
        wrongPinPhrase.SetActive(false);
    }



    private void OnEnable()
    {
        wrongPinPhrase.SetActive(false);
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CheckPin();
        }

    }



    public void CheckPin()
    {
        Debug.Log(pinInputField.text);
        Debug.Log(pin);
        if (!pinInputField.text.Equals(pin))
        {
            pinInputField.text = "";
            wrongPinPhrase.SetActive(true);
            return;
        }
        mainCanvas.SetActive(true);
        gameObject.SetActive(false);
    }
}
