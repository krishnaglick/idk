using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCards : MonoBehaviour {
  public Sprite[] cardFronts;
  public Sprite[] cardBacks;
  public GameObject cardPrefab;

  static System.Random _R = new System.Random();
  static T RandomEnumValue<T>() {
    var v = Enum.GetValues(typeof(T));
    return (T)v.GetValue(_R.Next(v.Length));
  }


  protected void Start() {
    GenerateHand(); // This will ideally be called by a Player script
  }

  // Update is called once per frame
  protected void Update() {

  }

  public List<GameObject> GenerateHand(int size = 5) {
    List<GameObject> hand = new();
    for (int i = 0; i < size + 1; i++) {
      var card = Instantiate(cardPrefab, transform);
      card.transform.position += new Vector3(i * 10, 0, 0);
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
