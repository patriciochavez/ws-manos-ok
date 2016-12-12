using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class RESTFactory : MonoBehaviour
{
    private string results;
    Dictionary<string, string> headers = new Dictionary<string, string>();
        
    public String Results
    {
        get
        {
            return results;
        }
    }

    [Serializable]
    public class Login
    {
        public string username;
        public string password;        
    }

    [Serializable]
    public class Servidor
    {
        public string tokenid;
        public string tenantid;
        public string combo;
    }
    
    public WWW GET(string url, System.Action onComplete)
    {
        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(www, onComplete));
        return www;
    }

    internal string GET(string v1, object v2)
    {
        throw new NotImplementedException();
    }

    public WWW SERVERS_POST(string url, Dictionary<string, string> post, System.Action onComplete)
    {
        headers.Clear();
        headers.Add("Content-Type", "application/json");
        //WWWForm form = new WWWForm();
                
        Servidor servidor = new Servidor();
        
        servidor.tokenid = post["tokenid"];
        servidor.tenantid = post["tenantid"];
        servidor.combo = post["combo"];
        
        string postData = JsonUtility.ToJson(servidor);
        byte[] pData = System.Text.Encoding.ASCII.GetBytes(postData.ToCharArray());
        WWW www = new WWW(url, pData, headers);

        StartCoroutine(WaitForRequest(www, onComplete));

        post.Clear();
        return www;
    }

    public WWW LOGIN(string url, Dictionary<string, string> post, System.Action onComplete)
    {
        headers.Clear();
        headers.Add("Content-Type", "application/json");
        //WWWForm form = new WWWForm();

        Login login = new Login();

        login.username = post["username"];
        login.password = post["password"];
        //foreach (KeyValuePair<string, string> post_arg in post)
        //{           
        //form.AddField(post_arg.Key, post_arg.Value);            
        //}

        //Debug.Log(JsonUtility.FromJson<Sube>(www_sube.text));
        string postData = JsonUtility.ToJson(login);
        byte[] pData = System.Text.Encoding.ASCII.GetBytes(postData.ToCharArray());
        WWW www = new WWW(url, pData, headers);

        StartCoroutine(WaitForRequest(www, onComplete));
        post.Clear();
        return www;
    }

    private IEnumerator WaitForRequest(WWW www, System.Action onComplete)
    {        
        yield return www;
        
        // check for errors
        if (www.error == null)
        {
            results = www.text;
            onComplete();
        }
        else
        {
            Debug.Log(www.error);
        }
    }


}
