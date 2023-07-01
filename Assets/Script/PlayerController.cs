using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody playerRb;
    [SerializeField] float speed;
    GameObject focalPoint;
    [SerializeField] bool hasPowerup;
    [SerializeField] GameObject powerupIndicator;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        //パワーアップの印のやつは、プレイヤーと同じ座標に変更
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            //アイテムの効果が切れるコルーチン発動
            //コルーチンを呼び出すときは StartCoroutine を書いて、
            //（）の中にコルーチンの関数を入れる。
            StartCoroutine(PowerupCountdownRoutine());
            //さっきまでは非表示だった印が
            //非表示状態になる（パワーアップしてる感じ）
            powerupIndicator.gameObject.SetActive(true);
        }
    }

    float powerupStrength = 15.0f;
    private void OnCollisionEnter(Collision collision)
    {
        //ぶつかった相手がEnemyタグを持ってる、かつ、
        //(自分が)パワーアップ状態なら
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRb = collision.gameObject.
                GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = 
                collision.gameObject.transform.position - 
                transform.position;
                enemyRb.AddForce(awayFromPlayer
                    * powerupStrength, ForceMode.Impulse);
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        //yield returnは絶対にひとつは必要だが、
        //2個以上書ける。
        yield return new WaitForSeconds(2); //7秒待つ
        hasPowerup = false;
    }
}
