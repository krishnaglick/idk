using UnityEngine;

public class HP : MonoBehaviour {
  private int _HP;

  public int GetHP() {
    return _HP;
  }

  public void SetHP(int HP) {
    Debug.Log(HP);
    _HP = HP;
  }
}
