using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PagesControl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public string textToWrite;

    private Text _Text;
    private GameControl _GameControl;

    private string _currentText = "";
    private bool _isWritten;
    private float _writingSpeed;
    private int _currentTextChar = 0;

    // Start is called before the first frame update
    void Start()
    {
        _GameControl = FindObjectOfType<GameControl>();
        _writingSpeed = _GameControl.writingSpeed;
        _Text = gameObject.GetComponentInChildren<Text>();
        _Text.text = _currentText;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isWritten)
        {
            StartCoroutine(WriteText());
        }
        if (_currentText == textToWrite)
        {
            print("Victory");
        }
    }

    IEnumerator WriteText()
    {
        _currentText = textToWrite.Substring(0, _currentTextChar);
        _Text.text = _currentText;
        if (_currentTextChar <= textToWrite.Length)
        {
            _currentTextChar++;
        }
        yield return new WaitForSeconds(_writingSpeed);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isWritten = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isWritten = false;
    }
}