using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseDatabaseManager : MonoBehaviour
{
    private DatabaseReference reference;

    private void Awake()
    {
        FirebaseApp app = FirebaseApp.DefaultInstance;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    private void Start()
    {
        
    }
    public void WriteDatabase(string id, string msg)
    {
        reference.Child("Users").Child(id).SetValueAsync(msg).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {

            }
            else
            {

            }
        });
    }

    public void ReadDatabase(string id)
    {
        reference.Child("Users").Child(id).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

            }
            else
            {

            }
        });
    }
}
