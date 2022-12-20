using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text _scoreText;
    [SerializeField] Text _timeText;
    [SerializeField] Text _allEnemyText;
    int _allEnemyCount = 0;
    int _killEnemyCount = 0;
    float _time;
    PlayerController _startBool;
    // Start is called before the first frame update
    void Start()
    {
        Begin();
        _startBool = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        _scoreText.text = _killEnemyCount.ToString("00");
        if(_startBool._gameStart == true)
        {
            _time += Time.deltaTime;
            _timeText.text = _time.ToString("00");
        }
    }
    public void EnemyCount(int enemycount)
    {
        _killEnemyCount += enemycount;
    }
    void Begin()
    {
        _allEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        _allEnemyText.text = _allEnemyCount.ToString("00");
    }
}
