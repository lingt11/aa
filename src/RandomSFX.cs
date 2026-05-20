using System;
using UnityEngine;

// Token: 0x02000402 RID: 1026
public class RandomSFX : MonoBehaviour
{
	// Token: 0x06001786 RID: 6022 RVA: 0x00092FF4 File Offset: 0x000911F4
	private void Start()
	{
		this.asource = base.GetComponent<AudioSource>();
		this.asource.clip = this.clips[Random.Range(0, this.clips.Length)];
		this.asource.Play();
		this.asource.pitch = Random.Range(0.9f, 1.1f);
	}

	// Token: 0x0400166B RID: 5739
	public AudioClip[] clips;

	// Token: 0x0400166C RID: 5740
	private AudioSource asource;
}
