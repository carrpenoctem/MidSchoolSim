using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    [Header("Enemy")]
    public float spawnRatio = 1;
    public float enemySpeed = 10;
    public float penisDrawSpeed = 0.0000001f;
    private EnemySpawnControl[] _SpawnPoints;

    [Header("Player")]
    public float penisEreaseSpeed = 1f;
    public float hitPower = 55;
    public float writingSpeed = 0.1f;
    public int livesLeft = 3;

    [Header("Pen Power")]
    public RectTransform PenImage;
    private Vector2 _PenImageStartWidth;
    private Animator _PenAnimator;
    public float penPower = 0;
    public float penDischargeRatio = 8f;
    private float _penDischargerTimer = 0;

    [Header("Game Settings")]
    public RectTransform ProgressBar;
    public GameObject[] Apples;
    public float time = 0;
    public int _hpCount = 3;


    // Start is called before the first frame update
    void Start()
    {
        _PenAnimator = PenImage.transform.parent.GetComponent<Animator>();
        _PenImageStartWidth = PenImage.sizeDelta;
        _SpawnPoints = FindObjectsOfType<EnemySpawnControl>();
        foreach(EnemySpawnControl esc in _SpawnPoints)
        {
            esc.Spawn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        time = time + Time.deltaTime;
        PenImageControl();
        DischargePen();
    }

    public void ChargePen()
    {
        penPower = penPower + 30;
        _PenAnimator.StopPlayback();
        _PenAnimator.Play("Game_PencilPowerUp");
        if(penPower>= 100)
        {
            _PenAnimator.Play("Game_PencilFullPower");
            penPower = 100;
            _penDischargerTimer = 4;
        }
    }

    public void DischargePen()
    {
        if(penPower < 100 || _penDischargerTimer <=0)
        {
            penPower = penPower - penDischargeRatio * Time.deltaTime;
            if(penPower <= 0)
            {
                penPower = 0;
            }
        }
        else
        {
            _penDischargerTimer = _penDischargerTimer - Time.deltaTime;
        }
        
    }

    public void FullyDischargePen()
    {
        _PenAnimator.StopPlayback();
        penPower = 0;
    }

    public void PenImageControl()
    {
        Vector2 _PenImageNewSize = new Vector2(penPower * 237.19f / 100, _PenImageStartWidth.y);
        PenImage.sizeDelta = _PenImageNewSize;
    }

    public void LowerHP()
    {
        Apples[_hpCount-1].GetComponent<Animator>().Play("Game_AppleScale");
        _hpCount = _hpCount - 1;
        if(_hpCount == 0)
        {
            //defeat
        }
    }

    public void AddHP()
    {
        if(_hpCount < 3)
        {
            Apples[_hpCount].GetComponent<Animator>().Play("Game_AppleScaleUp");
            _hpCount = _hpCount + 1;
        }
    }
}
