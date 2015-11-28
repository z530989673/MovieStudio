using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneNode {
    protected GameObject gameObject;
    protected Pair coord;

    public void Reset(Pair pos,string path = "")
    {
        coord = pos;
        if (gameObject == null)
            gameObject = GameObject.Instantiate(GameManager.Instance.GetResourceObject("UI/Prefabs/SceneItem"));
        if (path != "")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.GetResourceSprite(path);
        }

        Vector2 actualPos = SceneManager.Instance.GetActualPos(coord);
        gameObject.transform.position = new Vector3(actualPos.x, actualPos.y,0);
    }

    public void Remove()
    {
        GameObject.Destroy(gameObject);
    }
}
