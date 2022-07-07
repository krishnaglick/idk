using UnityEngine;
using System.Text;
using System.Linq;

public class CardText : MonoBehaviour {
  [HideInInspector]
  public GameObject CardTextRenderer;

  private string GetDescription(Effect[] effects) {
    var description = new StringBuilder();
    var buffEffects = effects.OfType<BuffEffect>();
    var debuffEffects = effects.OfType<DebuffEffect>();
    var aggregatedEffects = BuffManager.Aggregate(buffEffects.ToArray(), debuffEffects.ToArray());
    foreach(var effect in effects) {
      switch(effect) {
        case DamageEffect:
          description.AppendLine("Deals " + effect.effectValue + " " + effect.effectType + "-type damage");
          break;
        case HealEffect:
          description.AppendLine("Heals " + effect.effectValue + " using " + effect.effectType);
          break;
        default:
          break;
      }
    }

    foreach(var buff in aggregatedEffects.buffs) {
      var buffText = BuffManager.GenerateBuffText(buff);
      if(buffText != "") {
        description.AppendLine(buffText);
      }
    }

    foreach(var debuff in aggregatedEffects.debuffs) {
      var debuffText = BuffManager.GenerateDebuffText(debuff);
      if(debuffText != "") {
        description.AppendLine(debuffText);
      }
    }

    return description.ToString();
  }

  private void OnMouseOver() {
    var flipped = GetComponent<CardFlip>().flipped;
    if(flipped) {
      var effects = GetComponent<CardEffect>().effects;
      var description = GetDescription(effects);
      CardTextRenderer.GetComponent<Message>().RenderText(description);
      CardTextRenderer.GetComponent<Message>().GetComponent<MeshRenderer>().enabled = true;
    }
  }

  private void OnMouseExit() {
    CardTextRenderer.GetComponent<Message>().GetComponent<MeshRenderer>().enabled = false;
  }
}
