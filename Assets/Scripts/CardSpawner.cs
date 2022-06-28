using System;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : MonoBehaviour {
  public Sprite[] cardFronts;
  public Sprite[] cardBacks;
  public GameObject cardPrefab;

  static System.Random _R = new System.Random();
  static T RandomEnumValue<T>() {
    var v = Enum.GetValues(typeof(T));
    return (T)v.GetValue(_R.Next(v.Length));
  }

  private GameObject GenerateCard(Vector3 position, EffectType effectType, Sprite cardFront, Sprite cardBack) {
    var card = Instantiate(cardPrefab, position, Quaternion.identity);
    card.transform.position = position;
    card.GetComponent<CardFlip>().frontSprite = cardFront;
    card.GetComponent<CardFlip>().backSprite = cardBack;
    card.GetComponent<CardEffect>().card = card;
    card.GetComponent<CardEffect>().effectType = effectType;
    card.name = cardFront.name + cardBack.name;

    return card;
  }

  public List<GameObject> GenerateHand(Vector3 position, int cards = 5) {
    List<GameObject> hand = new();
    for(int i = 0; i < cards + 1; i++) {
      var spawnCardPosition = new Vector3(position.x + (i * 10), position.y, position.z);
      hand.Add(GenerateCard(spawnCardPosition, RandomEnumValue<EffectType>(), cardFronts[i % cardFronts.Length], cardBacks[i % cardBacks.Length]));
    }

    var damageCardPosition = new Vector3(position.x + ((cards + 1) * 10), position.y, position.z);
    hand.Add(GenerateCard(damageCardPosition, EffectType.damage, cardFronts[0], cardBacks[1]));

    return hand;
  }
}
