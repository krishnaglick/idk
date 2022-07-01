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

public class Effect {
  public double effectValue;
  public EffectType effectType;
  public Effect(double effectValue, EffectType effectType) {
    this.effectValue = effectValue;
    this.effectType = effectType;
  }
}

public class DamageEffect : Effect {
  public DamageEffect(double effectValue, EffectType effectType) : base(effectValue, effectType) { }
}

public class HealEffect : Effect {
  public HealEffect(double effectValue, EffectType effectType) : base(effectValue, effectType) { }
}

public class BuffEffect : Effect {
  public StatusEffect statusEffect;
  public BuffEffect(double effectValue, EffectType effectType, StatusEffect statusEffect) : base(effectValue, effectType) {
    this.statusEffect = statusEffect;
  }
}

public class DebuffEffect : BuffEffect {
  public DebuffEffect(double effectValue, EffectType effectType, StatusEffect statusEffect) : base(effectValue, effectType, statusEffect) { }
}