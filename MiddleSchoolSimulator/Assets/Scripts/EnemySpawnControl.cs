using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnControl : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public GameObject[] Destinations;

    private GameControl _GameControl;
    private GameObject _SelectedDestination;

    void Start()
    {
        _GameControl = FindObjectOfType<GameControl>();
    }

    public void Spawn()
    {
        PickDestination();
        GameObject enemy = Instantiate(EnemyPrefab);
        enemy.transform.SetParent(gameObject.transform, false);
        enemy.GetComponent<EnemyControl>().DickPoint = _SelectedDestination;
    }

    private void PickDestination()
    {
        _SelectedDestination = Destinations[Random.Range(0, Destinations.Length)];
    }
}
