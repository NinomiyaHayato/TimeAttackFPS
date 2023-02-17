using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAnimation : MonoBehaviour
{
    Animator _anim;
    [SerializeField] bool _action = true;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(_action == true && other.gameObject.tag == "Player")
        {

        }
        else if(_action == false && other.gameObject.tag == "Player")
        {
            _anim.SetTrigger("DoorLeft");
            _anim.SetTrigger("DoorRight");
        }
    }
}
