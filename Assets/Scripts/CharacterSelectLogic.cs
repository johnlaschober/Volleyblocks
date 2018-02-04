﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectLogic : MonoBehaviour {

    SelectedCharacters charStorage;
    GameObject menu;
    Passed passed;
    public GameObject p1Arrow, p2Arrow;

    private int p1Selection = 0, p2Selection = 4;
    bool p1Locked = false, p2Locked = false;
    bool menuInitialized = false;

    private void Awake()
    {
        menu = gameObject;
        charStorage = GameObject.Find("PassedObject").GetComponent<SelectedCharacters>();
        passed = GameObject.Find("PassedObject").GetComponent<Passed>();
    }
	
	void Update ()
    {
	    if (menu.activeSelf)
        {
            if (!menuInitialized)
            {
                Debug.Log(passed.playersInPlay);
                switch (passed.playersInPlay)
                {
                    case "Player VS CPU":
                        SetRandomCharacter(2);
                        p1Arrow.SetActive(true);
                        p2Locked = true;
                        break;
                    case "Player VS Player":
                        p1Arrow.SetActive(true);
                        p2Arrow.SetActive(true);
                        break;
                    case "CPU VS CPU":
                    default:
                        SetRandomCharacter(1);
                        SetRandomCharacter(2);
                        // Just exit / start battle / whatever
                        break;
                }
                menuInitialized = true;
            }
            MoveCursor(p1Arrow, ref p1Selection);
            MoveCursor(p2Arrow, ref p2Selection);
            // If player vs CPU -> One cursor
            // If player vs player -> Two cursors
            // If CPU Vs CPU -> skip character select and load game
            // but first set up random portraits

            if (p1Locked && p2Locked)
            {
                // display start button
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    // start
                    GameObject.Find("MenuLogic").GetComponent<ButtonLogic>().ContinueFromCharacterSelect();
                }
            }
        }
	}

    void MoveCursor(GameObject arrow, ref int selection)
    {
        if (arrow.name.Equals("P1arrow"))
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                HorizontalMovement(true, arrow, ref selection);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                HorizontalMovement(false, arrow, ref selection);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                Confirm();
                LockCursor(1);
            }
        }
        else
        {

        }
    }

    void HorizontalMovement(bool right, GameObject arrow, ref int selection)
    {
        if (right)
        {
            selection++;
            arrow.transform.position = new Vector3(arrow.transform.position.x + 3.5f, arrow.transform.position.y, 0f);
            if (selection >= 5)
            {
                selection = 0;
                arrow.transform.position = new Vector3(-7f, arrow.transform.position.y, 0f);
            }
        }
        else
        {
            selection--;
            arrow.transform.position = new Vector3(arrow.transform.position.x - 3.5f, arrow.transform.position.y, 0f);
            if (selection < 0)
            {
                selection = 4;
                arrow.transform.position = new Vector3(7f, arrow.transform.position.y, 0f);
            }
        }
    }

    void Confirm()
    {
        SetCharacters();
    }

    void LockCursor(int playerNum)
    {
        if (playerNum == 1)
        {
            p1Locked = true;
        }
        else
        {
            p2Locked = true;
        }
    }

    void SetRandomCharacter(int playerNum)
    {
        if (playerNum == 1)
        {
            p1Selection = Random.Range(0, 5);
        }
        else
        {
            p2Selection = Random.Range(0, 5);
        }
    }

    void SetCharacters()
    {
        string p1Name;
        string p2Name;
        switch (p1Selection)
        {
            default:
            case 0:
                p1Name = "buff";
                break;
            case 1:
                p1Name = "girl";
                break;
            case 2:
                p1Name = "hatty";
                break;
            case 3:
                p1Name = "prof";
                break;
            case 4:
                p1Name = "sensei";
                break;
        }
        switch (p2Selection)
        {
            default:
            case 0:
                p2Name = "buff";
                break;
            case 1:
                p2Name = "girl";
                break;
            case 2:
                p2Name = "hatty";
                break;
            case 3:
                p2Name = "prof";
                break;
            case 4:
                p2Name = "sensei";
                break;
        }
        charStorage.SetLeftBoard(p1Name);
        charStorage.SetRightBoard(p2Name);

    }
    // Need to add a new "start" button to character select screen that calls
    // button logic and does the same similar stuff as "start" used to do before
    // going to the character select screen
}
