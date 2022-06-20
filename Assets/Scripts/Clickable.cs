using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClickableEventArgs : EventArgs {
  public GameObject gameObject { get; set; }
}

public class Clickable : MonoBehaviour {
  public Action ClickEvent;
  public delegate void ClickableEventHandler(object sender, ClickableEventArgs e);

  void Update() {
    if (Input.GetMouseButtonDown(0) || Input.touchCount > 0) {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

      // we hit a card
      if (hit.collider != null && hit.collider.gameObject == gameObject) {
        // Action
        ClickEvent.Invoke();
      }
    }
  }
}
