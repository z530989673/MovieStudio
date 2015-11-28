using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameInfo {
    private List<Company> companyPool;
    private List<Movie> moviePool;
    private List<Charactor> charactorPool;


    public int MyCompanyID;
    public int[] myMovieIDs;
    public int[] myCharactorIDs;
    public List<int> roomLevel;
    public int startTime;   //TODO: Time type
    public int totalTime;   //TODO：Time type
}
