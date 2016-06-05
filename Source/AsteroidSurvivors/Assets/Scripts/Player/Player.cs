using UnityEngine;
using System.Collections;

public class Player {
    private static Player playerInstant;
    public static Player GetInstant
    {
        get
        {
            if (playerInstant == null)
                playerInstant = new Player();
            return playerInstant;
        }
    }

    private GameObject PlayerObject;


    private PlayerState playerState = PlayerState.None;

    public PlayerState PlayerState
    {
        get { return playerState; }
        set { playerState = value; }
    }

    private Player()
    {
        // private constructor so nobody can create a new player
        foreach (GameObject potentionalPlayer in GameObject.FindGameObjectsWithTag("Player"))
        {
            if(potentionalPlayer.name == "Player")
            {
                if (PlayerObject != null)
                {
                    Debug.LogWarning("More player objects! (" + GameObject.FindGameObjectsWithTag("Player").Length + ")");
                }
                else
                {
                    PlayerObject = potentionalPlayer;
                }
            }
        }
        if (PlayerObject == null)
        {
            Debug.LogWarning("No player objects!");
        }
    }

    public PlayerData Save()
    {
        return new PlayerData()
        {
            TimePlayed = 552454
        };
    }
    public void Load(PlayerData playerData)
    {

    }
}
