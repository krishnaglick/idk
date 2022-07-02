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
    card.GetComponent<CardEffect>().card = card;
    card.GetComponent<CardEffect>().effects = effects;
    card.name = cardFront.name + cardBack.name;

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
        return new BuffEffect(new System.Random().Next(1, 10), RandomEnumValue<EffectType>((int)EffectType.transcendent), RandomEnumValue<StatusEffect>());
      case 3:
        return new DebuffEffect(new System.Random().Next(1, 10), RandomEnumValue<EffectType>((int)EffectType.transcendent), RandomEnumValue<StatusEffect>());
      default:
        Debug.LogError("rand is not an expected value: " + rand);
        return new DamageEffect(new System.Random().Next(10), RandomEnumValue<EffectType>()); // This should never be hit.
    }
  }

  private Effect[] GetRandomEffects() {
    var numberOfEffects = new System.Random().NextDouble();
    if(numberOfEffects > 0.9) {
      return new Effect[] { GetRandomEffect(), GetRandomEffect(), GetRandomEffect(), GetRandomEffect(), GetRandomEffect() };
    } else if(numberOfEffects > 0.8) {
      return new Effect[] { GetRandomEffect(), GetRandomEffect(), GetRandomEffect(), GetRandomEffect() };
    } else if(numberOfEffects > 0.6) {
      return new Effect[] { GetRandomEffect(), GetRandomEffect(), GetRandomEffect() };
    } else if(numberOfEffects > 0.4) {
      return new Effect[] { GetRandomEffect(), GetRandomEffect() };
    } else {
      return new Effect[] { GetRandomEffect() };
    }
  }

  public List<GameObject> GenerateHand(Vector3 position, int cards = 5) {
    List<GameObject> hand = new();
    for(int i = 0; i < cards + 1; i++) {
      var spawnCardPosition = new Vector3(position.x + (i * 10), position.y, position.z);
      hand.Add(GenerateCard(spawnCardPosition, GetRandomEffects(), cardFronts[i % cardFronts.Length], cardBacks[i % cardBacks.Length]));
    }

    // Guarentee a damage card for testing. Should really just create 1 of each type manually.
    var damageCardPosition = new Vector3(position.x + ((cards + 1) * 10), position.y, position.z);
    hand.Add(GenerateCard(damageCardPosition, new Effect[] { GetRandomEffect(0) }, cardFronts[0], cardBacks[1]));

    return hand;
  }
}
