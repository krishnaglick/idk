using UnityEngine;

public class Enemy : MonoBehaviour {
  [HideInInspector]
  public GameObject HPText;
  public int BaseHP = 20;

  public void Start() {
    GetComponent<HP>().SetHP(BaseHP);
  }

  private void OnGUI() {
    HPText.GetComponent<Message>().RenderText(GetComponent<HP>().GetHP().ToString());
  }
}
