using TMPro;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
class Message : MonoBehaviour {
  [HideInInspector]
  public string displayText;

  void Start() {
    if(displayText.Length > 0) {
      Debug.Log("Showing a message");
      var tmp_text = GetComponent<TMP_Text>();
      tmp_text.text = displayText;
      tmp_text.DOFade(0f, 0.7f);
      transform.DOMove(transform.position + Vector3.up, 0.75f).OnComplete(() => {
        Destroy(gameObject);
      });
    }
  }
}