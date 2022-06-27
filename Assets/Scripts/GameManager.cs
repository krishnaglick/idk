using UnityEngine;
using TMPro;
using DG.Tweening;

public class GameManager : MonoBehaviour {
  public static void DoFloatingText(Vector3 position, string text, Color c) {
    EffectsManager effectsManager = FindObjectOfType<EffectsManager>();
    GameObject floatingText = Instantiate(effectsManager.textPrefab, position, Quaternion.identity);
    floatingText.GetComponent<TMP_Text>().color = c;
    floatingText.GetComponent<Message>().displayText = text;
  }
}
