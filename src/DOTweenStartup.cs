using System;
using DG.Tweening;
using UnityEngine;

// Token: 0x02000035 RID: 53
public static class DOTweenStartup
{
	// Token: 0x060000C5 RID: 197 RVA: 0x00005C66 File Offset: 0x00003E66
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void Configure()
	{
		DOTween.SetTweensCapacity(1000, 500);
	}

	// Token: 0x040000FA RID: 250
	private const int TweenersCapacity = 1000;

	// Token: 0x040000FB RID: 251
	private const int SequencesCapacity = 500;
}
