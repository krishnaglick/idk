using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
  public Sprite[] Enemies;
  public Sprite GetEnemy() {
    return Enemies[new System.Random().Next(0, Enemies.Length)];
  }
  public GameObject SpawnEnemy(Vector3 position) {
    var enemy = GetEnemy();
    var enemyGO = new GameObject(enemy.name);
    enemyGO.transform.position = new Vector3(position.x, position.y, position.z);
    var enemySprite = enemyGO.AddComponent<SpriteRenderer>();
    enemySprite.sprite = enemy;
    enemyGO.transform.localScale = new Vector3(10, 10, 10);
    return enemyGO;
  }
}
