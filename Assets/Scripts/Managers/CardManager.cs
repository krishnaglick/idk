using System;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour {
  public Sprite[] cardFronts;
  public Sprite[] cardBacks;
  public GameObject cardPrefab;

  static System.Random _R = new System.Random();
  static T RandomEnumValue<T>(int? excludeIndex = null) {
    var v = Enum.GetValues(typeof(T));
    int index = _R.Next(v.Length);
    if(excludeIndex != null) {
      while(index == excludeIndex) {
        index = _R.Next(v.Length);
      }
    }
    return (T)v.GetValue(index);
  }

  // Generates a random card
  private GameObject GenerateCard(Vector3 position, Effect[] effects, Sprite cardFront, Sprite cardBack) {
    var card = Instantiate(cardPrefab, position, Quaternion.identity);
    card.transform.position = position;
    card.GetComponent<CardFlip>().frontSprite = cardFront;
    card.GetComponent<CardFlip>().backSprite = cardBack;
    card.GetComponent<CardEffect>().effects = effects;
    card.name = cardFront.name + cardBack.name;

    var cardTextRenderer = Instantiate(FindObjectOfType<EffectsManager>().textPrefab, card.transform.position, Quaternion.identity);
    card.GetComponent<CardText>().CardTextRenderer = cardTextRenderer;

    return card;
  }

  private Effect GetRandomEffect(int? overRide = null) {
    // Override is counter intuitive here, but it's all scaffolded out so why not reuse.
    var rand = overRide != null ? overRide : new System.Random().Next(0, 4);
    switch(rand) {
      case 0:
        return new DamageEffect(new System.Random().Next(1, 10), RandomEnumValue<EffectType>());
      case 1:
        // I only want damage to potentially be transcendent
        return new HealEffect(new System.Random().Next(1, 10), RandomEnumValue<EffectType>((int)EffectType.transcendent));
      case 2:
        return new BuffEffect(
          new System.Random().Next(1, 10),
          RandomEnumValue<EffectType>(),
          RandomEnumValue<StatusEffect>(),
          new System.Random().Next(0, 1)
        ); ;
      case 3:
        return new DebuffEffect(
          new System.Random().Next(1, 10),
          RandomEnumValue<EffectType>(),
          RandomEnumValue<StatusEffect>(),
          new System.Random().Next(0, 1)
        );
      default:
        Debug.LogError("rand is not an expected value: " + rand);
        return new DamageEffect(new System.Random().Next(10), RandomEnumValue<EffectType>()); // This should never be hit.
    }
  }

  private Effect[] GetRandomEffects(int? overRide = null) {
    var numberOfEffects = new System.Random().NextDouble();
    if(numberOfEffects > 0.9) {
      return new Effect[] { GetRandomEffect(overRide), GetRandomEffect(), GetRandomEffect(), GetRandomEffect(), GetRandomEffect() };
    } else if(numberOfEffects > 0.8) {
      return new Effect[] { GetRandomEffect(overRide), GetRandomEffect(), GetRandomEffect(), GetRandomEffect() };
    } else if(numberOfEffects > 0.6) {
      return new Effect[] { GetRandomEffect(overRide), GetRandomEffect(), GetRandomEffect() };
    } else if(numberOfEffects > 0.4) {
      return new Effect[] { GetRandomEffect(overRide), GetRandomEffect() };
    } else {
      return new Effect[] { GetRandomEffect(overRide) };
    }
  }

  public List<GameObject> GenerateHand(Vector3 position, int cards = 5) {
    List<GameObject> hand = new();
    for(int i = 0; i < cards + 1; i++) {
      var spawnCardPosition = new Vector3(position.x + (i * 10), position.y, position.z);
      // Force at least one of each card type: Damage, Heal, Buff, and Debuff
      hand.Add(GenerateCard(spawnCardPosition, GetRandomEffects(i > 3 ? null : i), cardFronts[i % cardFronts.Length], cardBacks[i % cardBacks.Length]));
    }
    return hand;
  }
}
