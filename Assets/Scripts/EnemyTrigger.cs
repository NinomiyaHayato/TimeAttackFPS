using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] _enemy;@//Enemy‚ğ“ü‚ê‚é
    bool _enemyChange = true;@//animation‚Ì‰ñ”‚ğˆê‰ñ‚É§ŒÀ
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && _enemyChange == true)
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
        }
    }
}
