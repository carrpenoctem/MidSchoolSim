using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DickControl : MonoBehaviour
{
    public Sprite[] DickSprites;

    private Image _ImageComponent;
    private RectTransform _RectTransform;

    public bool isShowingUp = false;
    public bool isEreased = false;
    public float showingUpSpeed = 0.0000001f;

    private float _maxScale;
    private float _localScaleX;
    private float _localScaleY;

    // Start is called before the first frame update
    void Start()
    {
        _ImageComponent = gameObject.GetComponentInChildren<Image>();
        _ImageComponent.sprite = DickSprites[Random.Range(0,DickSprites.Length)];
        _RectTransform = gameObject.GetComponent<RectTransform>();
        _RectTransform.localScale = new Vector3(0, 0, 0);
        _localScaleX = _RectTransform.localScale.x;
        _localScaleY = _RectTransform.localScale.y;
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
            _localScaleY = _localScaleY + showingUpSpeed * Time.deltaTime;
            _localScaleX = _localScaleX + showingUpSpeed * Time.deltaTime;
            _RectTransform.localScale = new Vector3(_localScaleX, _localScaleY, 1.0f);
        }
    }

}
