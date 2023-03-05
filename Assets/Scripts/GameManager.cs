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
    float _time = 0.00f;�@//�Q�[���X�^�[�g����̌o�ߎ���
    PlayerController _startBool;�@//�Q�[�����n�܂������ǂ�������
    [SerializeField] Image _backGround;�@//fade���邽�߂�image
    [SerializeField] GameObject _panel; //�Q�[���I�[�o�[���ɕ\������panel
    AudioSource _audio;
    [SerializeField] AudioClip[] _audioClip;
    public static GameManager _instance;
    public static float _nowTime = 0;// �Q�[���N���A����time
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
    public void EnemyCount(int enemycount)//�G��|���������J�E���g����
    {
        _killEnemyCount += enemycount;
    }
    public void GameOver() //�Q�[���I�[�o�[�̏���
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
    void Begin()//�V�[�����ǂݍ��܂�čŏ��ɂ�肽������
    {
        _allEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length / 2;
        _allEnemyText.text = _allEnemyCount.ToString("00");
        if(EnemyBord._bordTrigger != false)
        {
            EnemyBord._bordTrigger = false;
        }
        Cursor.visible = false;
    }
    public void SceneMoveFade(string scenename)//�V�[���̓ǂݍ���
    {
        this._backGround.DOFade(2f, 2f).SetDelay(0.7f).OnComplete(() => SceneManager.LoadScene(scenename));
    }
    void CheckInstance() //�����I�u�W�F�N�g����������j���inull�`�F�b�N
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
    public void Music(int num)�@//����炷
    {
        _audio.PlayOneShot(_audioClip[num],10f);
    }
}
