using System;
using UnityEngine;
//deprecated
public class GameEditorBlocks
{
    public GameObject PrefabFinder(GameObject blockSprite)
    {
        String name = blockSprite.name;
        String path = "Prefabs" + "/" + name;
        GameObject prefab = Resources.Load<GameObject>(path);
        return prefab;
    }
}