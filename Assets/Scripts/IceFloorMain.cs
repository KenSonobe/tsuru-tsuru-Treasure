using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;
using WebSocketSharp.Net;
using UnityEngine.SceneManagement;

public class IceFloorMain : MonoBehaviour {
	public GameObject camSet, CamShell, CamCube, MainCam, EnemySet, EnemyShell, EnemyCube, EnemyCam, Vm, Ve, StartCam, oni, nige;
	public GameObject[] pr = new GameObject[20];
    public static float moveX = 0, moveZ = 0, moveX1 = 0, moveZ1 = 0;
    float camRot;
    public static bool canMove0, canMove1;
    bool prj1 = true, prj2 = false, camj;

    // プレイヤー選択
    bool pj;
    public static bool player;

    // ソケット
    WebSocket ws;
    bool p0m, p1m, l0, r0, l1, r1;

    // 制限時間
    float time = 100, count = 6;
    public GameObject TIME, sCount, eCount;
    bool st;

	// Use this for initialization
	void Start () {
        CamShell.transform.Rotate(0, 45, 0);
        EnemyShell.transform.Rotate(0, -135, 0);

        player = false;

        // ソケット
        // 家thunderbolt
        //ws = new WebSocket("ws://192.168.10.3:3000");
        // 家wifi
        //ws = new WebSocket("ws://192.168.10.4:3000");
        // 製図
        //ws = new WebSocket("ws://172.21.36.69:3000");
        // 工場2ex
        //ws = new WebSocket("ws://172.28.0.192:3000");
        // 工場2pro
        //ws = new WebSocket("ws://172.28.0.116:3000");

        // startシーンから
        ws = new WebSocket("ws://" + GetAdress.ip + ":3000");

        ws.OnOpen += (sender, e) =>
        {
            Debug.Log("WebSocket Open");
        };

        ws.OnMessage += (sender, e) =>
        {
            Debug.Log(" Data: " + e.Data);

            if(e.Data == "P"){
                camSet.transform.position = new Vector3(-45, 0, -45);
                EnemySet.transform.position = new Vector3(45, 0, 45);
                if (pj == false)
                {
                    player = true;
                    pj = true;
                }
            }

            if (e.Data == "A0")
            {
                moveX = -2f;
            }
            if (e.Data == "B0")
            {
                moveZ = -2f;
            }
            if (e.Data == "C0")
            {
                moveX = 2f;
            }
            if (e.Data == "D0")
            {
                moveZ = 2f;
            }

            if (e.Data == "A1")
            {
                moveX1 = -2f;
            }
            if (e.Data == "B1")
            {
                moveZ1 = -2f;
            }
            if (e.Data == "C1")
            {
                moveX1 = 2f;
            }
            if (e.Data == "D1")
            {
                moveZ1 = 2f;
            }

            // 回転
            if (e.Data == "L0")
            {
                l0 = true;
            }
            if (e.Data == "R0")
            {
                r0 = true;
            }
            if (e.Data == "L1")
            {
                l1 = true;
            }
            if (e.Data == "R1")
            {
                r1 = true;
            }

            if (e.Data == "M0"){
                p0m = true;
            }
            if (e.Data == "M1"){
                p1m = true;
            }
        };

        ws.OnError += (sender, e) =>
        {
            Debug.Log("WebSocket could NOT be Opend");

            SceneManager.LoadScene("start");
        };

        ws.Connect();
	}

