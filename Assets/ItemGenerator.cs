using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour {

    public GameObject carPrefab;
    public GameObject coinPrefab;
    public GameObject conePrefab;

    //int startPos = -160;
    //int goalPos = 120;
    //アイテムを出すx方向の範囲
    float posRange = 3.4f;
    float generatePos;

    public void GenerateItems()
    {
        generatePos = GameObject.FindGameObjectWithTag("Respawn").transform.position.z + 45.0f;
        Debug.Log("Instantiate");
        //一定の距離ごとにアイテムを生成
        for (float i = generatePos; i < generatePos + 60; i += 15)
        {
            //どのアイテムを出すのかをランダムに設定。numは0～9で、numが1以下ならコーンを生成。
            int num = Random.Range(0, 10);
            if (num <= 1)
            {
                //コーンをx軸方向に一直線に生成
                for (float j = -1; j <= 1; j += 0.4f)
                {
                    GameObject cone = Instantiate(conePrefab) as GameObject;
                    cone.transform.position = new Vector3(4 * j, cone.transform.position.y, i);
                }
            }
            else
            {
                //レーンごとにアイテムを生成。-1、0、1の3レーン
                for (int j = -1; j < 2; j++)
                {
                    //アイテムの種類を決める。1～10
                    int item = Random.Range(1, 11);
                    //アイテムを置くZ座標のオフセットをランダムに設定
                    int offsetZ = Random.Range(-5, 6);
                    //60%コイン配置:30%車配置:10%何もなし
                    if (1 <= item && item <= 6)
                    {
                        //コインを生成
                        GameObject coin = Instantiate(coinPrefab) as GameObject;
                        coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, i + offsetZ);
                    }
                    else if (7 <= item && item <= 9)
                    {
                        //車を生成
                        GameObject car = Instantiate(carPrefab) as GameObject;
                        car.transform.position = new Vector3(posRange * j, car.transform.position.y, i + offsetZ);
                    }
                }
            }
        }
        this.transform.Translate(0, 0, + 60);
    }
}
