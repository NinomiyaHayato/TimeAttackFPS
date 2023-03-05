using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] _enemy;�@//Enemy������
    bool _enemyChange = true;�@//animation�̉񐔂����ɐ���
    [SerializeField] bool _parentBool = false; //�����G�Ȃ̂��ǂ�������
    [SerializeField] GameObject[] _movebord; //�����������邽�߂̔z��
    AudioSource _audio;
    [SerializeField] AudioClip _audioClip;
    // Start is called before the first frame update
    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && _enemyChange)�@//�G�̈ʒu�������_���ɓ���ւ���
        {
            for(int i = 0; i < _enemy.Length; i++)
            {
                Vector3 tmp = _enemy[i].transform.position;
                int random = Random.Range(0, _enemy.Length);
                _enemy[i].transform.position = _enemy[random].transform.position;
                _enemy[random].transform.position = tmp;
            }
            for(int i = 0; i < _enemy.Length; i++)
            {
                AnimationGizmo _enemytrigger = _enemy[i].GetComponent<AnimationGizmo>();
                _enemytrigger.AnimationTrigger(true);
                if(_enemy.Length - 1 == i)
                {
                    _enemyChange = false;
                }
            }
            if(_parentBool)�@//����ւ����ԋ߂��̓������̎q�I�u�W�F�N�g�ɂȂ�
            {
                for(int i = 0; i < _enemy.Length; i++)
                {
                    float _max = float.MaxValue;
                    Transform tmp = default;
                    for (int j = 0; j < _movebord.Length; j++)
                    {
                        float distance = Vector3.Distance(_enemy[i].transform.position, _movebord[j].transform.position);
                        if(distance < _max)
                        {
                            _max = distance;
                           tmp = _movebord[j].transform;
                        }
                        if (j == _movebord.Length - 1)
                        {
                            _enemy[i].transform.SetParent(tmp);
                        }
                    }
                }
            }
            _audio.PlayOneShot(_audioClip,10f);
        }
    }
}
