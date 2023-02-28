using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAnimation : MonoBehaviour
{
    Animator _anim;
    [SerializeField] bool _action = true;
    AudioSource _audio;
    [SerializeField] AudioClip _audioClip;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(_action == true && other.gameObject.tag == "Player")
        {
            _anim.SetTrigger("DoorLeft");
            _anim.SetTrigger("DoorRight");
            _action = false;
            _audio.PlayOneShot(_audioClip);
        }
    }
}
