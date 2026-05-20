using System;
using UnityEngine;

// Token: 0x0200006F RID: 111
public class HS_EffectSound : MonoBehaviour
{
	// Token: 0x0600022D RID: 557 RVA: 0x0000C19C File Offset: 0x0000A39C
	private void Start()
	{
		this.soundComponent = base.GetComponent<AudioSource>();
		this.clip = this.soundComponent.clip;
		if (this.RandomVolume)
		{
			this.soundComponent.volume = Random.Range(this.minVolume, this.maxVolume);
			this.RepeatSound();
		}
		if (this.Repeating)
		{
			base.InvokeRepeating("RepeatSound", this.StartTime, this.RepeatTime);
		}
	}

	// Token: 0x0600022E RID: 558 RVA: 0x0000C20F File Offset: 0x0000A40F
	private void RepeatSound()
	{
		this.soundComponent.PlayOneShot(this.clip);
	}

	// Token: 0x04000246 RID: 582
	public bool Repeating = true;

	// Token: 0x04000247 RID: 583
	public float RepeatTime = 2f;

	// Token: 0x04000248 RID: 584
	public float StartTime;

	// Token: 0x04000249 RID: 585
	public bool RandomVolume;

	// Token: 0x0400024A RID: 586
	public float minVolume = 0.4f;

	// Token: 0x0400024B RID: 587
	public float maxVolume = 1f;

	// Token: 0x0400024C RID: 588
	private AudioClip clip;

	// Token: 0x0400024D RID: 589
	private AudioSource soundComponent;
}
