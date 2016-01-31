using UnityEngine;
using System.Collections;

public class Movie {
    public int id;
    public string title;
    public string desc;
    public MOVIE_TYPE type;
    public int theme; // int or enum?
    public int platform; //int or enum?
    public int level;
    public int cost;
    public int incomes;
    public MOVIE_STAGE stage;
    public int storyScore;
    public int creativityScore;
    public int artScore;
    public int musicScore;
    public int companyID;
    public int[] characterIDs;
}