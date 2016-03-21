using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class SceneNode {
    protected GameObject gameObject;
    protected GameObject parent;
    protected Pair coord;
    public int refOrder;
    bool isActive;

    public SceneNode(GameObject p)
    {
        parent = p;
        isActive = false;
    }

    protected void Reset(Pair pos, int ord = 0, bool revert = false, string path = "", Color? color = null)
    {
        isActive = true;
        coord = pos;
        refOrder = ord;
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

        Vector2 actualPos = SceneManager.GetActualPos(coord);
        gameObject.transform.position = new Vector3(actualPos.x, actualPos.y,0);
    }

    virtual public void Update()
    {
        if (isActive)
        {
            int order = refOrder - (coord.x + coord.y) * GameConstant.ITEM_ORDER_UPLIMIT;
            order = refOrder + SceneManager.Instance.GetTileOrder(coord) * GameConstant.ITEM_ORDER_UPLIMIT;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = order;
        }
    }

    public void Remove()
    {
        GameObject.Destroy(gameObject);
    }
}
