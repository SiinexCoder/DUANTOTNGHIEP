using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseController : MonoBehaviour
{
    public GameObject loginPanel, signupPanel, profilePanel, notificationPanel;
    public InputField loginEmail, loginPassword, signupEmail, signupPassword, signupCPassword, signupUserName, forgetPassEmail;
    public Text notif_Title_Text,notif_Message_Text,profileUserName_Text,profileUserEmail_Text;

    public Toggle rememberMe;
    // Open Login Panel
    public void OpenLoginPanel()
    {
        loginPanel.SetActive(true);
        signupPanel.SetActive(false);
        profilePanel.SetActive(false);
    }

    // Open Signup Panel
    public void OpenSignUpPanel()
    {
        loginPanel.SetActive(false);
        signupPanel.SetActive(true);
        profilePanel.SetActive(false);
    }

    // Open Profile Panel
    public void OpenProfilePanel()
    {
        loginPanel.SetActive(false);
        signupPanel.SetActive(false);
        profilePanel.SetActive(true);
    }

    // Login User
    public void LoginUser()
    {
        if (string.IsNullOrEmpty(loginEmail.text) || string.IsNullOrEmpty(loginPassword.text))
        {
            showNotificationMessage("Error" , "Login Email Empty");
            return;
        }

    }

    // Signup User
    public void SignUpUser()
    {
        if (string.IsNullOrEmpty(signupEmail.text) || 
            string.IsNullOrEmpty(signupPassword.text) || 
            string.IsNullOrEmpty(signupCPassword.text) || 
            string.IsNullOrEmpty(signupUserName.text))
        {   
            showNotificationMessage("Error" , "Forget Email Empty"); 
            return;
        }
    
    }

    public void forgetPass()
    {
        if(string.IsNullOrEmpty(forgetPassEmail.text))
        {
            showNotificationMessage("Error" , "Forget Email Empty");
            return;
        }
    }

    private void showNotificationMessage(string title,string message)
    {
        notif_Title_Text.text = "" +  title;
        notif_Message_Text.text = "" + message;

        notificationPanel.SetActive(true);
    }

    public void CloseNotif_Panel()
    {
        notif_Title_Text.text= "";
        notif_Message_Text.text= "";

        notificationPanel.SetActive(false);
    }

    public void LogOut()
    {
        
        profileUserEmail_Text.text = "" ;
        profileUserName_Text.text = "" ;
        OpenLoginPanel();
    }
}
