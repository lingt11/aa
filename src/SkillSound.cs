using System;
using UnityEngine;

// Token: 0x0200007B RID: 123
public class SkillSound : MonoBehaviour
{
	// Token: 0x0600026B RID: 619 RVA: 0x0000CAC5 File Offset: 0x0000ACC5
	private void Awake()
	{
		this.myTransform = base.transform;
		if (Mathf.Approximately(this.volume, 0f))
		{
			this.volume = 1f;
		}
	}

	// Token: 0x0600026C RID: 620 RVA: 0x0000CAF0 File Offset: 0x0000ACF0
	private void OnEnable()
	{
		this.isPlay = false;
	}

	// Token: 0x0600026D RID: 621 RVA: 0x0000CAF9 File Offset: 0x0000ACF9
	private void OnDisable()
	{
		if (this.isPlay && this.isLoop)
		{
			this.StopSound();
		}
	}

	// Token: 0x0600026E RID: 622 RVA: 0x0000CB14 File Offset: 0x0000AD14
	private void Update()
	{
		if (GameHelperClient.localPlayer == null && this.isPlay)
		{
			this.StopSound();
			return;
		}
		if (!this.isPlay)
		{
			if (Util.GetV2Distance(GameHelperClient.localPlayer.MyTransform.position, this.myTransform.position) < 20f)
			{
				this.PlaySound();
				return;
			}
		}
		else if (this.isLoop && Util.GetV2Distance(GameHelperClient.localPlayer.MyTransform.position, this.myTransform.position) > 20f)
		{
			this.StopSound();
		}
	}

	// Token: 0x0600026F RID: 623 RVA: 0x0000CBA8 File Offset: 0x0000ADA8
	private void PlaySound()
	{
		this.isPlay = true;
		if (this.isLoop)
		{
			this.mySound = Game.AudioManager.PlayAudio(this.clip, this.isLoop, this.volume);
			return;
		}
		Game.AudioManager.PlayAudio(this.clip, this.isLoop, this.volume);
	}

	// Token: 0x06000270 RID: 624 RVA: 0x0000CC04 File Offset: 0x0000AE04
	private void StopSound()
	{
		this.isPlay = false;
		if (this.mySound != null && this.mySound.source != null)
		{
			this.mySound.source.Stop();
			this.mySound = null;
		}
	}

	// Token: 0x04000264 RID: 612
	public AudioClip clip;

	// Token: 0x04000265 RID: 613
	public float volume = 1f;

	// Token: 0x04000266 RID: 614
	public bool isLoop;

	// Token: 0x04000267 RID: 615
	private bool isPlay;

	// Token: 0x04000268 RID: 616
	private Transform myTransform;

	// Token: 0x04000269 RID: 617
	private Sound mySound;
}
