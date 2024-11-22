using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
   public static UIManager instance;

   //Screen object variables
   public GameObject loginUI;
   public GameObject registerUI;

   private void Awake()
   {
    if (instance == null)
    {
        instance = this;
    }
    else if ( instance != null)
    {
        Debug.Log("Phiên bản đã tồn tại, đang hủy đối tượng !");
        Destroy(this);
    }
   }

   public void LoginScreen() //back button
   {
    loginUI.SetActive(true);
    registerUI.SetActive(false);
   }
   public void RegisterScreen() //Register button
   {
    loginUI.SetActive(false);
    registerUI.SetActive(true);
   }
}
