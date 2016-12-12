using UnityEngine;
using System.Collections.Generic;
using System;


public class CrearInstancia : MonoBehaviour {
    public bool colisionDetectada = false;
    private String token;
    private String tenant;
    WWW www_server;
    public String sabor;
    Dictionary<string, string> post_datos = new Dictionary<string, string>();

    public string url_srv = "https://10.105.231.23:3000/servers";
    RESTFactory restFactory;
    GameObject Login;

    [Serializable]
    public class Token
    {
        public String tokenid;
        public String tenantid;
    }

    // Use this for initialization
    void Start () {
        
      restFactory = GetComponent<RESTFactory>();
      Login = GameObject.Find("LoginOpenStack");
    }
        
    void OnCollisionEnter(Collision col)
    {
        token = Login.GetComponent<LoginOpenStack>().token;
        tenant = Login.GetComponent<LoginOpenStack>().tenant;
        if (!colisionDetectada && token != "") {            
            Debug.Log("Colisión detectada: " + gameObject.transform.name);
                        
            colisionDetectada = !colisionDetectada;

            post_datos.Clear();
            post_datos.Add("tokenid", token);
            post_datos.Add("tenantid", tenant);
            post_datos.Add("combo", sabor);
            www_server = restFactory.SERVERS_POST(url_srv, post_datos, Respuesta);
            
            gameObject.SetActive(false);

          
        }
    }

    void Respuesta() {
       
    }
}
