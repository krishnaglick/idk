using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour {
  public Sprite[] Enemies;
  public GameObject enemyPrefab;

  public Sprite GetEnemy() {
    return Enemies[new System.Random().Next(0, Enemies.Length)];
  }
  public GameObject SpawnEnemy(Vector3 position) {
    var enemy = GetEnemy();
    var enemyGO = Instantiate(enemyPrefab, position, Quaternion.identity);
    var enemySprite = enemyGO.GetComponent<SpriteRenderer>();
    enemySprite.sprite = enemy;
    enemyGO.transform.localScale = new Vector3(10, 10, 10);
    return enemyGO;
  }
}
