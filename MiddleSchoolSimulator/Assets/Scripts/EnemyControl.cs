﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControl : MonoBehaviour {

    public GameObject DickPoint;    // spawn point
    public GameObject DickPrefab;
    public GameObject Dick; // dick which is created

    private GameControl _GameControl;
    private Animator _Pencil;
    private Vector3 _Destination;
    private Vector3 _StartPosition;

    private bool _isVisible;
    private bool _isDrawing;
    private bool _wasHit;
    private bool _startedPenis = false; // so it instantiates only one
    private bool _isWithdrawing;
    private int _loadType;  // 0-54 small dick, 55-89 - big dick, 90-100 - ad
    private float _enemySpeed = 8f;  // speed of hand moving towards the spawn


    // Start is called before the first frame update
    void Start()
    {
        _enemySpeed = FindObjectOfType<GameControl>().enemySpeed;
        _StartPosition = transform.position;
        _GameControl = FindObjectOfType<GameControl>();
        _Destination = new Vector3(DickPoint.transform.position.x, DickPoint.transform.position.y, 0);
        _Pencil = transform.GetComponentInChildren<Animator>();
        _loadType = Random.Range(0, 100);
        print(_loadType);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isWithdrawing)
        {
            Withdraw();
        }
        else
        {
            MoveTowardsPoint();
        }
        CheckIfInsideMap();
    }

    void CheckIfInsideMap()
    {
        if (transform.position.x < 0 || transform.position.x > Screen.width)
        {
            _isVisible = false;
            if (_wasHit || _isWithdrawing)
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
            //if (transform.position != _Destination)
            if (Vector3.Distance(transform.position, _Destination) >= 2)    // in case the hand is stuck
            {
                _isDrawing = false;
                transform.position = Vector3.MoveTowards(transform.position, _Destination, _enemySpeed * Time.deltaTime);
            }
            else
            {
                _isDrawing = true;
                if (!_startedPenis)
                {
                    if (_loadType < 55)
                    {
                        CreatePenis();
                        Dick.GetComponent<DickControl>().SetMaxScale(0.6f);
                    }
                    else if (_loadType < 90)
                    {
                        CreatePenis();
                        Dick.GetComponent<DickControl>().SetMaxScale(1);
                    }
                    else
                    {
                        print("I have ad");
                        _isWithdrawing = true;
                    }
                    _startedPenis = true;
                }
            }
            DrawDick();
        }
    }

    public void Withdraw()
    {
        transform.position = Vector3.MoveTowards(transform.position, _StartPosition, 5 * _enemySpeed * Time.deltaTime);
        _Pencil.Play("Idle");
    }

    public void IsHit()
    {
        if (!_isWithdrawing)
        {
            if(_GameControl.penPower == 100)
            {
                //change sprite
                _GameControl.FullyDischargePen();
                _isWithdrawing = true;
                Withdraw();
            }
            else
            {
                _GameControl.ChargePen();
                transform.position = Vector3.MoveTowards(transform.position, _StartPosition, _GameControl.hitPower);
            }
            _wasHit = true;
            _isDrawing = false;
        }
    }

    void DrawDick()
    {
        if (_isDrawing)
        {
            if (Dick == null)
            {
                _isWithdrawing = true;
                Withdraw();
            }
            else
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
        Dick.GetComponent<DickControl>().hasStarted = true;
    }

}
