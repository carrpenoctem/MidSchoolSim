using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControl : MonoBehaviour {

    public GameObject DickPoint;    // spawn point
    public GameObject DickPrefab;
    public GameObject Dick; // dick which is created

    public float pencilSpeed = 8f;  // speed of hand moving towards the spawn

    private GameControl _GameControl;
    private Animator _Pencil;
    private Vector3 _Destination;

    private bool _isVisible;
    private bool _isDrawing;
    private bool _wasHit;
    private bool _startedPenis = false; // so it instantiates only one
    private bool _isWithdrawing;
    private int _loadType;  // 0+ - small dick, 4+ - big dick, 6 - ad

    // Start is called before the first frame update
    void Start()
    {
        _GameControl = FindObjectOfType<GameControl>();
        _Destination = new Vector3(DickPoint.transform.position.x, DickPoint.transform.position.y, 0);
        _loadType = Random.Range(0, 2);
        _Pencil = transform.GetComponentInChildren<Animator>();
        _loadType = Random.Range(0, 6);
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsPoint();
        CheckIfInsideMap();
    }

    void CheckIfInsideMap()
    {
        if (transform.position.x < 0 || transform.position.x > Screen.width)
        {
            _isVisible = false;
            if (_wasHit||_isWithdrawing)
            {
                Destroy(this.gameObject);
            }
        }
        else if (transform.position.y < 0 || transform.position.y > Screen.height)
        {
            _isVisible = false;
            if (_wasHit || _isWithdrawing)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            _isVisible = true;
        }
    }

    void MoveTowardsPoint()
    {
        if (!_isWithdrawing)
        {
            if (transform.position != _Destination)
            {
                _isDrawing = false;
                transform.position = Vector3.MoveTowards(transform.position, _Destination, pencilSpeed * Time.deltaTime);
                Vector2 rotation = new Vector2(_Destination.x , _Destination.y );
                transform.right = rotation;
            }
            else
            {
                _isDrawing = true;
                if (!_startedPenis)
                {
                    if (_loadType < 4)
                    {
                        CreatePenis();
                        Dick.GetComponent<DickControl>().SetMaxScale(0.6f);
                    }
                    else if (_loadType < 6)
                    {
                        CreatePenis();
                        Dick.GetComponent<DickControl>().SetMaxScale(1);
                    }
                    else
                    {
                        _isWithdrawing = true;
                    }
                    _startedPenis = true;
                }
            }
            DrawDick();
        }
        else
        {
            Withdraw();
        }
    }

    public void Withdraw()
    {
        transform.position = Vector3.MoveTowards(transform.position, -_Destination, 2 * pencilSpeed * Time.deltaTime);
        _Pencil.Play("Idle");
    }

    public void GoBack()
    {
       // transform.position = Vector3.MoveTowards(transform.position, -transform.right, _GameControl.attackPower);
        transform.position = Vector3.MoveTowards(transform.position, -transform.right, 55);
        _wasHit = true;
        _isDrawing = false;
    }

    void DrawDick()
    {
        if (_isDrawing)
        {
            float _dickCurrentScale = Dick.GetComponent<RectTransform>().localScale.x;
            float _dickMaxScale = Dick.GetComponent<DickControl>().GetMaxScale();

            if (_dickCurrentScale < _dickMaxScale)
            {
                Dick.GetComponent<DickControl>().DrawPenis();
                _Pencil.Play("PencilDrawing");
            }
            else
            {
                _isWithdrawing = true;
            }
        }
        else
        {
            _Pencil.Play("Idle");
        }
    }

    void CreatePenis()
    {
        Dick = Instantiate(DickPrefab);
        Dick.transform.SetParent(DickPoint.transform, false);
    }

}
