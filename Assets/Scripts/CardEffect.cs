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
    var message = effectType.ToString() + "-type card";
    GameManager.DoFloatingText(new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), message, Color.black);
    var flipped = FindObjectOfType<CardFlip>().flipped;
    if(flipped && effectType == EffectType.damage) {
      var enemies = GameObject.FindGameObjectsWithTag("Enemy");
      var enemy = enemies[new System.Random().Next(0, enemies.Length)];
      GameManager.Hit(enemy);
    }
  }
}
