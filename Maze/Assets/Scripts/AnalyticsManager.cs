using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;


using Firebase;
using Firebase.Analytics;
using Firebase.Database;
using Firebase.Auth;

public class AnalyticsManager : MonoBehaviour
{
    // Analytics instances 
    public static FirebaseApp app;

    // Database references
    public static DatabaseReference MazeGameRef;

    // Firebase instances
    public static FirebaseAuth auth;


    static string SessionKey;
    static string UserID;

    bool AnalyticsFlag = false;

    int coinCount =0;

    void Start()
    { }

    void Update()
    {}



    public static void Analytics_SessionStart()
    {
        // session date is saving

        if((auth.CurrentUser != null) && (MazeGameRef != null) && (app != null) )
        {
            // prepare info

            string start_time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            FirebaseAnalytics.GetSessionIdAsync().ContinueWith(task =>
            {
                if(task.IsCanceled || task.IsFaulted)
                {
                    Debug.Log("GetSessionIdAsync error: " + task.Exception);
                }
                else
                {
                    long ID = task.Result;
                    SessionKey = ID.ToString();
                }
            });

            UserID = auth.CurrentUser.UserId;
            // check user in Users collection

            MazeGameRef.Child("Users").Child(UserID).GetValueAsync().ContinueWith(task =>
            {

                if(task.IsCanceled || task.IsFaulted)
                {
                    Debug.LogError("UsersRef database error: "+ task.Exception);
                }
                else
                {
                    bool isSessionID = false;
                    DataSnapshot snapshot = task.Result;
                    
                    if (snapshot.Value != null)  // deviceID(user) database'de ise 
                    {
                        Debug.Log("UserID: "+ UserID+ "\nSnapshotKey: "+ snapshot.Key.ToString());
                        if(auth.CurrentUser.UserId == snapshot.Key.ToString()) //giriş yapılmış ise
                        {
                            while(!isSessionID)
                            {
                                if (SessionKey != "")
                                {
                                    // uygulamadan çıktıktan sonra sessionın ne zaman kapanacağını belirtiyoruz.
                                    TimeSpan duration = TimeSpan.FromMinutes(1);
                                    FirebaseAnalytics.SetSessionTimeoutDuration(duration);

                                    
                                    FirebaseAnalytics.SetUserId(UserID);

                                    MazeGameRef.Child("Actions").Child(UserID).Child(SessionKey).Child("Session Start At").SetValueAsync(start_time);

                                    FirebaseAnalytics.LogEvent("session_start",SessionKey,start_time);

                                    isSessionID = true;
                                    Debug.Log("User " + UserID+ " start session-"+ SessionKey+ " at "+ start_time );
                                }
                            }
                        }
                        else
                        {
                            Debug.Log("Current User name does not match with deviceID");
                        }
                    }
                    else
                    {
                        Debug.Log("UsersRef does not have user in collection");
                    }
                }
            });
        }
        else
        {
            Debug.Log("Session Start instances is null");
        }
        
        // events is logging

        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventAppOpen);
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLogin);
        //FirebaseAnalytics.SetUserId(SystemInfo.deviceUniqueIdentifier);

    }

    public void CollectCoin()
    {
        coinCount += 1;
    }

    private void Analytics_CoinEvent()
    {
        MazeGameRef.Child("Actions").Child(UserID).Child(SessionKey).Child("Coin Count").SetValueAsync(coinCount);
        Debug.Log("Coin Count: "+ coinCount);
    }


    private void Analytics_SessionFinish() 
    {
        string finish_time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        MazeGameRef.Child("Actions").Child(UserID).Child(SessionKey).Child("Session Finish At").SetValueAsync(finish_time);

        FirebaseAnalytics.LogEvent("session_finish",SessionKey,finish_time);
    }

    private void OnDestroy()
    {

        Analytics_CoinEvent();
  
    }

    void OnApplicationQuit()
    {
        Analytics_SessionFinish();
        Debug.Log("exit");
    }

}


/*
// Check and fix Firebase dependencies.
Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
{
    var dependencyStatus = task.Result;
    if (dependencyStatus == DependencyStatus.Available)
    {
        // Firebase is properly initialized, you can proceed with using Firebase services.

        // Enable analytics data collection.
        FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);

        // Log an event to test if analytics is working.
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventAppOpen);

        //var eventParameter = FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventAppOpen);
        //Debug.Log("Event logged: " + eventParameter.Name);
    }
    else
    {
        Debug.LogErrorFormat("Could not resolve all Firebase dependencies: {0}", dependencyStatus);
        // Handle the error or display a message indicating Firebase initialization failure.
    }
});
*/