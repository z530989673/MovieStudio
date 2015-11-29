using UnityEngine;
using System.Collections;

public class SceneCharactor : SceneNode
{
    public SceneCharactor(GameObject parent) : base(parent) { }
    public Pair pos;
    private int currentState;
}
