using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public GameObject DickPoint;

    public float pencilSpeed = 8f;

    private GameObject _Pencil;
    private Vector3 _Destination;

    private bool _isVisible;
    private int _loadType;  // 0 - small dick, 1 - big dick, 2 - advertisement


    // Start is called before the first frame update
    void Start()
    {
        _Destination = new Vector3(DickPoint.transform.position.x, DickPoint.transform.position.y, 0);
        _loadType = Random.Range(0, 2);
        _Pencil = this.gameObject;
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
        }
        else if(transform.position.y < 0 || transform.position.y > Screen.height)
        {
            _isVisible = false;
        }
        else
        {
            _isVisible = true;
        }
    }

    void MoveTowardsPoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, _Destination, pencilSpeed * Time.deltaTime);
    }

}
