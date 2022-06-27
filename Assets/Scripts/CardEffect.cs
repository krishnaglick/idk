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

  void HandleClick(GameObject card) {
    Debug.Log("clicked");
    //Camera.main.GetComponent<Message>().ShowMessage("asdf");
    var message = effectType.ToString() + "-type card";
    GameManager.DoFloatingText(new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), message, Color.black);
  }
}
