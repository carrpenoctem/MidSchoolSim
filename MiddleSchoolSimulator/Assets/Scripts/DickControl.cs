using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DickControl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Sprite[] DickSprites;

    private Button _PenisButton;
    private Image _ImageComponent;
    private RectTransform _RectTransform;
    private Text _Procentage;
    private GameControl _GameControl;

    public bool isShowingUp = false;
    public bool isEreased = false;
    public bool hasStarted = false;

    private float _maxScale;
    private float _localScaleX;
    private float _localScaleY;
    private int _procentDone = 0;

    // Start is called before the first frame update
    void Start()
    {
        _GameControl = FindObjectOfType<GameControl>();
        _PenisButton = gameObject.GetComponent<Button>();
        _Procentage = gameObject.GetComponentInChildren<Text>();
        _ImageComponent = gameObject.GetComponentInChildren<Image>();
        _ImageComponent.sprite = DickSprites[Random.Range(0,DickSprites.Length)];
        _RectTransform = gameObject.GetComponent<RectTransform>();
        _RectTransform.localScale = new Vector3(0, 0, 0);
        _localScaleX = _RectTransform.localScale.x;
        _localScaleY = _RectTransform.localScale.y;
    }

    private void Update()
    {
        if (isEreased)
        {
            EreasePenis();
        }
    }

    public void SetMaxScale(float maxScale)
    {
        this._maxScale = maxScale;
    }
    public float GetMaxScale()
    {
        return _maxScale;
    }

    public void DrawPenis()
    {
        if (_RectTransform != null)
        {
            _localScaleY = _localScaleY + _GameControl.penisDrawSpeed * Time.deltaTime;
            _localScaleX = _localScaleX + _GameControl.penisDrawSpeed * Time.deltaTime;
            _RectTransform.localScale = new Vector3(_localScaleX, _localScaleY, 1.0f);
            UpdateProcentage();
        }
    }

    public void EreasePenis()
    {
        if(isShowingUp == false)
        {
            isEreased = true;
            _localScaleY = _localScaleY - _GameControl.penisEreaseSpeed * Time.deltaTime;
            _localScaleX = _localScaleX - _GameControl.penisEreaseSpeed * Time.deltaTime;
            _RectTransform.localScale = new Vector3(_localScaleX, _localScaleY, 1.0f);
            UpdateProcentage();
        }
    }

    public void UpdateProcentage()
    {
        float _procentDoneFloat = (100 * _localScaleX) / _maxScale;
        _procentDone = (int)_procentDoneFloat;
        if(_procentDoneFloat > 100)
        {
            _Procentage.text = "100%";
        }
        else if(_procentDoneFloat <= 0 && hasStarted)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _Procentage.text = _procentDone + "%";
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isEreased = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isEreased = false;
    }
}
