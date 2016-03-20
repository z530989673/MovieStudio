using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneCharacter : SceneNode
{
	private CharacterData characterData;
    public SceneCharacter(GameObject parent) : base(parent) { }
    public Pair pos;
    private int currentState;
}
