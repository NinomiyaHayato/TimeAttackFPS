using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotController : MonoBehaviour
{
    [SerializeField] Image _crosshair;�@//�Ə�
    [SerializeField] float _range = 10;�@//�����̒���
    [SerializeField] Transform _shotPosition;�@//�ǂ�����ray���΂���
    [SerializeField] LayerMask _layerMask;�@
    [SerializeField] Color _defaultColor = Color.green; //ray�ɉ��������Ă��Ȃ��Ƃ��̏Ə��̐F
    [SerializeField] Color _hitColor = Color.red;�@//ray�ɓ����Ă���Ƃ�
    [SerializeField] LineRenderer _line;
    GameManager _enemyCount;�@//�|�����G�̃J�E���g
    //AnimationGizmo _animtrigger;
    // Start is called before the first frame update
    void Start()
    {
        _enemyCount = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray _ray = Camera.main.ScreenPointToRay(_crosshair.rectTransform.position);
        RaycastHit _hit = default;
        Vector3 _hitPosition = _shotPosition.transform.position + _shotPosition.forward * _range;
        Collider _hitCollider = default;
        if(Physics.Raycast(_ray,out _hit,_range,_layerMask))
        {
            _hitPosition = _hit.point;
            _hitCollider = _hit.collider;
            _crosshair.color = _hitColor;
        }
        else
        {
            _crosshair.color = _defaultColor;
        }
        if(Input.GetButtonDown("Fire1"))
        {
            ShotLaser(_hitPosition);
            if(_hitCollider)
            {
                AnimationGizmo _animtrigger = _hitCollider.GetComponentInParent<AnimationGizmo>();
                _animtrigger.AnimationTrigger(false);
                if(_hitCollider.gameObject.tag == "Enemy")
                {
                    _enemyCount.EnemyCount(1);
                }
                else if(_hitCollider.gameObject.tag == "NotEnemy")
                {
                    _enemyCount.GameOver();
                }
            }
        }
        void ShotLaser(Vector3 destination)
        {
            Vector3[] shotLaserPositions = { _shotPosition.position, destination };
            _line.positionCount = shotLaserPositions.Length;
            _line.SetPositions(shotLaserPositions);
        }
    }
}
