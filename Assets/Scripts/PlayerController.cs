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
    [SerializeField] GameObject _arms;
    Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float y = _rb.velocity.y;
        if(_canjump)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                y = _jumpPower;
                _rb.velocity = Vector3.up * _jumpPower;
            }
        }
        if(Input.GetButtonDown("Fire1")) //射撃のanimation
        {
            _anim.SetTrigger("Shot");
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
        _canjump = true;
        if(other.gameObject.tag == "GameStart")
        {
            _gameStart = true;
            Debug.Log("Trueになりました");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _canjump = false;
    }
}
