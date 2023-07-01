using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _speed;
    Rigidbody _enemyRb;
    GameObject _player;

    void Start()
    {
        _enemyRb = GetComponent<Rigidbody>();
        _player = GameObject.Find("Player");
        StartCoroutine(DestroyMyself());
    }

    void Update()
    {
        //enemyからplayerの向きになっている
        Vector3 lockDirection = (_player.transform.position - transform.position);
        _enemyRb.AddForce(lockDirection * _speed);
    }

    IEnumerator DestroyMyself()
    {
        while (true)
        {
            yield return new WaitForSeconds(1); //1秒待つ
            Debug.Log("Test");
            //y座標が規定値より落ちてたら、自分を消す
            if (transform.position.y < -10)
            {
                Destroy(gameObject);
            }
        }
        
    }
}
