using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using Newtonsoft.Json;
using UnityEngine;

public class LoadDataManager : MonoBehaviour
{
    public static FirebaseUser firebaseUser;
    public static User userInGame;

    private DatabaseReference reference;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        FirebaseApp app = FirebaseApp.DefaultInstance;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        firebaseUser = FirebaseAuth.DefaultInstance.CurrentUser;
        GetUserInGame();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetUserInGame()
    {
        reference.Child("Users").Child(firebaseUser.UserId).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                userInGame = JsonConvert.DeserializeObject<User>(snapshot.Value.ToString());
            }
            else
            {

            }
        });
    }
}
