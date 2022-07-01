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
        case BuffEffect:
          description.AppendLine("Improves " + effect.effectType + " " + ((BuffEffect)effect).statusEffect + " by " + effect.effectValue + "%");
          break;
        case DebuffEffect:
          description.AppendLine("Reduces enemy " + effect.effectType + " " + ((DebuffEffect)effect).statusEffect + " by " + effect.effectValue + "%");
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
      foreach(var effect in effects) {
        switch(effect) {
          case DamageEffect:
            GameManager.Damage(enemy, (int)effect.effectValue);
            break;
          case HealEffect:
            // TODO: This should be "self", not enemy
            GameManager.Heal(enemy, (int)effect.effectValue);
            break;
          case BuffEffect:
            // TODO: Allow for buff application and stacking
            break;
          case DebuffEffect:
            // TODO: Allow for debuff application and stacking
            break;
        }
      }
    }
  }
}
