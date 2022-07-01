using UnityEngine;
using System.Text;


public class CardEffect : MonoBehaviour {
  public GameObject card;
  public Effect[] effects;
  // Start is called before the first frame update
  void Start() {
    gameObject.GetComponent<Clickable>().ClickEvent += HandleClick;
  }

  public string GetDescription() {
    var description = new StringBuilder();
    foreach(var effect in effects) {
      switch(effect) {
        case DamageEffect:
          description.AppendLine("Deals " + effect.effectValue + " " + effect.effectType + "-type damage");
          break;
        case HealEffect:
          description.AppendLine("Heals " + effect.effectValue + " using " + effect.effectType);
          break;
        case DebuffEffect:
          description.AppendLine("Reduces enemy " + effect.effectType + " " + ((DebuffEffect)effect).statusEffect + " by " + effect.effectValue + "%");
          break;
        case BuffEffect:
          description.AppendLine("Improves " + effect.effectType + " " + ((BuffEffect)effect).statusEffect + " by " + effect.effectValue + "%");
          break;
      }
    }
    return description.ToString();
  }

  void HandleClick(GameObject card) {
    var description = GetDescription();
    GameManager.DoFloatingText(new Vector3(transform.position.x, transform.position.y + 30, transform.position.z), description, Color.black);
    var flipped = FindObjectOfType<CardFlip>().flipped;
    if(flipped) {
      var enemies = GameObject.FindGameObjectsWithTag("Enemy");
      // Get random enemy. Only one enemy right now.
      var enemy = enemies[new System.Random().Next(0, enemies.Length)];

      // TODO: Loop through effects and apply them
      GameManager.Damage(enemy);
    }
  }
}