    public void OnClick(){
        if (pj == false)
        {
            ws.Send("P");
            pj = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pj == true && player == true && camj == false){
            MainCam.SetActive(true);
            EnemyCam.SetActive(false);
            StartCam.SetActive(false);
            camj = true;
            Ve.SetActive(true);
            oni.SetActive(true);
            TIME.SetActive(true);
            GetComponent<Text>().text = ((int)time).ToString();
        }else if (pj == true && player == false && camj == false){
            MainCam.SetActive(false);
            EnemyCam.SetActive(true);
            StartCam.SetActive(false);
            camj = true;
            Vm.SetActive(true);
            nige.SetActive(true);
            TIME.SetActive(true);
            GetComponent<Text>().text = ((int)time).ToString();
        }

        // カメラの向き
        if (player == true)
        { 
            if (Input.GetKey(KeyCode.UpArrow) && (Camera.main.transform.localEulerAngles.x > 335 || MainCam.transform.localEulerAngles.x < 90))
            {
                MainCam.transform.Rotate(-4, 0, 0);
            }
            if (Input.GetKey(KeyCode.DownArrow) && (Camera.main.transform.localEulerAngles.x > 325 || MainCam.transform.localEulerAngles.x < 80))
            {
                MainCam.transform.Rotate(4, 0, 0);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                ws.Send("L0");
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                ws.Send("R0");
            }
        }

        // 敵の向き
        if (player == false)
        {
            if (Input.GetKey(KeyCode.UpArrow) && (EnemyCam.transform.localEulerAngles.x > 335 || EnemyCam.transform.localEulerAngles.x < 90))
            {
                EnemyCam.transform.Rotate(-4, 0, 0);
            }
            if (Input.GetKey(KeyCode.DownArrow) && (EnemyCam.transform.localEulerAngles.x > 325 || EnemyCam.transform.localEulerAngles.x < 80))
            {
                EnemyCam.transform.Rotate(4, 0, 0);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                ws.Send("L1");
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                ws.Send("R1");
            }
        }

        if (l0 == true)
        {
            CamShell.transform.Rotate(0, -4, 0);
            l0 = false;
        }
        if (r0 == true)
        {
            CamShell.transform.Rotate(0, 4, 0);
            r0 = false;
        }

        if (l1 == true)
        {
            EnemyShell.transform.Rotate(0, -4, 0);
            l1 = false;
        }
        if (r1 == true)
        {
            EnemyShell.transform.Rotate(0, 4, 0);
            r1 = false;
        }

        // 移動
        // Player0
		camSet.transform.position += new Vector3 (moveX, 0, moveZ);
        // Player1
        EnemySet.transform.position += new Vector3(moveX1, 0, moveZ1);

        if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Z)) && canMove0 == true && player == true){
            ws.Send("M0");
        }
        if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Z)) && canMove1 == true && player == false)
        {
            ws.Send("M1");
        }

        // Player0

        if (player == true && p0m == true) {
			camRot = CamShell.transform.localEulerAngles.y;
			if (camRot > 225 && camRot < 315)
            {
                ws.Send("A0");
			}
            else if (camRot > 135 && camRot < 225)
            {
                ws.Send("B0");
			}
            else if (camRot > 45 && camRot < 135)
            {
                ws.Send("C0");
			}
            else if (camRot > 315 || camRot < 45)
            {
                ws.Send("D0");
			}

			canMove0 = false;
		}

        // Player1
        if (player == false && p1m == true)
        {
            camRot = EnemyShell.transform.localEulerAngles.y;
            if (camRot > 225 && camRot < 315)
            {
                ws.Send("A1");
            }
            else if (camRot > 135 && camRot < 225)
            {
                ws.Send("B1");
            }
            else if (camRot > 45 && camRot < 135)
            {
                ws.Send("C1");
            }
            else if (camRot > 315 || camRot < 45)
            {
                ws.Send("D1");
            }

            canMove1 = false;
        }

        if (p0m == true)
        {
            p0m = false;
        }
        if (p1m == true)
        {
            p1m = false;
        }


        // Poison Move
		for (int i = 0; i < 10; i++) {
			if (prj1 == true) {
				pr [i].transform.position += new Vector3(0, 0.05f, 0);
			} else if (prj1 == false) {
				pr [i].transform.position -= new Vector3(0, 0.05f, 0);
			}
		}
		for (int i = 10; i < 20; i++) {
			if (prj2 == true) {
				pr [i].transform.position += new Vector3(0, 0.05f, 0);
			} else if (prj2 == false) {
				pr [i].transform.position -= new Vector3(0, 0.05f, 0);
			}
		}
		if (pr [0].transform.position.y >= 1.3f) {
			prj1 = false;
			prj2 = true;
		} else if (pr [0].transform.position.y <= -0.2f) {
			prj1 = true;
			prj2 = false;
		}

        // TIME
        if (pj == true && count >= 1.1f)
        {
            sCount.SetActive(true);
            count -= Time.deltaTime;
            sCount.GetComponent<Text>().text = ((int)count).ToString();
        }else if (pj == true)
        {
            st = true;
            sCount.SetActive(false);
            canMove0 = true;
            canMove1 = true;
        }

        if (st == true)
        {
            time -= Time.deltaTime;
        }
        if (time < 1) {
            if(player == true){
                SceneManager.LoadScene("lose");
            }else{
                SceneManager.LoadScene("win");
            }
        }

        if (time >= 11 && st == true)
        {
            TIME.SetActive(true);
            TIME.GetComponent<Text>().text = ((int)time).ToString();
        }else if (st == true && time >= 1)
        {
            TIME.SetActive(false);
            eCount.SetActive(true);
            eCount.GetComponent<Text>().text = ((int)time).ToString();
        }
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Enemy")
        {
            if (player == true)
            {
                SceneManager.LoadScene("win");
            }
            else if (player == false)
            {
                SceneManager.LoadScene("lose");
            }
            
        }
	}
}
