using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    float spawnRange = 9;

    void Start()
    {
        //初めて見た人が最初に敵を作ることを
        //わかってもらいやすい
        SpawnEnemyWave(waveNumber);//デフォ1体生成
        StartCoroutine(RespawnEnemiesCoroutine());
    }

    Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }

    //引数にint~~って書くことで、
    //呼び出すときにintを指定する必要がある
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        //i=0, 1, 2で実行が終了
        //i=3はi<3を満たさないので、できない
        for (int i = 0; i < enemiesToSpawn; ++i)
        {
            Instantiate(enemyPrefab,
                    GenerateSpawnPosition(),
                    enemyPrefab.transform.rotation);
            //Instantiateの第二引数（ひきすう）もrandomPos
            //にちゃんと変更する
        }
    }

    int enemyCount;//エネミー数
    int waveNumber = 1;//ウェーブ
    IEnumerator RespawnEnemiesCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(1); //1秒待つ
            //エネミーの数を数える
            enemyCount = FindObjectsOfType<Enemy>().Length;
            //もしも数えてみてエネミー数0ならリスポーン
            if (enemyCount == 0)
            {
                ++waveNumber;//ウェーブ増やす
                SpawnEnemyWave(waveNumber);
                //一行前で++したので次は2体作れる。
                //さらに次にエネミー全滅したら、3体になる
            }
        }
    }
}
