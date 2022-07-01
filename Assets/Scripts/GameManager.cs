using UnityEngine;
using TMPro;
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
    floatingText.GetComponent<Message>().RenderTemporaryText(text);
  }

  public static List<GameObject> SpawnCards(Vector3 position, int cards = 5) {
    var cardSpawner = FindObjectOfType<CardManager>();
    return cardSpawner.GenerateHand(position, cards);
  }

  public static GameObject SpawnEnemy(Vector3 position) {
    EnemyManager enemyManager = FindObjectOfType<EnemyManager>();
    return enemyManager.SpawnEnemy(position);
  }

  public static void Damage(GameObject target, int damage = 1) {
    var HP = target.GetComponent<HP>();
    HP.SetHP(HP.GetHP() - damage);
  }

  public static void Heal(GameObject target, int healing = 1) {
    var HP = target.GetComponent<HP>();
    HP.SetHP(HP.GetHP() + healing);
  }
}
