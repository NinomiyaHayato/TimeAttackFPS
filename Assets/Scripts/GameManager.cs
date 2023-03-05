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
    float _time = 0.00f;　//ゲームスタートからの経過時間
    PlayerController _startBool;　//ゲームが始まったかどうか判定
    [SerializeField] Image _backGround;　//fadeするためのimage
    [SerializeField] GameObject _panel; //ゲームオーバー時に表示するpanel
    AudioSource _audio;
    [SerializeField] AudioClip[] _audioClip;
    public static GameManager _instance;
    public static float _nowTime = 0;// ゲームクリア時のtime
    // Start is called before the first frame update
    private void Awake()
    {
        CheckInstance();
    }
    void Start()
    {
        Begin();
        _startBool = GameObject.Find("Player").GetComponent<PlayerController>();
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _scoreText.text = _killEnemyCount.ToString("00");
        if(_startBool._gameStart)
        {
            _time += Time.deltaTime;
            _timeText.text = $"Time : {_time.ToString("00.00")}s" ;
        }
        if(_killEnemyCount == _allEnemyCount && _startBool._gameStart)
        {
            _startBool._gameStart = false;
            _nowTime = _time;
            SceneMoveFade("Result");
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneMoveFade("GameScene");
            Music(1);
        }
    }
    public void EnemyCount(int enemycount)//敵を倒した数をカウントする
    {
        _killEnemyCount += enemycount;
    }
    public void GameOver() //ゲームオーバーの処理
    {
        _panel.SetActive(true);
        _startBool._gameStart = false;
        Destroy(GameObject.Find("Player"));
        Music(0);
        if(Cursor.visible == false)
        {
            Cursor.visible = true;
        }
    }
    void Begin()//シーンが読み込まれて最初にやりたい処理
    {
        _allEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length / 2;
        _allEnemyText.text = _allEnemyCount.ToString("00");
        if(EnemyBord._bordTrigger != false)
        {
            EnemyBord._bordTrigger = false;
        }
        Cursor.visible = false;
    }
    public void SceneMoveFade(string scenename)//シーンの読み込み
    {
        this._backGround.DOFade(2f, 2f).SetDelay(0.7f).OnComplete(() => SceneManager.LoadScene(scenename));
    }
    void CheckInstance() //同じオブジェクトがあったら破棄（nullチェック
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void Music(int num)　//音を鳴らす
    {
        _audio.PlayOneShot(_audioClip[num],10f);
    }
}
