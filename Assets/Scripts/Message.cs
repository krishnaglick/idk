using TMPro;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
class Message : MonoBehaviour {
  [HideInInspector]
  public string displayText;

  //void Update() {
  //  currentTime = Time.time;
  //  if(_message != null) {
  //    showText = true;
  //  } else {
  //    showText = false;
  //  }

  //  if(executedTime != 0.0f) {
  //    if(currentTime - executedTime > timeToWait) {
  //      executedTime = 0.0f;
  //      _message = null;
  //    }
  //  }
  //}

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