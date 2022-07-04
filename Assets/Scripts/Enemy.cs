using UnityEngine;
using System.Text;

[RequireComponent(typeof(BuffManager))]
[RequireComponent(typeof(HP))]
public class Enemy : MonoBehaviour {
  [HideInInspector]
  public GameObject HPText;
  public int BaseHP = 20;

  public void Start() {
    GetComponent<HP>().SetHP(BaseHP);
    GetComponent<HP>().HPUpdateEvent += UpdateStatus;
    GetComponent<BuffManager>().BuffUpdateEvent += UpdateStatus;
    UpdateStatus();
  }

  private void UpdateStatus() {
    SpriteRenderer sr = GetComponent<SpriteRenderer>();
    var x = transform.position.x - sr.bounds.extents.x;
    var y = transform.position.y + sr.bounds.extents.y;
    var status = new StringBuilder();
    status.AppendLine("HP: " + GetComponent<HP>().GetHP());
    if(TryGetComponent<BuffManager>(out var buffManager)) {
      // Add buffs to status
      buffManager.buffs.ForEach(delegate (BuffEffect buff) {
        var buffText = BuffManager.GenerateBuffText(buff);
        if(buffText != "") {
          status.AppendLine(buffText);
        }
      });

      // Add debuffs to status
      buffManager.debuffs.ForEach(delegate (DebuffEffect debuff) {
        var debuffText = BuffManager.GenerateDebuffText(debuff);
        if(debuffText != "") {
          status.AppendLine(debuffText);
        }
      });
    }

    HPText.GetComponent<Message>().RenderText(status.ToString(), new Vector3(x, y, transform.position.z));
  }
}
