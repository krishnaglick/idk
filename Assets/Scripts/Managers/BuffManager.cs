using System.Collections.Generic;
using UnityEngine;

// This manages buffs and debuffs
public class BuffManager : MonoBehaviour {
  public List<BuffEffect> buffs = new List<BuffEffect>();
  public List<DebuffEffect> debuffs = new List<DebuffEffect>();

  // Unsure if these should be static
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
  }

  // Unsure if these should be static
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
      // Structs be like
      buffManager.debuffs[debuffIndex] = new DebuffEffect(buffManager.buffs[debuffIndex].effectValue + effect.effectValue, effect.effectType, effect.statusEffect);
    }
  }
}
