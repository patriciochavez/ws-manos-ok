using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class EchoTest : MonoBehaviour {
    public Text respuesta;
    public GameObject instancia1;
    public GameObject instancia2;
    public GameObject instancia3;

    IEnumerator Start () {
		WebSocket w = new WebSocket(new Uri("wss://10.105.231.23:8443"));
        respuesta.text = "Esperando conexión...";
    
        yield return StartCoroutine(w.Connect());
		//w.SendString("Hi there");
		//int i=0;
		while (true)
		{
			string reply = w.RecvString();
			if (reply != null)
			{
				Debug.Log ("Recibido: "+reply);
			//	w.SendString("Respuesta " + i++);
                respuesta.text = reply;
                if (respuesta.text == "Reiniciar_Credito") {
                    instancia1.SetActive(true);
                    instancia1.GetComponent<CrearInstancia>().colisionDetectada = false;
                    instancia2.SetActive(true);
                    instancia2.GetComponent<CrearInstancia>().colisionDetectada = false;
                    instancia3.SetActive(true);
                    instancia3.GetComponent<CrearInstancia>().colisionDetectada = false;
                }

            }
			if (w.error != null)
			{
				Debug.LogError ("Error: "+w.error);
				break;
			}
			yield return 0;
		}
		w.Close();
	}

    
}
