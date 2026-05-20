using System;
using UnityEngine;

// Token: 0x0200007A RID: 122
public class SingletonMonobehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
	// Token: 0x17000039 RID: 57
	// (get) Token: 0x06000268 RID: 616 RVA: 0x0000CA50 File Offset: 0x0000AC50
	public static T Instance
	{
		get
		{
			if (SingletonMonobehaviour<T>.instance != null)
			{
				return SingletonMonobehaviour<T>.instance;
			}
			SingletonMonobehaviour<T>.instance = new GameObject(typeof(T).Name).AddComponent<T>();
			if (Application.isPlaying)
			{
				Object.DontDestroyOnLoad(SingletonMonobehaviour<T>.instance.gameObject);
			}
			return SingletonMonobehaviour<T>.instance;
		}
	}

	// Token: 0x06000269 RID: 617 RVA: 0x0000CAB3 File Offset: 0x0000ACB3
	private void Awake()
	{
		SingletonMonobehaviour<T>.instance = (this as T);
	}

	// Token: 0x04000263 RID: 611
	private static T instance;
}
