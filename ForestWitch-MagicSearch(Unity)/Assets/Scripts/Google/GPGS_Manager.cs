using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GPGS_Manager : MonoBehaviour
{
    public GameObject login;
    public GameObject noneLogin;

    public void GPGS_LogIn()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            noneLogin.SetActive(false);
            login.SetActive(true);
        }
        else
        {

        }
    }


}
