public enum EffectType {
  transcendent,

  slashing,
  piercing,
  blunt,

  fire,
  ice,
  water,
  nature,
  lightning,
  holy,
  chaos,

  bleed,
  poison,
  burn,
  frostbite,
  suffocation,
  // More DOT damage types?
}
public enum StatusEffect {
  damage,
  healing,
  absorb,
  resistance,
  immunity,
  energy,
  health,
}

public interface Effect {
  public int effectValue { get; set; }
  public EffectType effectType { get; set; }
}

public struct DamageEffect : Effect {
  public int effectValue { get; set; }
  public EffectType effectType { get; set; }
  public DamageEffect(int effectValue, EffectType effectType) {
    this.effectValue = effectValue;
    this.effectType = effectType;
  }
}

public struct HealEffect : Effect {
  public int effectValue { get; set; }
  public EffectType effectType { get; set; }
  public HealEffect(int effectValue, EffectType effectType) {
    this.effectValue = effectValue;
    this.effectType = effectType;
  }
}

public interface EffectWithStatus : Effect {
  public StatusEffect statusEffect { get; set; }
  // How much of the buff is lost per turn
  public int burnRate { get; set; }
}

public struct BuffEffect : EffectWithStatus {
  public int effectValue { get; set; }
  public EffectType effectType { get; set; }
  public StatusEffect statusEffect { get; set; }
  public int burnRate { get; set; }

  public BuffEffect(int effectValue, EffectType effectType, StatusEffect statusEffect, int burnRate) {
    this.effectValue = effectValue;
    this.effectType = effectType;
    this.statusEffect = statusEffect;
    this.burnRate = burnRate;
  }
}

public class DebuffEffect : EffectWithStatus {
  public int effectValue { get; set; }
  public EffectType effectType { get; set; }
  public StatusEffect statusEffect { get; set; }
  public int burnRate { get; set; }

  public DebuffEffect(int effectValue, EffectType effectType, StatusEffect statusEffect, int burnRate) {
    this.effectValue = effectValue;
    this.effectType = effectType;
    this.statusEffect = statusEffect;
    this.burnRate = burnRate;
  }
}