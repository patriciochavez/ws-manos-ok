using UnityEngine;
using System.Collections.Generic;
using System;


public class LoginOpenStack : MonoBehaviour
{
    public String token;
    public String tenant;

    WWW www_token; 
    public string url_auth = "https://10.105.231.23:3000/tokens";
    RESTFactory restFactory;

    [Serializable]
    public class Token
    {
        public String tokenid;
        public String tenantid;
    }

    // Use this for initialization
    void Start()
    {
        //crear instancia
        //pido token
        restFactory = GetComponent<RESTFactory>();
        Dictionary<string, string> usuario = new Dictionary<string, string>();
        usuario.Add("username", "admin");
        usuario.Add("password", "nomoresecrete");
        www_token = restFactory.LOGIN(url_auth, usuario, Autenticacion);
        usuario.Clear();
    }

    
    void Autenticacion()
    {
        token = JsonUtility.FromJson<Token>(www_token.text).tokenid;
        tenant = JsonUtility.FromJson<Token>(www_token.text).tenantid;
    }
}
