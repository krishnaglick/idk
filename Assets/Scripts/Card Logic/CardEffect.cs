using UnityEngine;


public class CardEffect : MonoBehaviour {
  public Effect[] effects;

  private void OnMouseUp() {
    var flipped = GetComponent<CardFlip>().flipped;
    if(flipped) {
      var enemies = GameObject.FindGameObjectsWithTag("Enemy");
      // Get random enemy. Only one enemy right now.
      var enemy = enemies[new System.Random().Next(0, enemies.Length)];
      foreach(var effect in effects) {
        switch(effect) {
          case DamageEffect:
            GameManager.Damage(enemy, effect.effectValue);
            break;
          case HealEffect:
            // TODO: This should be "self", not enemy
            GameManager.Heal(enemy, effect.effectValue);
            break;
          case BuffEffect:
            GameManager.ApplyBuff(enemy, (BuffEffect)effect);
            break;
          case DebuffEffect:
            GameManager.ApplyDebuff(enemy, (DebuffEffect)effect);
            break;
        }
      }
    }
  }
}
