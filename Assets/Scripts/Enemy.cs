using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
  [HideInInspector]
  public int HP;
  [HideInInspector]
  public GameObject HPText;

  //private void Update() {
  //  if(HPText != null && HPText.GetComponent<Message>() != null) {
  //    HPText.GetComponent<Message>().displayText = HP.ToString();
  //  }
  //}

  private void OnGUI() {
    HPText?.GetComponent<Message>()?.RenderText(HP.ToString());
  }
}
