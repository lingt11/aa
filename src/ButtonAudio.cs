using System;
using UnityEngine;

// Token: 0x0200000D RID: 13
public class ButtonAudio : MonoBehaviour
{
	// Token: 0x0600002A RID: 42 RVA: 0x00002D27 File Offset: 0x00000F27
	public void PlayButtonAudio()
	{
		this.clip == null;
	}

	// Token: 0x0400003C RID: 60
	[Header("可选,无则播放默认音频")]
	[Tooltip("可选,无则播放默认音频")]
	public AudioClip clip;
}
