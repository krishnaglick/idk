using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
    ClickEvent += HandleClick;
  }

  // Update is called once per frame
  void HandleClick() {
    Debug.Log("clicked");
  }
}
