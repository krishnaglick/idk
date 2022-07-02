using TMPro;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class Message : MonoBehaviour {
  public void RenderTemporaryText(string text) {
    var tmp_text = GetComponent<TMP_Text>();
    tmp_text.text = text;
    tmp_text.DOFade(0f, 0.7f);
    transform.DOMove(transform.position + Vector3.up, 0.75f).OnComplete(() => {
      Destroy(gameObject);
    });
  }

  public void RenderText(string text, Vector3? location = null) {
    var tmp_text = GetComponent<TMP_Text>();
    tmp_text.text = text;
    if(location != null) {
      // Stupid C#
      tmp_text.transform.position = (Vector3)location;
    }
  }
}