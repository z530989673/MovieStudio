using UnityEngine;
using System.Collections;

public class PlayerInfo {
    public PlayerInfo()
    {
        configInfo = new ConfigInfo();
    }

    public ConfigInfo configInfo;
    public int MyCompanyID;
    public int[] myMovieIDs;
    public int[] myCharactorIDs;
    public int startTime;   //TODO: Time type
    public int totalTime;   //TODO：Time type
}
