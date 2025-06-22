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
    public InputField ipRegisterPassword2;
    public Button buttonRegister;
    public Text DK_Notify;

    [Header("Dang nhap")]
    public InputField ipLoginEmail;
    public InputField ipLoginPassword;
    public Button buttonLogin;
    public Text DN_Notify;

    [Header("Switch form")]
    public Button buttonMoveToLogin;
    public Button buttonMoveToRegister;
    public GameObject loginForm;
    public GameObject registerForm;

    private FirebaseDatabaseManager databaseManager;
    private LoadDataManager loadDataManager;
    //Firebase Authentication -> đăng ký ,đăng nhập
    private FirebaseAuth auth;
    private void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        buttonRegister.onClick.AddListener(RegisterAccountWithFirebase);
        buttonLogin.onClick.AddListener(SignInAccountWithFirebase);
        buttonMoveToLogin.onClick.AddListener(SwitchForm);
        buttonMoveToRegister.onClick.AddListener(SwitchForm);

        databaseManager = GetComponent<FirebaseDatabaseManager>();
    }
    public void RegisterAccountWithFirebase()
    {
        string email = ipRegisterEmail.text;
        string pass = ipRegisterPassword.text;
        string pass2 = ipRegisterPassword2.text;
        if(pass == pass2)
        {
            auth.CreateUserWithEmailAndPasswordAsync(email, pass).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    DK_Notify.text = "Huy dang ky";
                    return;
                }
                else if (task.IsFaulted)
                {
                    DK_Notify.text = "Dang ky that bai";
                    return;
                }
                else if (task.IsCompleted)
                {
                    DK_Notify.text = "Dang ky thanh cong";
                    Map mapInGame = new Map();
                    User userInGame = new User("", 100, 50, mapInGame);
                    FirebaseUser firebaseUser = task.Result.User;
                    databaseManager.WriteDatabase("Users/" + firebaseUser.UserId, userInGame.ToString());

                    LoadingManager.next_scene = "SampleScene";
                    SceneManager.LoadScene("LoadingScene");
                }
            });
        }
        else
        {
            DK_Notify.text = "Mat khau khong khop";
        }

    }

    public void SignInAccountWithFirebase()
    {
        string email = ipLoginEmail.text;
        string pass = ipLoginPassword.text;
        auth.SignInWithEmailAndPasswordAsync(email, pass).ContinueWithOnMainThread(task => 
        {
            if (task.IsCanceled)
            {
                DN_Notify.text = "Huy dang nhap";
                return;
            }
            else if (task.IsFaulted)
            {
                DN_Notify.text = "Dang nhap that bai";
                
            }
            else if (task.IsCompleted)
            {
                DN_Notify.text = "Dang nhap thanh cong";
                FirebaseUser user = task.Result.User;

                LoadingManager.next_scene = "SampleScene";
                SceneManager.LoadScene("LoadingScene");
            }
        });
    }

    public void SwitchForm()
    {
        loginForm.SetActive(!loginForm.activeSelf);
        registerForm.SetActive(!registerForm.activeSelf);
    }
}
