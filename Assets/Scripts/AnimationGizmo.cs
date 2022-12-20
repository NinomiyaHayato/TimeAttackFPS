using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationGizmo : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float _gizmosize = 0.3f;
    [SerializeField] Color _gizmocolor = Color.yellow;
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
        }
    }
}
