using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000006 RID: 6
public class AssetManager
{
	// Token: 0x0600000F RID: 15 RVA: 0x00002930 File Offset: 0x00000B30
	public static void Init()
	{
		AssetManager.assetPoolRoot = new GameObject
		{
			name = "_AssetPoolRoot"
		};
	}

	// Token: 0x06000010 RID: 16 RVA: 0x00002947 File Offset: 0x00000B47
	public static void Clear()
	{
		AssetManager.loadDic.Clear();
		AssetManager.assetsQueueDic.Clear();
		AssetManager.assetPoolRoot = null;
	}

	// Token: 0x06000011 RID: 17 RVA: 0x00002964 File Offset: 0x00000B64
	public static T LoadAsset<T>(string path) where T : ScriptableObject
	{
		GameMode gameMode = Launch.GameMode;
		int startIndex = path.LastIndexOf('/') + 1;
		path = path.Substring(startIndex);
		return (T)((object)Launch.assetBundle.LoadAsset<ScriptableObject>(path + ".asset"));
	}

	// Token: 0x06000012 RID: 18 RVA: 0x000029A5 File Offset: 0x00000BA5
	private static GameObject LoadAssetData(string path)
	{
		return Resources.Load<GameObject>("Bundles/" + path);
	}

	// Token: 0x06000013 RID: 19 RVA: 0x000029B7 File Offset: 0x00000BB7
	public static GameObject LoadPrefab(string path, Vector3 pos)
	{
		GameObject gameObject = AssetManager.LoadPrefab(path, null, true);
		gameObject.transform.position = pos;
		return gameObject;
	}

	// Token: 0x06000014 RID: 20 RVA: 0x000029D0 File Offset: 0x00000BD0
	public static GameObject LoadPrefab(string path, Transform parent = null, bool isActive = true)
	{
		Queue<GameObject> queue;
		if (AssetManager.assetsQueueDic.ContainsKey(path))
		{
			queue = AssetManager.assetsQueueDic[path];
		}
		else
		{
			queue = new Queue<GameObject>();
			AssetManager.assetsQueueDic.Add(path, queue);
		}
		GameObject gameObject;
		if (queue.Count > 0)
		{
			gameObject = queue.Dequeue();
		}
		else
		{
			GameObject gameObject2;
			if (AssetManager.loadDic.ContainsKey(path))
			{
				gameObject2 = AssetManager.loadDic[path];
			}
			else
			{
				gameObject2 = AssetManager.LoadAssetData(path);
				AssetManager.loadDic.Add(path, gameObject2);
			}
			if (gameObject2 == null)
			{
				Debug.LogError("加载失败" + path);
			}
			gameObject = Object.Instantiate<GameObject>(gameObject2, parent);
		}
		if (isActive)
		{
			gameObject.SetActive(true);
		}
		gameObject.name = path;
		gameObject.transform.SetParent(parent);
		if (gameObject.GetComponent<IRes>() != null)
		{
			IRes[] components = gameObject.GetComponents<IRes>();
			for (int i = 0; i < components.Length; i++)
			{
				components[i].ResReset();
			}
		}
		return gameObject;
	}

	// Token: 0x06000015 RID: 21 RVA: 0x00002AB8 File Offset: 0x00000CB8
	public static void UnLoadPrefab(GameObject go, bool notMove = false)
	{
		string name = go.name;
		if (!AssetManager.assetsQueueDic.ContainsKey(name))
		{
			return;
		}
		if (AssetManager.assetsQueueDic[name].Contains(go))
		{
			Debug.LogError("已经在池子里面了" + name);
			return;
		}
		if (AssetManager.assetsQueueDic[name].Count >= AssetManager.maxCount)
		{
			Object.Destroy(go);
			return;
		}
		go.SetActive(false);
		if (!notMove)
		{
			go.transform.parent = AssetManager.assetPoolRoot.transform;
		}
		AssetManager.assetsQueueDic[name].Enqueue(go);
	}

	// Token: 0x04000027 RID: 39
	private static int maxCount = 50;

	// Token: 0x04000028 RID: 40
	private static GameObject assetPoolRoot;

	// Token: 0x04000029 RID: 41
	private static Dictionary<string, GameObject> loadDic = new Dictionary<string, GameObject>();

	// Token: 0x0400002A RID: 42
	private static Dictionary<string, Queue<GameObject>> assetsQueueDic = new Dictionary<string, Queue<GameObject>>();
}
