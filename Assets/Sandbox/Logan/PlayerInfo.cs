using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PlayerDatum { // singular for data is datum
    public string AccessScore { get; set; }
    public string Name { get; set; }
}

public static class PlayerInfo
{
    private static string name = "test player"; // test data until we have player login
    private static string username = "testPlayerOne";
    static PlayerDatum currentPlayer;
    private static string accessScore = "10"; // test data to initialize JSON with
    static Dictionary<string, PlayerDatum> playerData = new Dictionary<string, PlayerDatum>(); 
    static PlayerInfo() {
        playerData = DB.LoadItems<PlayerDatum>("playerData.json");
        // check if there is any saved data for current username
        if(!playerData.ContainsKey(username)) {
            PlayerDatum initPlayer = new PlayerDatum(); 
            initPlayer.Name = name; 
            initPlayer.AccessScore = accessScore; 
            playerData[username] = initPlayer; 
            savePlayerData();
        }

        currentPlayer = playerData[username];
    }
    
    public static void savePlayerData() {
        DB.SaveItems("playerData.json", playerData);
    }
    public static string getName() {
        return currentPlayer.Name;
    }
    public static string getScore() {
        return currentPlayer.AccessScore;
    }
}
