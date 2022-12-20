using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] float _jumpPower;
    Rigidbody _rb;
    bool _canjump = true;
    [SerializeField] Transform _rightTtarget;
    [SerializeField] Transform _leftTarget;
    [SerializeField, Range(0f, 1f)] float _rightHandPosition;
    [SerializeField, Range(0f, 1f)] float _rightHandRotation;
    [SerializeField, Range(0f, 1f)] float _leftHandPosition;
    [SerializeField, Range(0f, 1f)] float _leftHandRotation;
    Animator _anim;
    public bool _gameStart = false;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float y = _rb.velocity.y;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            y = _jumpPower;
            _rb.velocity = Vector3.up * _jumpPower;
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
    private void OnAnimatorIK(int layerIndex)
    {
        _anim.SetIKPositionWeight(AvatarIKGoal.RightHand, _rightHandPosition);
        _anim.SetIKPositionWeight(AvatarIKGoal.RightHand, _rightHandRotation);
        _anim.SetIKPosition(AvatarIKGoal.RightHand, _rightTtarget.position);

        _anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, _leftHandPosition);
        _anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, _leftHandRotation);
        _anim.SetIKPosition(AvatarIKGoal.LeftHand, _leftTarget.position);
    }
}
