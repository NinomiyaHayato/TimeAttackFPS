using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text _scoreText;　//倒した敵の数をカウントするtext
    [SerializeField] Text _timeText;　//ゲームスタートからの経過時間を表すtext
    [SerializeField] Text _allEnemyText;　//倒すべき敵の全ての数を表すtext
    int _allEnemyCount = 0; //倒すべき敵の数
    int _killEnemyCount = 0;　//倒した敵の数
    float _time;　//ゲームスタートからの経過時間
    PlayerController _startBool;　//ゲームが始まったかどうか判定
    [SerializeField] Image _backGround;　//fadeするためのimage
    [SerializeField] GameObject _panel; //ゲームオーバー時に表示するpanel
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
        if(_killEnemyCount == _allEnemyCount && _startBool._gameStart == true)
        {
            _startBool._gameStart = false;
            SceneMoveFade("Result");
        }
    }
    public void EnemyCount(int enemycount)
    {
        _killEnemyCount += enemycount;
    }
    public void GameOver()
    {
        _panel.SetActive(true);
        _startBool._gameStart = false;
        Destroy(GameObject.Find("Player"));
        if(Cursor.visible == false)
        {
            Cursor.visible = true;
        }
    }
    void Begin()
    {
        _allEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        _allEnemyText.text = _allEnemyCount.ToString("00");
        if(EnemyBord._bordTrigger != false)
        {
            EnemyBord._bordTrigger = false;
        }
        Cursor.visible = false;
    }
    public void SceneMoveFade(string scenename)
    {
        this._backGround.DOFade(2f, 2f).SetDelay(1.5f).OnComplete(() => SceneManager.LoadScene(scenename));
    }
    public void SceneMove(string name)
    {
        SceneManager.LoadScene(name);
    }
}
