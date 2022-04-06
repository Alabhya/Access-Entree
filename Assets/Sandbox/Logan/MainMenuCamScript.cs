using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MainMenuCamScript : MonoBehaviour
{
    public float CameraDistance;
    public float DownAngle;
    public float ZoomStop;
    public float centerX;
    public float centerY;
    public float centerZ;

    private float angleTimer = 0f;
    private float radAngle;
    //private float angleTimer2 = 0f;
    private float yMult;
    private float xzMult;
    private bool startClicked;
    private bool mainMenu;
    private float xPos;
    private float zPos;
    //private float zoomTimer = 0f;
    private Image transition;
    //private bool transIn = true;
    private GameObject mainMenuUI, ingameHUD;
    private GameObject character;
    private GameObject playerCameraObject;
    //private bool canMove = false;
    public static MainUser mainCharacter;
    private Boolean barrierGame;
    Vector3 playerPos; 


    MainUser getUser()
    {
        bool newGame = true; // temp until we can load games
        if(newGame) // should eventually trigger a character creation function
        {
            string username = "we should ask user for their name in a menu";
            mainCharacter = new MainUser(username);
        }
        else
        {
            // need to load character data from database
            string username = "this should be from database?";
            mainCharacter = new MainUser(username);
            mainCharacter.loadCharacterData(username); 
        }
        return mainCharacter; 
    }
    private void Awake() 
    {
        barrierGame = false;
        mainMenu = true; 
        mainMenuUI = GameObject.Find("MainMenu");
        playerCameraObject = GameObject.Find("PlayerCamera");
        character = GameObject.FindGameObjectWithTag("Player");
        ingameHUD = GameObject.Find("InGameHUD");
        playerCameraObject.SetActive(false);
        character.SetActive(false);
        ingameHUD.SetActive(false);
        mainCharacter = getUser(); // gets the info of the user's character
    }

    private void Start()
    {
        float downRadAngle = DownAngle * Mathf.PI / 180;
        yMult = Mathf.Sin(downRadAngle);
        xzMult = Mathf.Cos(downRadAngle);
        GameObject.Find("StartButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            startClicked = true;
            mainMenu = false; 
            //transition.enabled = true;
            this.GetComponent<Camera>().enabled = false;
            this.gameObject.SetActive(false);
            mainMenuUI.SetActive(false);
            ingameHUD.SetActive(true);
            character.SetActive(true);
            playerCameraObject.SetActive(true);
            playerCameraObject.GetComponent<Camera>().enabled = true;
            xPos = Mathf.Sin(radAngle);
            zPos = Mathf.Cos(radAngle);
            startMenuAnimation();
        });
        GameObject.Find("ExitButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            Application.Quit();
        });
        //transition = GameObject.Find("Transition").GetComponent<Image>();
        //transition.enabled = false;
    }

    private void Update()
    {

        if(mainMenu)
        {
            angleTimer += Time.deltaTime;
            radAngle = angleTimer * Mathf.PI / 5;
            transform.rotation = Quaternion.Euler(DownAngle, angleTimer * -36, 0);
            transform.position = new Vector3(Mathf.Sin(radAngle) * xzMult + centerX, yMult + centerY, Mathf.Cos(radAngle) * -xzMult + centerZ) * CameraDistance;
        }
        else if (barrierGame)
        {
            //Debug.Log("y pos: " + transform.position.y);
            playerPos.y += (Time.deltaTime * 10);
            if (transform.rotation.x < 0.65)
            {
                angleTimer += Time.deltaTime * 36;
                transform.rotation = Quaternion.Euler(angleTimer, 0, 0);
                //Debug.Log("x rot: " + transform.rotation.x);
                if (transform.position.y < 50)
                {
                    // radAngle = angleTimer/36 * (Mathf.PI / 5);
                    //Debug.Log("angle: " + Mathf.Cos(radAngle));
                    //transform.position = new Vector3(Mathf.Sin(radAngle) * playerPos.x, playerPos.y, Mathf.Cos(radAngle) * playerPos.z) * 5;
                    transform.position = new Vector3(playerPos.x, playerPos.y, playerPos.z);

                }
            }
        }
        
    }
   
    public void changeToTopView()
    {
        Debug.Log("Camera Invoked!");
        // switch cameras from character oriented to overhead

        this.gameObject.SetActive(true);
        this.GetComponent<Camera>().enabled = true;
        playerPos = playerCameraObject.GetComponent<Transform>().position;
        transform.position = playerPos; // set menu camera to player's camera position initially


        angleTimer = transform.rotation.x;
        playerCameraObject.SetActive(false);
        playerCameraObject.GetComponent<Camera>().enabled = false;
        barrierGame = true;

    }
    private void startMenuAnimation()
    {
        //if (angleTimer2 < 1)
        //{
        //    angleTimer2 += Time.deltaTime;
        //    if (angleTimer2 > 1) { angleTimer2 = 1; }
        //    float smoothAngleTimer = (Mathf.Cos((angleTimer2 + 1) * Mathf.PI) + 1) / 2;
        //    float movingDownAngle = DownAngle + (90 - DownAngle) * smoothAngleTimer;
        //    transform.rotation = Quaternion.Euler(movingDownAngle, angleTimer * -36, 0);
        //    float movingRadAngle = movingDownAngle * Mathf.PI / 180;
        //    transform.position = new Vector3(xPos * Mathf.Cos(movingRadAngle), Mathf.Sin(movingRadAngle), zPos * -Mathf.Cos(movingRadAngle)) * CameraDistance;
        //}
        //else if (zoomTimer > 0 || transIn)
        //{
        //    if (transIn)
        //    {
        //        zoomTimer += Time.deltaTime;
        //        transform.position = Vector3.up * (CameraDistance - (CameraDistance - ZoomStop) * zoomTimer * zoomTimer);
        //        if (zoomTimer >= 1)
        //        {
        //            zoomTimer = 1;
        //            transIn = false;
        //            mainMenu.SetActive(false);
        //            character.SetActive(true);
        //            canMove = true;
        //            //Transition stuff. Reposition the camera?
        //        }
        //    }
        //    else
        //    {
        //        zoomTimer -= Time.deltaTime;
        //        if (zoomTimer <= 0)
        //        {
        //            zoomTimer = 0;
        //        }
        //    }
        //    transition.color = new Color(1, 1, 1, zoomTimer);
        //}
        startClicked = false; // so this isn't ran repeatedly 
    }
}