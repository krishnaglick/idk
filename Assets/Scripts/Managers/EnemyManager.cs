using TMPro;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
  public Sprite[] Enemies;
  public GameObject enemyPrefab;

  public Sprite GetEnemy() {
    return Enemies[new System.Random().Next(0, Enemies.Length)];
  }
  public GameObject SpawnEnemy(Vector3 position) {
    var enemyGO = Instantiate(enemyPrefab, position, Quaternion.identity);
    var enemySpriteRenderer = enemyGO.GetComponent<SpriteRenderer>();
    enemySpriteRenderer.sprite = GetEnemy();

    // Scale up enemy, won't need once custom sprites
    enemyGO.transform.localScale = new Vector3(10, 10, 10);

    var HPText = Instantiate(FindObjectOfType<EffectsManager>().textPrefab, enemyGO.transform.position, Quaternion.identity);
    enemyGO.GetComponent<Enemy>().HPText = HPText;


    return enemyGO;
  }
}
