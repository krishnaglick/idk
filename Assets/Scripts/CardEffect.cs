using UnityEngine;

public enum EffectType {
  damage,
  healing,
  buff,
  debuff,
};

public class CardEffect : Clickable {
  public GameObject card;
  public EffectType effectType;
  // Start is called before the first frame update
  void Start() {
    gameObject.GetComponent<Clickable>().ClickEvent += HandleClick;
  }

  void HandleClick() {
    Debug.Log("clicked");
  }
}
