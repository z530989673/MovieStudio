using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneNode {
    protected GameObject gameObject;
    protected GameObject parent;
    protected Pair coord;

    public SceneNode(GameObject p)
    {
        parent = p;
    }

    public void Reset(Pair pos, int order = 0, bool revert = false, string path = "")
    {
        coord = pos;
        if (gameObject == null)
        {
            gameObject = GameObject.Instantiate(GameManager.Instance.GetResourceObject("UI/Prefabs/SceneItem"));
            gameObject.transform.parent = parent.transform;
        }
        if (path != "")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.GetResourceSprite(path);
        }
        
        if (revert)
            gameObject.transform.Rotate(0, 180, 0);
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = order;

        Vector2 actualPos = SceneManager.Instance.GetActualPos(coord);
        gameObject.transform.position = new Vector3(actualPos.x, actualPos.y,0);
    }

    public void Remove()
    {
        GameObject.Destroy(gameObject);
    }
}
