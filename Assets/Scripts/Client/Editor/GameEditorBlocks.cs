using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
