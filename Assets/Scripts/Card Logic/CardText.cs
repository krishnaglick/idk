using UnityEngine;
using System.Text;

public class CardText : MonoBehaviour {
  [HideInInspector]
  public GameObject CardTextRenderer;

  private string GetDescription(Effect[] effects) {
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
          var buffText = BuffManager.GenerateBuffText((BuffEffect)effect);
          if(buffText != "") {
            description.AppendLine(buffText);
          }
          break;
        case DebuffEffect:
          // TODO: Card effect text != buff/debuff text!
          var debuffText = BuffManager.GenerateDebuffText((DebuffEffect)effect);
          if(debuffText != "") {
            description.AppendLine(debuffText);
          }
          break;
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
