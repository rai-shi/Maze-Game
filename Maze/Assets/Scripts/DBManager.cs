using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using UnityEngine.UI;
using TMPro;
public class DBManager : MonoBehaviour
{
void Start()
{

}



void Update()
{}


}


/*
public DatabaseReference usersRef;
    public DatabaseReference coinsRef;
    public TMP_InputField usernameInput, passwordInput;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Initilization());
    }

    private IEnumerator Initilization()
    {
        // database'e erişim için bir task oluştur
        var task = FirebaseApp.CheckAndFixDependenciesAsync();
        while (!task.IsCompleted)
        {
            yield return null;
        }
        if(task.IsCanceled|| task.IsFaulted)
        {
            Debug.LogError("Database Error: "+ task.Exception);
        }
        var dependencyStatus = task.Result;

        // avaliable ise erişim sağlanmıştır
        if (dependencyStatus == DependencyStatus.Available)
        {
            usersRef = FirebaseDatabase.DefaultInstance.GetReference("Users"); // users tablosunu tut
            coinsRef = FirebaseDatabase.DefaultInstance.GetReference("CoinData");
            Debug.Log("Initialization completed");

        }
        else{
            Debug.LogError("Database Error: ");
        }
    }

    public void SaveUser()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        Dictionary<string, object> user = new Dictionary<string, object>();
        user["username"] = username;
        user["password"] = password;

        string key = usersRef.Push().Key; // unique key oluştur
        usersRef.Child(key).UpdateChildrenAsync(user); // o key içerisine verileri ekle
    }


    public void getData()
    {
        StartCoroutine(getUserData());
    }
    public IEnumerator getUserData()
    {
        string name = usernameInput.text;
        print(name);
        var task = usersRef.GetValueAsync();
        print(task.IsCompleted);
        while(!task.IsCompleted)
        {
            yield return null;
        }
        print(task.IsCompleted);
        if(task.IsCanceled|| task.IsFaulted)
        {
            print("cancel: "+task.IsCanceled);
            print("fault: "+task.IsFaulted);
            Debug.LogError("database error: "+ task.Exception);
            yield break;
        }
        DataSnapshot snapshot = task.Result;

        foreach (DataSnapshot userSnapshot in snapshot.Children)
        {
            string username = userSnapshot.Child("username").Value.ToString();
            string password = userSnapshot.Child("password").Value.ToString();

            Debug.Log("Username: " + username + ", Password: " + password);
        }


    }


        var task = usersRef.Child(name).GetValueAsync();


        DataSnapshot snapshot = task.Result;
        print("snapshot: "+ snapshot);

        foreach (DataSnapshot user in snapshot.Children)
        {
            print("burada");
            if (user.Key =="username")
            {
                print("username: "+ user.Value.ToString());
            }
        }
   


    void Update()
    {
       
        
    }
*/
