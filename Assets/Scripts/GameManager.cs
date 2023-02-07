using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text _scoreText;�@//�|�����G�̐����J�E���g����text
    [SerializeField] Text _timeText;�@//�Q�[���X�^�[�g����̌o�ߎ��Ԃ�\��text
    [SerializeField] Text _allEnemyText;�@//�|���ׂ��G�̑S�Ă̐���\��text
    int _allEnemyCount = 0; //�|���ׂ��G�̐�
    int _killEnemyCount = 0;�@//�|�����G�̐�
    float _time;�@//�Q�[���X�^�[�g����̌o�ߎ���
    PlayerController _startBool;�@//�Q�[�����n�܂������ǂ�������
    [SerializeField] Image _backGround;�@//fade���邽�߂�image
    [SerializeField] GameObject _panel; //�Q�[���I�[�o�[���ɕ\������panel
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
