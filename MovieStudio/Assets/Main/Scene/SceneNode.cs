using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class SceneNode {
    protected GameObject gameObject;
    protected GameObject parent;
    protected Pair coord;
    public int order;

    public SceneNode(GameObject p)
    {
        parent = p;
    }

    protected void Reset(Pair pos, int ord = 0, bool revert = false, string path = "", Color? color = null)
    {
        coord = pos;
        order = ord - (pos.x + pos.y) * (int)ITEM_ITEM_ORDER.ITEM_ORDER_MAX;
        if (gameObject == null)
        {
            gameObject = GameObject.Instantiate(GameManager.Instance.GetResourceObject("UI/Prefabs/SceneItem"));
            gameObject.transform.parent = parent.transform;
        }
        if (path != "")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.GetResourceSprite(path);
            gameObject.GetComponent<SpriteRenderer>().color = color / 255.0f?? Color.white;
        }
        if (revert)
            gameObject.transform.Rotate(0, 180, 0);
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = order;

        Vector2 actualPos = SceneManager.GetActualPos(coord);
        gameObject.transform.position = new Vector3(actualPos.x, actualPos.y,0);
    }

    public void Remove()
    {
        GameObject.Destroy(gameObject);
    }
}
