using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour {

    private static ResourceManager m_instance;
    private ResourceManager() { }

    public static ResourceManager Instance
    {
        get
        {
            if (m_instance == null)
                m_instance = GameObject.FindObjectOfType(typeof(ResourceManager)) as ResourceManager;
            return m_instance;
        }
    }

    private Dictionary<string, GameObject> loadedObjects;
    // There may have more things should be loaded at the very beginning
    // like config files and game data and textures!!! But, temporarily,
    // I can't find any good type to represent them.

    public GameObject GetResourceObject(string str)
    {
        if (!loadedObjects.ContainsKey(str))
        {
            GameObject obj = Resources.Load<GameObject>(str);
            loadedObjects.Add(str, obj);
        }

        //TODO: what if failed?

        return loadedObjects[str];
    }

	// Use this for initialization
	void Start () {
        loadedObjects = new Dictionary<string, GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
