using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public float SpawnRatio = 1;

    private EnemySpawnControl[] _SpawnPoints;


    // Start is called before the first frame update
    void Start()
    {
        _SpawnPoints = FindObjectsOfType<EnemySpawnControl>();
        foreach(EnemySpawnControl esc in _SpawnPoints)
        {
            esc.Spawn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
