using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Clickable : MonoBehaviour {
  public Action ClickEvent;

  void Update() {
    if (Input.GetMouseButtonDown(0) || Input.touchCount > 0) {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

      if (hit.collider != null && hit.collider.gameObject == gameObject) {
        ClickEvent?.Invoke();
      }
    }
  }
}
