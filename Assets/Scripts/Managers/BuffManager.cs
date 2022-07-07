using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public struct EffectAggregation {
  public BuffEffect[] buffs { get; }
  public DebuffEffect[] debuffs { get; }

  public EffectAggregation(BuffEffect[] buffs, DebuffEffect[] debuffs) {
    this.buffs = buffs ?? new BuffEffect[] { };
    this.debuffs = debuffs ?? new DebuffEffect[] { };
  }
}

// This manages buffs and debuffs
public class BuffManager : MonoBehaviour {
  public List<BuffEffect> buffs = new();
  public List<DebuffEffect> debuffs = new();

  [HideInInspector]
  public EffectAggregation AggregatedEffects;

  private void Start() {
    AggregatedEffects = Aggregate(buffs, debuffs);
  }

  [HideInInspector]
  public Action BuffUpdateEvent;

  public static void ApplyBuff(GameObject target, BuffEffect effect) {
    if(!target.TryGetComponent<BuffManager>(out var buffManager)) {
      buffManager = target.AddComponent<BuffManager>();
    }

    // Always add a new buff/debuff, and sort it out @ the display level.
    buffManager.buffs.Add(effect);

    buffManager.AggregatedEffects = Aggregate(buffManager.buffs, buffManager.debuffs);
    buffManager.BuffUpdateEvent?.Invoke();
  }

  public static void ApplyDebuff(GameObject target, DebuffEffect effect) {
    if(!target.TryGetComponent<BuffManager>(out var buffManager)) {
      buffManager = target.AddComponent<BuffManager>();
    }
    buffManager.debuffs.Add(effect);

    buffManager.AggregatedEffects = Aggregate(buffManager.buffs, buffManager.debuffs);
    buffManager.BuffUpdateEvent?.Invoke();
  }

  public static EffectAggregation Aggregate(List<BuffEffect> buffs, List<DebuffEffect> debuffs) {
    return Aggregate(buffs.ToArray(), debuffs.ToArray());
  }
  public static EffectAggregation Aggregate(BuffEffect[] buffs, DebuffEffect[] debuffs) {
    var buffSum = new Dictionary<(EffectType, StatusEffect), BuffEffect>();
    var debuffSum = new Dictionary<(EffectType, StatusEffect), DebuffEffect>();

    foreach(var buff in buffs) {
      var key = (buff.effectType, buff.statusEffect);
      if(!buffSum.ContainsKey(key)) {
        buffSum.Add(key, buff);
      } else {
        var effect = buffSum[key];
        effect.effectValue += buff.effectValue;
        buffSum[key] = effect;
      }
    }

    foreach(var debuff in debuffs) {
      var key = (debuff.effectType, debuff.statusEffect);
      if(!debuffSum.ContainsKey(key)) {
        debuffSum.Add(key, debuff);
      } else {
        var effect = debuffSum[key];
        effect.effectValue += debuff.effectValue;
        debuffSum[key] = effect;
      }
    }

    return new EffectAggregation(buffSum.Values.ToArray(), debuffSum.Values.ToArray());
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
