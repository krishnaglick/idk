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

  public List<GameObject> GenerateHand(Vector3 position, int cards = 5) {
    List<GameObject> hand = new();
    for(int i = 0; i < cards + 1; i++) {
      var card = Instantiate(cardPrefab, position, Quaternion.identity);
      card.transform.position = new Vector3(position.x + (i * 10), position.y, position.z);
      var cardFront = cardFronts[i % cardFronts.Length];
      var cardBack = cardBacks[i % cardBacks.Length];
      card.GetComponent<CardFlip>().frontSprite = cardFront;
      card.GetComponent<CardFlip>().backSprite = cardBack;
      card.GetComponent<CardEffect>().card = card;
      card.GetComponent<CardEffect>().effectType = RandomEnumValue<EffectType>();
      card.name = cardFront.name + cardBack.name;
      hand.Add(card);
    }

    return hand;
  }
}
