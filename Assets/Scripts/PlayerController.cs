using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _moveSpeed; //playerの動く速さ
    [SerializeField] float _jumpPower;　//playerのジャンプ力
    Rigidbody _rb;
    [SerializeField]bool _canjump = true;　//playerの設置判定
    public bool _gameStart = false;
    [SerializeField] GameObject _arms;　//腕のGameobject
    Animator _anim;
    ParticleSystem _pars;// 射撃のパーティクル
    int count = 0;
    float _time; //ジャンプ後の計測
    [SerializeField] float _rimitTime;//次のジャンプまでのrimit
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponentInChildren<Animator>();
        _pars = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        float y = _rb.velocity.y;
        if(count == 0) //ジャンプ
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                y = _jumpPower;
                _rb.velocity = Vector3.up * _jumpPower;
                count++;
            }
        }
        if(count != 0) //ジャンプ回数の制限
        {
            _time += Time.deltaTime;
            if(_time > _rimitTime)
            {
                count = 0;
                _time = 0;
            }
        }
        if(Input.GetButtonDown("Fire1")) //射撃のanimation
        {
            _anim.SetTrigger("Shot");
            _pars.Play();
        }
    }
    private void FixedUpdate()
    {
        float _h = Input.GetAxisRaw("Horizontal");
        float _v = Input.GetAxisRaw("Vertical");
        Vector3 dir = Vector3.forward * _v + Vector3.right * _h;
        // カメラのローカル座標系を基準に dir を変換する
        dir = Camera.main.transform.TransformDirection(dir);
        // カメラは斜め下に向いているので、Y 軸の値を 0 にして「XZ 平面上のベクトル」にする
        dir.y = 0;
        // 移動の入力がない時は回転させない。入力がある時はその方向にキャラクターを向ける。
        if (dir != Vector3.zero) this.transform.forward = dir;
        _rb.velocity = dir.normalized * _moveSpeed + _rb.velocity.y * Vector3.up;
        this.transform.forward = Camera.main.transform.forward;
        _arms.transform.forward = Camera.main.transform.forward;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "GameStart")
        {
            _gameStart = true;
            Debug.Log("Trueになりました");
        }
    }
}
