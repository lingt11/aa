using System;
using UnityEngine;

// Token: 0x02000019 RID: 25
public class Sound
{
	// Token: 0x17000006 RID: 6
	// (get) Token: 0x0600006D RID: 109 RVA: 0x00003B7F File Offset: 0x00001D7F
	public float progress
	{
		get
		{
			if (this.source == null || this.clip == null)
			{
				return 0f;
			}
			return (float)this.source.timeSamples / (float)this.clip.samples;
		}
	}

	// Token: 0x17000007 RID: 7
	// (get) Token: 0x0600006E RID: 110 RVA: 0x00003BBC File Offset: 0x00001DBC
	public bool finished
	{
		get
		{
			return !this.loop && this.progress >= 1f;
		}
	}

	// Token: 0x17000008 RID: 8
	// (get) Token: 0x0600006F RID: 111 RVA: 0x00003BD8 File Offset: 0x00001DD8
	// (set) Token: 0x06000070 RID: 112 RVA: 0x00003BF5 File Offset: 0x00001DF5
	public bool playing
	{
		get
		{
			return this.source != null && this.source.isPlaying;
		}
		set
		{
			if (value)
			{
				if (!this.source.isPlaying)
				{
					this.source.UnPause();
					return;
				}
			}
			else if (this.source.isPlaying)
			{
				this.source.Pause();
			}
		}
	}

	// Token: 0x06000071 RID: 113 RVA: 0x00003C2C File Offset: 0x00001E2C
	public Sound(AudioManager audioManager, AudioClip clip, AudioSource source, string path, bool loop)
	{
		this.audioManager = audioManager;
		this.path = path;
		this.clip = clip;
		this.source = source;
		this.source.clip = clip;
		this.source.Play();
		this.loop = loop;
	}

	// Token: 0x06000072 RID: 114 RVA: 0x00003C86 File Offset: 0x00001E86
	public void Play()
	{
		this.source.Play();
	}

	// Token: 0x06000073 RID: 115 RVA: 0x00003C93 File Offset: 0x00001E93
	public void Update()
	{
		if (this.source != null)
		{
			this.source.loop = this.loop;
		}
		if (this.finished)
		{
			this.Finish();
		}
	}

	// Token: 0x06000074 RID: 116 RVA: 0x00003CC2 File Offset: 0x00001EC2
	public void Finish()
	{
		this.audioManager.ReleaseAudioSource(this.source);
		this.source = null;
	}

	// Token: 0x04000066 RID: 102
	public AudioClip clip;

	// Token: 0x04000067 RID: 103
	public AudioSource source;

	// Token: 0x04000068 RID: 104
	public bool loop;

	// Token: 0x04000069 RID: 105
	public string path;

	// Token: 0x0400006A RID: 106
	private AudioManager audioManager;

	// Token: 0x0400006B RID: 107
	public float lifeTime = 3f;
}
