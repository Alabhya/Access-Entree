using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PlayerDatum { // singular for data is datum
    public int AcessScore { get; set; }
    public string Name { get; set; }
}

public static class PlayerInfo
{
    private static string name = "test player";
    private static string username = "testPlayerOne";
    static PlayerDatum currentPlayer;
    private static int accessScore;
    static Dictionary<string, PlayerDatum> playerData = new Dictionary<string, PlayerDatum>(); 
    
    static PlayerInfo() {
        playerData = DB.LoadItems<PlayerDatum>("playerData.json");
        currentPlayer = playerData["testPlayerOne"];
    }
    
    public static void savePlayerData() {
        DB.SaveItems("playerData.json", playerData);
    }
    public static string getName() {
        return currentPlayer.Name;
    }
    public static int getScore() {
        return currentPlayer.AcessScore;
    }
}
