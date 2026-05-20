using System;
using System.Collections;
using System.Threading;
using UnityEngine;

// Token: 0x020000A6 RID: 166
public class MainThreadUtil : MonoBehaviour
{
	// Token: 0x1700003A RID: 58
	// (get) Token: 0x06000326 RID: 806 RVA: 0x0001558F File Offset: 0x0001378F
	// (set) Token: 0x06000327 RID: 807 RVA: 0x00015596 File Offset: 0x00013796
	public static MainThreadUtil Instance { get; private set; }

	// Token: 0x1700003B RID: 59
	// (get) Token: 0x06000328 RID: 808 RVA: 0x0001559E File Offset: 0x0001379E
	// (set) Token: 0x06000329 RID: 809 RVA: 0x000155A5 File Offset: 0x000137A5
	public static SynchronizationContext synchronizationContext { get; private set; }

	// Token: 0x0600032A RID: 810 RVA: 0x000155AD File Offset: 0x000137AD
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	public static void Setup()
	{
		MainThreadUtil.Instance = new GameObject("MainThreadUtil").AddComponent<MainThreadUtil>();
		MainThreadUtil.synchronizationContext = SynchronizationContext.Current;
	}

	// Token: 0x0600032B RID: 811 RVA: 0x000155D0 File Offset: 0x000137D0
	public static void Run(IEnumerator waitForUpdate)
	{
		MainThreadUtil.synchronizationContext.Post(delegate(object _)
		{
			MainThreadUtil.Instance.StartCoroutine(waitForUpdate);
		}, null);
	}

	// Token: 0x0600032C RID: 812 RVA: 0x00015601 File Offset: 0x00013801
	private void Awake()
	{
		base.gameObject.hideFlags = HideFlags.HideAndDontSave;
		Object.DontDestroyOnLoad(base.gameObject);
	}
}
