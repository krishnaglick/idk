using System.Collections.Generic;
using UnityEngine;

public class SpawnCards : MonoBehaviour {
  public Sprite[] cardFronts;
  public Sprite[] cardBacks;
  public GameObject cardPrefab;
  // Start is called before the first frame update

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
      card.GetComponent<CardFlip>().frontSprite = cardFronts[i % cardFronts.Length];
      card.GetComponent<CardFlip>().backSprite = cardBacks[i % cardBacks.Length];
      card.GetComponent<CardEffect>().card = card;
      hand.Add(card);
    }

    return hand;
  }
}
