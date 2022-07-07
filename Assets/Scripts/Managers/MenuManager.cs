using UnityEngine;
using System;

public class MenuManager : MonoBehaviour {
  public GameObject[] menus;
  public int startMenuIndex;

  private void Start() {
    ChangeActiveMenu(startMenuIndex);
  }

  private void ChangeActiveMenu(int menuIndex) {
    for(var i = 0; i < menus.Length; i++) {
      if(i == menuIndex) {
        menus[i].SetActive(true);
      } else {
        menus[i].SetActive(false);
      }
    }
  }

  public void SwitchMenu(int menuIndex) {
    ChangeActiveMenu(menuIndex);
  }

  public void SwitchMenu(GameObject menuCanvas) {
    var targetMenuIndex = Array.FindIndex(menus, delegate (GameObject menu) {
      return menu == menuCanvas;
    });
    ChangeActiveMenu(targetMenuIndex);
  }
}