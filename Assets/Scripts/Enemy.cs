using UnityEngine;

public class Enemy : MonoBehaviour {
  [HideInInspector]
  public GameObject HPText;
  public int BaseHP = 20;

  public void Start() {
    GetComponent<HP>().SetHP(BaseHP);
  }

  private void OnGUI() {
    SpriteRenderer sr = GetComponent<SpriteRenderer>();
    var x = transform.position.x - sr.bounds.extents.x;
    var y = transform.position.y + sr.bounds.extents.y;
    HPText.GetComponent<Message>().RenderText(GetComponent<HP>().GetHP().ToString(), new Vector3(x, y, transform.position.z));
    if(TryGetComponent<BuffManager>(out var buffManager)) {
      Debug.Log("Buffs: " + buffManager.buffs.Count);
      Debug.Log("Debuffs: " + buffManager.debuffs.Count);
    }
  }
}
