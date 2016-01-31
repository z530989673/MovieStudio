using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum eCharacterType {
	eCharacterTypeActor = 1,
	eCharacterTypeTech = 2
}

public enum eCharacterAbility {
	eCharacterAbilityNone = 0
}

public class CharacterData {
	public int characterID;
	public string name;
	public int gender;
	public string description;
	public int rarity;
	public int level;
	public eCharacterType type;
	public int appearance;
	public int voice;
	public int actingSkill;
	public int creativeSkill;
	public int artTechSkill;
	public int musicTechSkill;
	public int companyID;
	public List<int> movieIDs;
	public eCharacterAbility characterAbilityID;
}
