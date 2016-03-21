using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameInfo {

    public GameInfo()
    {
        //temp
        int length = 4;
        roomLevel = new int[length];
        for (int i = 0; i < length; i++)
            roomLevel[i] = 1;
    }

    private List<Company> companyPool;
    private List<Movie> moviePool;
    private List<Character> characterPool;


    public int MyCompanyID;
    public int[] myMovieIDs;
    public int[] myCharacterIDs;
    public int startTime;   //TODO: Time type
    public int totalTime;   //TODO：Time type

    public int[] roomLevel;
}
