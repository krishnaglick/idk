using System.Collections;
using UnityEngine;

public class CardFlip : MonoBehaviour {
  public Sprite frontSprite;
  public Sprite backSprite;
  public float uncoverTime = 12.0f;

  private GameObject cardFront;
  private GameObject cardBack;
  private bool flipping = false;
  private bool flipped = false;

  // Use this for initialization
  protected void Start() {
    cardFront = new GameObject("CardFront");
    cardBack = new GameObject("CardBack");

    // front (motive)
    cardFront.AddComponent<SpriteRenderer>();
    cardFront.GetComponent<SpriteRenderer>().sprite = frontSprite;
    cardFront.GetComponent<SpriteRenderer>().sortingOrder = -1;

    cardFront.transform.SetParent(transform);
    cardFront.transform.SetPositionAndRotation(transform.position, transform.rotation);

    // back
    cardBack.AddComponent<SpriteRenderer>();
    cardBack.GetComponent<SpriteRenderer>().sprite = backSprite;
    cardBack.GetComponent<SpriteRenderer>().sortingOrder = 1;

    cardBack.transform.SetParent(transform);
    cardBack.transform.SetPositionAndRotation(transform.position, transform.rotation);

    gameObject.AddComponent<BoxCollider2D>();
    gameObject.GetComponent<BoxCollider2D>().size = frontSprite.bounds.size;

    gameObject.GetComponent<Clickable>().ClickEvent += HandleClick;
  }


  void HandleClick(GameObject card) {
    if(!flipped) {
      cardFront.GetComponent<SpriteRenderer>().sprite = frontSprite;
      cardBack.GetComponent<SpriteRenderer>().sprite = backSprite;
      if(!flipping) {
        flipping = true;
        // This seems to run twice. Idk why.
        gameObject.GetComponent<Clickable>().ClickEvent -= HandleClick;
        StartCoroutine(uncoverCard(card.transform, true));
      }
    }
  }

  IEnumerator uncoverCard(Transform card, bool uncover) {
    float minAngle = uncover ? 0 : 180;
    float maxAngle = uncover ? 180 : 0;

    float t = 0;
    bool uncovered = false;

    while(t < 1f) {
      t += Time.deltaTime * uncoverTime;

      float angle = Mathf.LerpAngle(minAngle, maxAngle, t);
      card.eulerAngles = new Vector2(0, angle);

      if(((angle >= 90 && angle < 180) || (angle >= 270 && angle < 360)) && !uncovered) {
        uncovered = true;
        flipping = false;
        flipped = true;
        for(int i = 0; i < card.childCount; i++) {
          // reverse sorting order to show the other side of the card
          // otherwise you would still see the same sprite because they are sorted
          // by order not distance (by default)
          Transform c = card.GetChild(i);
          var sprite = c.GetComponent<SpriteRenderer>();
          sprite.sortingOrder *= -1;

          yield return null;
        }
      }

      yield return null;
    }

    yield return 0;
  }
}
