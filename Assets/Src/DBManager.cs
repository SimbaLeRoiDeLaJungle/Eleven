using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

public static class DBManager
{
    static string addCardUrl = "http://localhost/Eleven/AddCard.php?";
    static string connectUserUrl = "http://localhost/Eleven/ConnectUser.php?";
    static string addUserUrl = "http://localhost/Eleven/AddUser.php?";
    static string secretKey = "Yoooolo";

    static public IEnumerator ConnectUser(string userName, string password)
    {
        string hash = HashInput(secretKey);
        string post_url = connectUserUrl + "username=" + 
            UnityWebRequest.EscapeURL(userName) + "&password=" +
            UnityWebRequest.EscapeURL(password) +
            "&hash=" + hash;
        Debug.Log(post_url);
        UnityWebRequest hs_post = UnityWebRequest.Post(post_url, hash);
        yield return hs_post.SendWebRequest();
        if (hs_post.error != null)
            Debug.Log("There was an error in  DBManager.ConnectUser : " 
                    + hs_post.error);
        Debug.Log(hs_post.downloadHandler.text);
    }
    static public IEnumerator AddCardToDB(CardScriptable cardScript)
    {
        int card_id = cardScript.number;
        string hash = HashInput(card_id.ToString() + secretKey);
        string post_url = addCardUrl + "card_id=" + 
            UnityWebRequest.EscapeURL(card_id.ToString()) + "&hash=" + hash;

        UnityWebRequest hs_post = UnityWebRequest.Post(post_url, hash);
        yield return hs_post.SendWebRequest();
        if (hs_post.error != null)
            Debug.Log("There was an error in DBManager.AddCardToBD : " 
                    + hs_post.error);
    }
    static public IEnumerator AddUserToDB(string userName, string email, string password)
    {
        string hash = HashInput(secretKey);
        string post_url = addUserUrl + "username=" + 
            UnityWebRequest.EscapeURL(userName) + "&email=" +
            UnityWebRequest.EscapeURL(email) + "&password=" + 
            UnityWebRequest.EscapeURL(password) + "&hash=" + hash;
        Debug.Log(post_url);
        UnityWebRequest hs_post = UnityWebRequest.Post(post_url, hash);
        yield return hs_post.SendWebRequest();
        if (hs_post.error != null)
            Debug.Log("There was an error in DBManager.AddCardToBD : " 
                    + hs_post.error);
        Debug.Log(hs_post.downloadHandler.text);
    }


    static public string HashInput(string input)
    {
        SHA256Managed hm = new SHA256Managed();
        byte[] hashValue = 	
                hm.ComputeHash(System.Text.Encoding.ASCII.GetBytes(input));
        string hash_convert = 
                BitConverter.ToString(hashValue).Replace("-", "").ToLower();
        return hash_convert;
    }
}
