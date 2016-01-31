using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Company {
    public int id;
    public string title;
    public string desc;
    public COMPANY_STAGE stage;
    public Dictionary<COMPANY_STAGE, int> movieTypeLevels;
    public Dictionary<int, int> themeTypeLevels;
    public Dictionary<int, int> paltformLevels;
    public int level;
    public int incomes;
    public int[] movieIDs;
    public int[] charactorIDs;
	public List<Team> teams;
}
