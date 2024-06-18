using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Analytics;



public class AuthManager : MonoBehaviour
{
    [SerializeField] TMP_InputField emailInputSignUp;
    [SerializeField] TMP_InputField passwordInputSignUp;

    [SerializeField] TMP_InputField emailInputLogIn;
    [SerializeField] TMP_InputField passwordInputLogIn;

    public static FirebaseAuth auth;
    public static DatabaseReference MazeGameRef;

    string userID;


    bool authFlag = false;

    void Start()
    {
        StartCoroutine(Initilization());
    }

    void Update()
    {

    }

    private IEnumerator Initilization()
    {
        var task = FirebaseApp.CheckAndFixDependenciesAsync();

        while (!task.IsCompleted)
        {
            yield return null;
        }
        
        if(task.IsCanceled || task.IsFaulted)
        {
            Debug.LogError("Authentication Error: "+ task.Exception);
        }
        else if(task.IsCompleted)
        {
            var dependencyStatus = task.Result;

        // Firebase Initializations Start
        if (dependencyStatus == DependencyStatus.Available)
        {
            // authentication initialization
            auth = FirebaseAuth.DefaultInstance;
            MazeGameRef = FirebaseDatabase.DefaultInstance.GetReference("CustomID/com_google_firebase_Maze"); 


            // analytics initialization
            AnalyticsManager.app = Firebase.FirebaseApp.DefaultInstance;
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);

            AnalyticsManager.MazeGameRef = FirebaseDatabase.DefaultInstance.GetReference("CustomID/com_google_firebase_Maze"); 
            AnalyticsManager.auth = FirebaseAuth.DefaultInstance;


            Debug.Log("Initialization is okey"+
                "\nMazeGameRef: "+ MazeGameRef +
                "\nauth: "+ auth); 
        }
        }
    }

    public void UserRegister()
    {
        Debug.Log("****USER REGISTER****");
        string email = emailInputSignUp.text;
        string password = passwordInputSignUp.text;
        
        bool registerFlag = false;
        bool authenticationFlag = false;

        while(!registerFlag)
        {
            if(auth!=null && MazeGameRef!=null)
            {
                if(!authenticationFlag)
                {
                    Authentication_Register(email,password);
                    authenticationFlag = true;
                }
                if((auth.CurrentUser != null) && (auth.CurrentUser.Email == email) )
                {
                    userID = auth.CurrentUser.UserId;

                    Database_Register(userID, email, password);

                    AnalyticsManager.Analytics_SessionStart();

                    registerFlag = true;
                }
            }
        }
        Debug.Log("current user after registeration: "+auth.CurrentUser
        +"\nemail: "+ auth.CurrentUser.Email 
        + "\nuserID: "+ auth.CurrentUser.UserId );

    }

    private void Authentication_Register(string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(RegisterTask =>
        {
            if(RegisterTask.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled");
                return;
            }
            if(RegisterTask.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: "+ RegisterTask.Exception);
                return;
            }

            
            Firebase.Auth.AuthResult authResult = RegisterTask.Result;
            Firebase.Auth.FirebaseUser newUser = authResult.User;
            Debug.LogFormat("Firebase user created succesfully : {0} ({1})", newUser.UserId, newUser.Email);  
        });
    }
    private void Database_Register(string ID, string email, string password)
    {
        string SignInDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        MazeGameRef.Child("Users").Child(ID).Child("Sign In Date").SetValueAsync(SignInDate);
        MazeGameRef.Child("Users").Child(ID).Child("Email").SetValueAsync(email);
        MazeGameRef.Child("Users").Child(ID).Child("Password").SetValueAsync(password);
        Debug.Log("user "+ auth.CurrentUser.Email+ " registered in database.");
    }




    public void UserLogIn()
    {
        Debug.Log("****USER LOGIN****");
        string email= emailInputLogIn.text;
        string password = passwordInputLogIn.text;

        bool LogInFlag = false;
        bool authenticationFlag = false;

        while(!LogInFlag)
        {
            if(auth!=null && MazeGameRef!=null)
            {
                if(!authenticationFlag)
                {
                    Authentication_LogIn(email,password);
                    authenticationFlag = true;
                }
                if((auth.CurrentUser != null) && (auth.CurrentUser.Email == email) )
                {
                    userID = auth.CurrentUser.UserId;

                    AnalyticsManager.Analytics_SessionStart();
                    //Database_LogIn(userID);

                    LogInFlag = true;
                }
            }
        }
        Debug.Log("current user after logging in: "+auth.CurrentUser
        +"\nemail: "+ auth.CurrentUser.Email 
        + "\nuserID: "+ auth.CurrentUser.UserId );

    }

    private void Authentication_LogIn(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(RegisterTask =>
        {
            if(RegisterTask.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled");
                return;
            }
            if(RegisterTask.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: "+ RegisterTask.Exception);
                return;
            }

            
            Firebase.Auth.AuthResult authResult = RegisterTask.Result;
            Firebase.Auth.FirebaseUser newUser = authResult.User;
            Debug.LogFormat("Firebase user logged in succesfully : {0} ({1})", newUser.UserId, newUser.Email);   
        });
    }


    public void deneme()
    {
        MazeGameRef.Child("game").GetValueAsync().ContinueWith(task =>
        {
            if(task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("Database Error: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                Debug.Log("SNAPSHOT: " + snapshot);
                string value = snapshot.Value.ToString();

                Debug.Log("game: " + value);

                MazeGameRef.Child("game").SetValueAsync("updated");
                MazeGameRef.Child("data").SetValueAsync("deneme");
            }
        });
    }



    

}



/*
    private void Database_LogIn(string ID)
    {
        string LogInDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

        SessionKey = MazeGameRef.Child("Actions").Child(ID).Push().Key;
        MazeGameRef.Child("Actions").Child(ID).Child(SessionKey).Child("Log In Date").SetValueAsync(LogInDate);
    }

*/