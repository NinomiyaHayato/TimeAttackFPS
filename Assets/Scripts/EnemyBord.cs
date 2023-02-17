using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBord : MonoBehaviour
{
    [SerializeField] Vector3 _vecF = Vector3.forward; //���̐i�ޕ���
    Rigidbody _rb;
    [SerializeField] public float _movespeed;�@//���̑���
    [SerializeField] Vector3 _plusVector; //raycast�̍�������
    [SerializeField] float _macDistance;�@//ray��distance
    [SerializeField] Vector3 _vecB = Vector3.back;�@//���̐i�ޕ���
    [SerializeField] bool _turn = true;�@//���̔���
    public static bool _bordTrigger = false;�@//���̋N��
    AnimationGizmo _bordSpeedChange; //���̕ϐ�
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        //_rb.velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if(_bordTrigger == true)
        {
            Invoke("BordMove", 0.2f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && _bordTrigger == false)
        {
            _bordTrigger = true;
        }
    }
    void BordMove()
    {
        Ray _ray = new Ray(this.gameObject.transform.position + _plusVector, _vecF);
        Debug.DrawRay(this.gameObject.transform.position + _plusVector, _vecF, Color.red);
        RaycastHit _hit;
        if (Physics.Raycast(_ray, out _hit, _macDistance))
        {
            string name = _hit.collider.name;
            if (name == "TurnTrigger")
            {
                _turn = false;
            }
            else if(name == "TurnTrigger2")
            {
                _turn = true;
            }
        }
        if (_turn == true)
        {
            _rb.velocity = _vecF * _movespeed;
        }
        else
        {
            _rb.velocity = _vecB * _movespeed;
        }
    }
}
