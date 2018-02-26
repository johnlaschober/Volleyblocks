﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : MonoBehaviour {

    public int pointsToGame = 3;
    public int gamesToWin = 3;
    private int leftPoints = 0, leftGames = 0, rightPoints = 0, rightGames = 0;
    public TextMesh lPointsText, lGamesText, rPointsText, rGamesText;

    private Passed passedObject;
    private RunningGame runningGame;

    bool matchComplete = false;

    private void Start()
    {
        runningGame = GetComponent<RunningGame>();
        try
        {
            passedObject = GameObject.Find("PassedObject").GetComponent<Passed>();
            pointsToGame = passedObject.gamesCount;
            gamesToWin = passedObject.setCount;
        }
        catch
        {
            Debug.LogError("Couldn't get Passed");
            pointsToGame = 1;
            gamesToWin = 1;
        }
    }

    public void PlayerLost (Board board)
    {
        if (board.Equals(GetComponent<BoardsInPlay>().rightBoard))
        {
            leftPoints++;
            lPointsText.text = leftPoints.ToString();
            CheckIfGame(leftPoints, "RIGHT");
        }
        else
        {
            rightPoints++;
            rPointsText.text = rightPoints.ToString();
            CheckIfGame(rightPoints, "LEFT");
        }
        bool matchPoint = false;
        bool setPoint = false;
        if (leftPoints == pointsToGame - 1 || rightPoints == pointsToGame - 1)
        {
            if (leftGames == gamesToWin - 1)
            {
                // Match point
                matchPoint = true;
                setPoint = false;
            }
            else if (rightGames == gamesToWin - 1)
            {
                // Match point
                matchPoint = true;
                setPoint = false;
            }
            else
            {
                // Set point only
                setPoint = true;
                matchPoint = false;
            }
        }

        runningGame.SetMatchComplete(matchComplete);
        runningGame.SetRunningGameOver(setPoint, matchPoint);
    }

    void CheckIfGame(int points, string position)
    {
        if (position.Equals("RIGHT"))
        {
            if (points >= pointsToGame)
            {
                points = 0;
                leftGames++;
                lGamesText.text = leftGames.ToString();
                lPointsText.text = "0";
                leftPoints = 0;
                rightPoints = 0;
                rPointsText.text = "0";
                CheckIfWinner(leftGames, position);
            }
        }
        else
        {
            if (points >= pointsToGame)
            {
                points = 0;
                rightGames++;
                rGamesText.text = rightGames.ToString();
                rPointsText.text = "0";
                rightPoints = 0;
                leftPoints = 0;
                lPointsText.text = "0";
                CheckIfWinner(rightGames, position);
            }
        }
    }

    private string playerWon;

    void CheckIfWinner(int games, string position)
    {
        if (position.Equals("RIGHT"))
        {
            if (games >= gamesToWin)
            {
                Debug.Log("LEFT BOARD WON THE SET");
                playerWon = "won";
                matchComplete = true;
                ResultsScreen();
            }
        }
        else
        {
            if (games >= gamesToWin)
            {
                Debug.Log("RIGHT BOARD WON THE SET");
                playerWon = "lost";
                matchComplete = true;
                ResultsScreen();
            }
        }
    }

    void ResultsScreen()
    {
        runningGame.SetMatchComplete(true);
        try
        {
            GetComponent<ResultsScreen>().ResultsSetup(playerWon, passedObject.isStory);
        }
        catch
        {
            GetComponent<ResultsScreen>().ResultsSetup(playerWon, false);
        }
        if (playerWon == "won" && passedObject.isStory)
        {
            passedObject.storyIndex++;
        }
    }
}
