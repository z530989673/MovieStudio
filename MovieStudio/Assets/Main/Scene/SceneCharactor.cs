using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneCharactor : SceneNode
{
	private CharacterData characterData;
    public SceneCharactor(GameObject parent) : base(parent) { }
    public Pair pos;
    private int currentState;
}
