using UnityEngine;
using UnityEngine.UI;

public class UsernameWizard : MonoBehaviour
{
    public GameObject usernameWizard;
    public InputField txtUsername;
    public Button btnOKUsername;
    public Text username;
    public Text gold;
    public Text diamond;

    private FirebaseDatabaseManager databaseManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        databaseManager = GameObject.Find("DatabaseManager").GetComponent<FirebaseDatabaseManager>();

        if (LoadDataManager.userInGame.Name == "")
        {
            usernameWizard.SetActive(true);
        }
        else
        {
            username.text = LoadDataManager.userInGame.Name;
            gold.text ="Gold: "+ LoadDataManager.userInGame.Gold.ToString();
            diamond.text ="Diamond: "+ LoadDataManager.userInGame.Diamond.ToString();
        }
        btnOKUsername.onClick.AddListener(SetNewUsername);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetNewUsername()
    {
        if(txtUsername.text != "")
        {
            LoadDataManager.userInGame.Name = txtUsername.text;
            databaseManager.WriteDatabase("Users/" + LoadDataManager.firebaseUser.UserId, LoadDataManager.userInGame.ToString());
            usernameWizard.SetActive(false);
            username.text = LoadDataManager.userInGame.Name;
        }
    }
}
