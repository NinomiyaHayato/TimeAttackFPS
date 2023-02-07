using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationGizmo : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float _gizmosize = 0.3f;　//gizmoのサイズ
    [SerializeField] Color _gizmocolor = Color.yellow;　//gizmoのカラー
    Animator _anim;
    private void Start()
    {
        _anim = GetComponent<Animator>();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmocolor;
        Gizmos.DrawWireSphere(this.transform.position, _gizmosize);
    }
    public  void AnimationTrigger(bool judgement)
    {
        if(judgement == true)
        {
            _anim.SetTrigger("Up");
        }
        else if(judgement == false)
        {
            _anim.SetTrigger("Down");
            GameObject _tmp = this.transform.parent.gameObject;
            if (_tmp != null)
            {
                EnemyBord speed = gameObject.GetComponentInParent<EnemyBord>();
                speed._movespeed = 0;
            }
        }
    }
}
