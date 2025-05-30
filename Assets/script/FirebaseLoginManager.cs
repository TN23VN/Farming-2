using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FirebaseLoginManager : MonoBehaviour
{
    [Header("Dang ky")]
    public InputField ipRegisterEmail;
    public InputField ipRegisterPassword;
    public Button buttonRegister;

    [Header("Dang nhap")]
    public InputField ipLoginEmail;
    public InputField ipLoginPassword;
    public Button buttonLogin;

    [Header("Switch form")]
    public Button buttonMoveToLogin;
    public Button buttonMoveToRegister;
    public GameObject loginForm;
    public GameObject registerForm;

    //Firebase Authentication -> đăng ký ,đăng nhập
    private FirebaseAuth auth;
    private void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        buttonRegister.onClick.AddListener(RegisterAccountWithFirebase);
        buttonLogin.onClick.AddListener(SignInAccountWithFirebase);
        buttonMoveToLogin.onClick.AddListener(SwitchForm);
        buttonMoveToRegister.onClick.AddListener(SwitchForm);
    }
    public void RegisterAccountWithFirebase()
    {
        string email = ipRegisterEmail.text;
        string pass = ipRegisterPassword.text;
        auth.CreateUserWithEmailAndPasswordAsync(email, pass).ContinueWithOnMainThread(task=>
        {
            if (task.IsCanceled)
            {
                Debug.Log("Huy dang ky");
                return;
            }
            else if (task.IsFaulted) 
            {
                Debug.Log("Dang ky that bai");
                return;
            }
            else if (task.IsCompleted)
            {
                Debug.Log("Dang ky thanh cong");
                return;
            }
        });

    }

    public void SignInAccountWithFirebase()
    {
        string email = ipLoginEmail.text;
        string pass = ipLoginPassword.text;
        auth.SignInWithEmailAndPasswordAsync(email, pass).ContinueWithOnMainThread(task => 
        {
            if (task.IsCanceled)
            {
                Debug.Log("Huy dang nhap");
                return;
            }
            else if (task.IsFaulted)
            {
                Debug.Log("Dang nhap that bai");
                
            }
            else if (task.IsCompleted)
            {
                Debug.Log("Dang nhap thanh cong");
                FirebaseUser user = task.Result.User;
                SceneManager.LoadScene("SampleScene");
            }
        });
    }

    public void SwitchForm()
    {
        loginForm.SetActive(!loginForm.activeSelf);
        registerForm.SetActive(!registerForm.activeSelf);
    }
}
