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
    // There may have more things that should be loaded at the very beginning
    // like config files and game data and textures!!! But, temporarily, I
    // can't find any good type to represent them.
    private List<string> prefabLoadList;
    private List<string> textureLoadList;
    private List<string> fileLoadList;

    private int totalLoad, currentLoaded;

    public void AddPreLoadResource(RESOURCE_TYPE type, string path)
    {
        switch(type)
        {
            case RESOURCE_TYPE.RESOURCE_PREFAB:
                prefabLoadList.Add(path);
                break;
            case RESOURCE_TYPE.RESOURCE_TEXTURE:
                textureLoadList.Add(path);
                break;
            case RESOURCE_TYPE.RESOURCE_FILE:
                fileLoadList.Add(path);
                break;
        }
    }

    public IEnumerator StartPreLoad()
    {
        totalLoad = prefabLoadList.Count + textureLoadList.Count + fileLoadList.Count;

        for (int i = 0; i < prefabLoadList.Count; i++)
        {
            Load(prefabLoadList[i], RESOURCE_TYPE.RESOURCE_PREFAB);
            yield return 0;
        }
        for (int i = 0; i < textureLoadList.Count; i++)
        {
            Load(textureLoadList[i], RESOURCE_TYPE.RESOURCE_TEXTURE);
            yield return 0;
        }
        for (int i = 0; i < fileLoadList.Count; i++)
        {
            Load(fileLoadList[i], RESOURCE_TYPE.RESOURCE_FILE);
            yield return 0;
        }

        EventManager.Instance.SendEvent(EVT_TYPE.EVT_TYPE_PRELOAD_TOTAL_FINISH);

    }

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

    private void Load(string path, RESOURCE_TYPE type)
    {
        Event evt = new Event(EVT_TYPE.EVT_TYPE_PRELOAD_PARTIAL_FINISH);

        switch (type)
        {
            case RESOURCE_TYPE.RESOURCE_PREFAB:
                GameObject obj = Resources.Load<GameObject>(path);
                evt.evt_obj.Add(obj);
                if (!loadedObjects.ContainsKey(path))
                    loadedObjects[path] = obj;
                break;
            case RESOURCE_TYPE.RESOURCE_TEXTURE:
                //textureLoadList.Add(path);
                break;
            case RESOURCE_TYPE.RESOURCE_FILE:
                //fileLoadList.Add(path);
                break;
        }

        currentLoaded++;
        evt.evt_obj.Add((float)currentLoaded / totalLoad);

        GameManager.Instance.SendEvent(evt);
    }

	// Use this for initialization
	void Start () {
        loadedObjects = new Dictionary<string, GameObject>(200);
        prefabLoadList = new List<string>(200);
        textureLoadList = new List<string>(200);
        fileLoadList = new List<string>(200);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
