using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GetAdress : MonoBehaviour {

    public InputField ipBox;

    public static string ip;

	// Use this for initialization
	void Start () {

        InitInputField();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void InputLogger () {

        ip = ipBox.text;
        Debug.Log("IP Adress : " + ip);

        SceneManager.LoadScene("IceFloorStage1.3");

    }

    void InitInputField () {

        ip = "";
        ipBox.ActivateInputField();

    }
}
