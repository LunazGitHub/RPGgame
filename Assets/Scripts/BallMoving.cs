using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BallMoving : MonoBehaviour
{

    public int BallSpeed;

    private Rigidbody rigi;


	// Use this for initialization
	void Start ()
    {

        rigi = GetComponent<Rigidbody>();
		
	}
	
	// Update is called once per frame
	void Update ()
    {

        float MoveHorizontal = Input.GetAxis("Horizontal");
        float MoveVertical = Input.GetAxis("Vertical");
        ExitToMenu();
        Vector3 movement = new Vector3(MoveVertical, 0.0f, MoveHorizontal);

        rigi.AddForce(movement * BallSpeed);
	}

    void ExitToMenu()
    {

        if(Input.GetKey(KeyCode.C))
        {

            SceneManager.LoadScene("BallMenu", LoadSceneMode.Single);
            
        }
    }
}
