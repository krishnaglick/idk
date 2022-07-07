using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuItem : MonoBehaviour {
  public void SwitchToMenu(GameObject menu) {
    FindObjectOfType<MenuManager>().SwitchMenu(menu);
  }

  public void LoadScene(string sceneName) {
    SceneManager.LoadScene(sceneName);
  }
}
