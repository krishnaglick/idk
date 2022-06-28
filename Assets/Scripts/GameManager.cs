using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
  private void Start() {
    SpawnCards(Vector3.zero);
    SpawnEnemy(new Vector3(Vector3.zero.x + 25, Vector3.zero.y + 20, Vector3.zero.z));
  }

  public static void DoFloatingText(Vector3 position, string text, Color c) {
    EffectsManager effectsManager = FindObjectOfType<EffectsManager>();
    GameObject floatingText = Instantiate(effectsManager.textPrefab, position, Quaternion.identity);
    floatingText.GetComponent<TMP_Text>().color = c;
    floatingText.GetComponent<Message>().displayText = text;
  }

  public static List<GameObject> SpawnCards(Vector3 position, int cards = 5) {
    CardSpawner cardSpawner = FindObjectOfType<CardSpawner>();
    return cardSpawner.GenerateHand(position, cards);
  }
  public static GameObject SpawnEnemy(Vector3 position) {
    EnemyManager enemyManager = FindObjectOfType<EnemyManager>();
    return enemyManager.SpawnEnemy(position);
  }
}
