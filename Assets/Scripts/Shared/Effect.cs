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
}
public enum StatusEffect {
  damage,
  healing,
  resistance,
  immunity,
}

public interface Effect {
  public double effectValue { get; set; }
  public EffectType effectType { get; set; }
}

public struct DamageEffect : Effect {
  public double effectValue { get; set; }
  public EffectType effectType { get; set; }
  public DamageEffect(double effectValue, EffectType effectType) {
    this.effectValue = effectValue;
    this.effectType = effectType;
  }
}

public struct HealEffect : Effect {
  public double effectValue { get; set; }
  public EffectType effectType { get; set; }
  public HealEffect(double effectValue, EffectType effectType) {
    this.effectValue = effectValue;
    this.effectType = effectType;
  }
}

public interface EffectWithStatus : Effect {
  public StatusEffect statusEffect { get; set; }
}

public struct BuffEffect : EffectWithStatus {
  public double effectValue { get; set; }
  public EffectType effectType { get; set; }
  public StatusEffect statusEffect { get; set; }
  public BuffEffect(double effectValue, EffectType effectType, StatusEffect statusEffect) {
    this.effectValue = effectValue;
    this.effectType = effectType;
    this.statusEffect = statusEffect;
  }
}

public class DebuffEffect : EffectWithStatus {
  public double effectValue { get; set; }
  public EffectType effectType { get; set; }
  public StatusEffect statusEffect { get; set; }
  public DebuffEffect(double effectValue, EffectType effectType, StatusEffect statusEffect) {
    this.effectValue = effectValue;
    this.effectType = effectType;
    this.statusEffect = statusEffect;
  }
}