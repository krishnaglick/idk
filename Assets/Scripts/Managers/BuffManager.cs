using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

// This manages buffs and debuffs
public class BuffManager : MonoBehaviour {
  public List<BuffEffect> buffs = new();
  public List<DebuffEffect> debuffs = new();

  [HideInInspector]
  public Action BuffUpdateEvent;

  public static void ApplyBuff(GameObject target, BuffEffect effect) {
    if(!target.TryGetComponent<BuffManager>(out var buffManager)) {
      buffManager = target.AddComponent<BuffManager>();
    }

    var buffIndex = buffManager.buffs.FindIndex(delegate (BuffEffect buff) {
      return buff.effectType == effect.effectType && buff.statusEffect == effect.statusEffect;
    });

    if(buffIndex == -1) {
      buffManager.buffs.Add(effect);
    } else {
      // Structs be like
      buffManager.buffs[buffIndex] = new BuffEffect(buffManager.buffs[buffIndex].effectValue + effect.effectValue, effect.effectType, effect.statusEffect);
    }

    buffManager.BuffUpdateEvent?.Invoke();
  }

  public static void ApplyDebuff(GameObject target, DebuffEffect effect) {
    if(!target.TryGetComponent<BuffManager>(out var buffManager)) {
      buffManager = target.AddComponent<BuffManager>();
    }

    var debuffIndex = buffManager.debuffs.FindIndex(delegate (DebuffEffect debuff) {
      return debuff.effectType == effect.effectType && debuff.statusEffect == effect.statusEffect;
    });

    if(debuffIndex == -1) {
      buffManager.debuffs.Add(effect);
    } else {
      // Currently buffs & debuffs stack value. So if you have 4 fire resist and apply 4 more, you'll have 8; it's additive.
      // I'm not yet sure how I want all this logic to play out, but additive is usually stronger than multiplicative.
      // Buffs could be more individualistic, where you have two +4 fire resist.
      // The current thought for buff/debuff decay is lose one stack/turn. But with the second option I can have a per-buff duration.
      buffManager.debuffs[debuffIndex] = new DebuffEffect(buffManager.buffs[debuffIndex].effectValue + effect.effectValue, effect.effectType, effect.statusEffect);
    }

    buffManager.BuffUpdateEvent?.Invoke();
  }

  private static string capitalizeEffectType(EffectType effectType) {
    var effectTypeAsString = effectType.ToString();
    return effectTypeAsString.First().ToString().ToUpper() + string.Join("", effectTypeAsString.Skip(1));
  }

  // There's some implied game logic in here. Worth extracting somewhere when I know how I want this all to work
  public static string GenerateBuffText(BuffEffect buff) {
    switch(buff.statusEffect) {
      case StatusEffect.absorb:
        // Can absorb DOTs, or normal damage.
        switch(buff.effectType) {
          case EffectType.transcendent:
            Debug.Log("Buff tried to apply " + buff.effectType + " absorbtion");
            break;
          default:
            return "Absorbs " + (buff.effectValue != int.MaxValue ? (buff.effectValue + " points of ") : "") + capitalizeEffectType(buff.effectType) + " damage";
        }
        break;
      case StatusEffect.damage:
        switch(buff.effectType) {
          case EffectType.bleed:
          case EffectType.poison:
          case EffectType.burn:
          case EffectType.frostbite:
          case EffectType.suffocation:
          case EffectType.transcendent:
            Debug.Log("Buff tried to apply " + buff.effectType + " damage boost");
            break;
          default:
            return "Deals " + buff.effectValue + "% more " + capitalizeEffectType(buff.effectType) + " damage";
        }
        break;
      case StatusEffect.healing:
        switch(buff.effectType) {
          case EffectType.nature:
          case EffectType.holy:
          case EffectType.water:
            return "Does " + buff.effectValue + " " + capitalizeEffectType(buff.effectType) + " healing every turn";
          case EffectType.transcendent:
            // Edge-case: transcendant type healing just puts a shield on the unit
            // This may be better as a status effect.
            return "Absorbs " + buff.effectValue + " points of damage";
          default:
            Debug.Log("Buff tried to apply " + buff.effectType + " healing");
            break;
        }
        break;
      case StatusEffect.immunity:
        switch(buff.effectType) {
          case EffectType.transcendent:
            Debug.Log("Buff tried to apply " + buff.effectType + " immunity");
            break;
          default:
            return "Immune to " + capitalizeEffectType(buff.effectType);
        }
        break;
      case StatusEffect.resistance:
        switch(buff.effectType) {
          case EffectType.bleed:
          case EffectType.poison:
          case EffectType.burn:
          case EffectType.frostbite:
          case EffectType.suffocation:
            Debug.Log("Buff tried to apply " + buff.effectType + " resistance");
            break;
          case EffectType.transcendent:
            return "Provides " + buff.effectValue + "% resistance to all damage";
          default:
            return "Provides " + buff.effectValue + "% resistance to " + capitalizeEffectType(buff.effectType);
        }
        break;
      default:
        break;
    }

    return "";
  }

  // There's some implied game logic in here. Worth extracting somewhere when I know how I want this all to work
  public static string GenerateDebuffText(DebuffEffect debuff) {
    switch(debuff.statusEffect) {
      // Eats healing
      case StatusEffect.absorb:
        switch(debuff.effectType) {
          case EffectType.nature:
          case EffectType.holy:
          case EffectType.water:
            return "Absorbs " + (debuff.effectValue != int.MaxValue ? (debuff.effectValue + " points of ") : "") + capitalizeEffectType(debuff.effectType) + " healing";
          default:
            Debug.Log("Buff tried to apply " + debuff.effectType + " absorbtion");
            break;
        }
        break;
      case StatusEffect.damage:
        switch(debuff.effectType) {
          case EffectType.bleed:
          case EffectType.poison:
          case EffectType.burn:
          case EffectType.frostbite:
          case EffectType.suffocation:
            return "Deals " + debuff.effectValue + " " + capitalizeEffectType(debuff.effectType) + " damage per turn";
          default:
            Debug.Log("Debuff tried to apply " + debuff.effectType + " damage per turn");
            break;
        }
        break;
      case StatusEffect.healing:
        switch(debuff.effectType) {
          case EffectType.nature:
          case EffectType.holy:
          case EffectType.water:
            return "Reduces healing from  " + capitalizeEffectType(debuff.effectType) + " sources by" + debuff.effectValue + "%";
          default:
            Debug.Log("Buff tried to apply " + debuff.effectType + " healing reduction");
            break;
        }
        break;
      case StatusEffect.resistance:
        switch(debuff.effectType) {
          case EffectType.bleed:
          case EffectType.poison:
          case EffectType.burn:
          case EffectType.frostbite:
          case EffectType.suffocation:
            Debug.Log("Debuff tried to apply " + debuff.effectType + " damage boost");
            break;
          default:
            return "Reduces " + capitalizeEffectType(debuff.effectType) + " damage done by " + debuff.effectValue + "%";
        }
        break;
      case StatusEffect.immunity:
        switch(debuff.effectType) {
          case EffectType.transcendent:
            return "Purges " + debuff.effectValue + " buff(s)";
          default:
            break;
        }
        break;
      default:
        break;
    }

    return "";
  }
}
