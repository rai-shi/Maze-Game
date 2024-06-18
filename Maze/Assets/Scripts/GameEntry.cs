using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntry : MonoBehaviour
{

    [SerializeField] private GameObject SignUpPanel;
    [SerializeField] private GameObject LogInPanel;
    [SerializeField] private GameObject EntryPanel;

    void Start()
    {
        EntryPanel.gameObject.SetActive(true);
        SignUpPanel.gameObject.SetActive(false);
        LogInPanel.gameObject.SetActive(false);
        
    }
    void Update()
    {
        
    }

        public void LogIn()
    {
        EntryPanel.gameObject.SetActive(false);
        SignUpPanel.gameObject.SetActive(false);
        LogInPanel.gameObject.SetActive(true);

    }

    public void SignUp()
    {
        EntryPanel.gameObject.SetActive(false);
        SignUpPanel.gameObject.SetActive(true);
        LogInPanel.gameObject.SetActive(false);
    }


}
