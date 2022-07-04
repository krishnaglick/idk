using UnityEngine;
using System;

public class HP : MonoBehaviour {
  public Action HPUpdateEvent;
  private int _HP;

  public int GetHP() {
    return _HP;
  }

  public void SetHP(int HP) {
    if(HP != _HP) {
      _HP = HP;
      HPUpdateEvent?.Invoke();
    }
  }
}
