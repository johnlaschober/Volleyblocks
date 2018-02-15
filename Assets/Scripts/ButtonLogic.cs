﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLogic : MonoBehaviour {

    private string players = "Player VS CPU";
    private string cpuDifficulty = "Easy";
    private float turnLength = 0.3f;

    private int gamesCount = 3;
    private int setsCount = 3;

    public UnityEngine.UI.Text playersText, cpuText, gamesText, setsText;
    public GameObject mainMenu, arcadeMenu, versusMenu, passwordMenu, characterSelect;
    public UnityEngine.EventSystems.EventSystem eventSystem;
    public GameObject arcadeButton, versusButton, arcadeNewButton, versusStartButton, passwordButton, passBackButton; // Entry buttons

    public void ButtonPressLogic(GameObject newButton, GameObject desiredMenu, GameObject currentMenu)
    {
        eventSystem.SetSelectedGameObject(newButton);
        newButton.GetComponent<UnityEngine.UI.Button>().OnSelect(null);
        desiredMenu.SetActive(true);
        currentMenu.SetActive(false);
    }

    public void Arcade()
    {
        ButtonPressLogic(arcadeNewButton, arcadeMenu, mainMenu);
    }

    public void Versus()
    {
        ButtonPressLogic(versusStartButton, versusMenu, mainMenu);
    }

    public void VersusBack()
    {
        ButtonPressLogic(versusButton, mainMenu, versusMenu);
    }

    public void ArcadeBack()
    {
        ButtonPressLogic(arcadeButton, mainMenu, arcadeMenu);
    }

    public void PasswordBack()
    {
        ButtonPressLogic(passwordButton, arcadeMenu, passwordMenu);
    }

    public void PasswordButton()
    {
        ButtonPressLogic(passBackButton, passwordMenu, arcadeMenu);
    }

    public void ArcadeNewGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public void StartButton()
    {
        // This should load character select instead
        GameObject.Find("PassedObject").GetComponent<Passed>().StoreValues(players, turnLength, gamesCount, setsCount);
        versusMenu.SetActive(false);
        characterSelect.SetActive(true);
    }

    public void ContinueFromCharacterSelect()
    {
        GetComponent<LoadBattle>().LoadNewBattle(players, turnLength, gamesCount, setsCount);
    }

    public void PlayersButton()
    {
        switch (players)
        {
            case "Player VS CPU":
                players = "Player VS Player";
                break;
            case "Player VS Player":
                players = "CPU VS CPU";
                break;
            case "CPU VS CPU":
            default:
                players = "Player VS CPU";
                break;
        }
        playersText.text = players;
    }

    public void DifficultyButton()
    {
        switch (cpuDifficulty)
        {
            case "Easy":
                cpuDifficulty = "Medium";
                turnLength = 0.2f;
                break;
            case "Medium":
                cpuDifficulty = "Hard";
                turnLength = 0.1f;
                break;
            case "Hard":
                cpuDifficulty = "Impossible";
                turnLength = 0.0f;
                break;
            case "Impossible":
            default:
                cpuDifficulty = "Easy";
                turnLength = 0.3f;
                break;
        }
        cpuText.text = cpuDifficulty;
    }

    public void GamesButton()
    {
        gamesCount++;
        if (gamesCount > 15)
        {
            gamesCount = 1;
        }

        gamesText.text = "Games: First to " + gamesCount;
    }

    public void SetsButton()
    {
        setsCount++;
        if (setsCount > 5)
        {
            setsCount = 1;
        }

        setsText.text = "Sets: First to " + setsCount;
    }

}
