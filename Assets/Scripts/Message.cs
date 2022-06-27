using TMPro;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
class Message : MonoBehaviour {
  private bool showText = false;
  private string _message = null;

  private float currentTime = 0.0f;
  private float executedTime = 0.0f;
  private readonly float timeToWait = 5.0f;

  public void ShowMessage(string message) {
    executedTime = Time.time;
    _message = message;
  }

  void Update() {
    currentTime = Time.time;
    if(_message != null) {
      showText = true;
    } else {
      showText = false;
    }

    if(executedTime != 0.0f) {
      if(currentTime - executedTime > timeToWait) {
        executedTime = 0.0f;
        _message = null;
      }
    }
  }

  void OnGUI() {
    if(showText) {
      Debug.Log("Showing a message");
      TMP_Text tmp_text = Camera.main.GetComponent<TMP_Text>();
      tmp_text.text = _message;
      ShortcutExtensions.DOFade(tmp_text.material, 0f, 0.7f);
      transform.DOMove(transform.position + Vector3.up, 0.75f).OnComplete(() => {
        Destroy(gameObject);
      });
    }
  }
}