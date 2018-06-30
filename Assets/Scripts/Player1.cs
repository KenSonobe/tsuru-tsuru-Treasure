using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player1 : MonoBehaviour {

    float posX, posZ;
    int pXi, pZi;

    public GameObject Set;

    public AudioSource don;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Stone")
        {
            // Player0
            posX = Set.transform.position.x / 10;
            posZ = Set.transform.position.z / 10;
            pXi = (int)posX;
            pZi = (int)posZ;
            if (posX > 0)
            {
                posX = (float)pXi * 10 + 5;
            }
            else if (posX < 0)
            {
                posX = (float)pXi * 10 - 5;
            }
            if (posZ > 0)
            {
                posZ = (float)pZi * 10 + 5;
            }
            else if (posZ < 0)
            {
                posZ = (float)pZi * 10 - 5;
            }
            Set.transform.position = new Vector3(posX, 0, posZ);
            IceFloorMain.moveX1 = 0;
            IceFloorMain.moveZ1 = 0;

            IceFloorMain.canMove1 = true;

            if (IceFloorMain.player == false)
            {
                don.PlayOneShot(don.clip);
            }
        }

        if (other.gameObject.tag == "Poison")
        {
            if (IceFloorMain.player == true)
            {
                SceneManager.LoadScene("win");
            }
            else if (IceFloorMain.player == false)
            {
                SceneManager.LoadScene("lose");
            }
        }
    }
}
