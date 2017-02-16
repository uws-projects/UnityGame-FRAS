using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapaGalbena : MonoBehaviour {

    public GameObject FPS;      // should be draggeed in inspector and point to the FPS Controller
    public GameObject Car;      // should be draggeed in inspector and point to the car

    // Use this for initialization
    void Start () {
        switch (PlayerPrefs.GetInt("GameMode"))
        {
            case 0:
                Car.SetActive(true);
                FPS.SetActive(false);
                break;
            case 1:
                Car.SetActive(false);
                FPS.SetActive(true);
                break;
            default: Debug.Log("Should not arrive here, error when switching PlayerPrefs \"GameMode\"");
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.UnloadLevel(1);
            Application.LoadLevel(0);
        }
	}
}
