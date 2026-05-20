using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000018 RID: 24
public class AudioManager : Entity, IUpdate, IApplicationQuit
{
	// Token: 0x06000050 RID: 80 RVA: 0x000031B4 File Offset: 0x000013B4
	public AudioManager()
	{
		this.gameObject = new GameObject("AudioManager");
		this.bgmSource = this.gameObject.AddComponent<AudioSource>();
		this.MusicVol = PlayerPrefs.GetFloat("settings.audio.sfx", 0.5f);
		this.bgmVol = PlayerPrefs.GetFloat("settings.audio.bgm", 0.5f);
		this.PlayBGM("Audio/BGM/GoblinBGM", true);
	}

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x06000051 RID: 81 RVA: 0x0000328D File Offset: 0x0000148D
	// (set) Token: 0x06000052 RID: 82 RVA: 0x00003298 File Offset: 0x00001498
	public float MusicVol
	{
		get
		{
			return this.musicVol;
		}
		set
		{
			this.musicVol = value;
			for (int i = 0; i < this.soundList.Count; i++)
			{
				this.soundList[i].source.volume = value;
			}
		}
	}

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x06000053 RID: 83 RVA: 0x000032D9 File Offset: 0x000014D9
	// (set) Token: 0x06000054 RID: 84 RVA: 0x000032E6 File Offset: 0x000014E6
	public float bgmVol
	{
		get
		{
			return this.bgmSource.volume;
		}
		set
		{
			this.bgmSource.volume = value;
		}
	}

	// Token: 0x17000005 RID: 5
	// (get) Token: 0x06000055 RID: 85 RVA: 0x000032F4 File Offset: 0x000014F4
	// (set) Token: 0x06000056 RID: 86 RVA: 0x00003301 File Offset: 0x00001501
	public bool bgmIsMute
	{
		get
		{
			return this.bgmSource.mute;
		}
		set
		{
			this.bgmSource.mute = value;
		}
	}

	// Token: 0x06000057 RID: 87 RVA: 0x00003310 File Offset: 0x00001510
	public Sound PlayAudio(AudioClip clip, bool isLoop = false, float volume = 1f)
	{
		AudioSource audioSource = this.GetAudioSource();
		audioSource.mute = this.musicIsMute;
		audioSource.loop = isLoop;
		audioSource.volume = this.MusicVol * volume;
		Sound sound = new Sound(this, clip, audioSource, "", isLoop);
		this.soundList.Add(sound);
		return sound;
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00003364 File Offset: 0x00001564
	public void PlayAttackAudio(AttackHitSound attackHitSound)
	{
		if (attackHitSound == AttackHitSound.None)
		{
			return;
		}
		int playIndex;
		if (!this.TryConsumeAudioLimit<AttackHitSound>(this.lastAttackSoundTimes, attackHitSound, 0.05f, ref this.lastAttackSoundFrame, ref this.attackSoundFrameCount, 3, out playIndex))
		{
			return;
		}
		float stackVolume = this.GetStackVolume(playIndex);
		switch (attackHitSound)
		{
		case AttackHitSound.Sword:
			this.PlayAudio(PathDefine.Concat("Audio/Battle_Audio/Attack/pike_start_", Random.Range(1, 5)), stackVolume, 3f);
			return;
		case AttackHitSound.Blunt:
			this.PlayAudio(PathDefine.Concat("Audio/Battle_Audio/Attack/pike_start_", Random.Range(1, 5)), stackVolume, 3f);
			return;
		case AttackHitSound.Spear:
			this.PlayAudio(PathDefine.Concat("Audio/Battle_Audio/Attack/pike_start_", Random.Range(1, 5)), stackVolume, 3f);
			return;
		case AttackHitSound.Gun:
			this.PlayAudio(PathDefine.Concat("Audio/Battle_Audio/Attack/gun_start_0", Random.Range(1, 4)), stackVolume, 3f);
			return;
		case AttackHitSound.Great:
			this.PlayAudio(PathDefine.Concat("Audio/Battle_Audio/Attack/upper_0", Random.Range(1, 3)), stackVolume, 3f);
			return;
		case AttackHitSound.Slash:
			this.PlayAudio(PathDefine.Concat("Audio/Battle_Audio/Attack/upper_0", Random.Range(1, 3)), stackVolume, 3f);
			return;
		case AttackHitSound.Staff:
			this.PlayAudio(PathDefine.Concat("Audio/Battle_Audio/Attack/staff_0", Random.Range(1, 4)), stackVolume, 3f);
			return;
		case AttackHitSound.Punch:
			this.PlayAudio(PathDefine.Concat("Audio/Battle_Audio/Attack/punch_0", Random.Range(1, 3)), stackVolume, 3f);
			return;
		case AttackHitSound.Laser:
			this.PlayAudio(PathDefine.Concat("Audio/Battle_Audio/Attack/laserguna_0", Random.Range(1, 3)), stackVolume, 3f);
			return;
		default:
			return;
		}
	}

	// Token: 0x06000059 RID: 89 RVA: 0x00003510 File Offset: 0x00001710
	public void PlayDeadAudio(DeadSound deadSound, Vector3 deadPos)
	{
		if (deadSound == DeadSound.None)
		{
			return;
		}
		float num;
		if (!this.TryGetDistanceVolume(deadPos, out num))
		{
			return;
		}
		float num2 = num;
		if (deadSound != DeadSound.Boss)
		{
			int playIndex;
			if (!this.TryConsumeAudioLimit<DeadSound>(this.lastDeadSoundTimes, deadSound, 0.05f, ref this.lastDeadSoundFrame, ref this.deadSoundFrameCount, 2, out playIndex))
			{
				return;
			}
			num2 *= this.GetStackVolume(playIndex);
		}
		switch (deadSound)
		{
		case DeadSound.Goblin:
			this.PlayAudio(PathDefine.Concat("Audio/Battle_Audio/Dead/gbn_die_0", Random.Range(1, 7)), num2, 3f);
			return;
		case DeadSound.Boss:
			this.PlayAudio(PathDefine.Concat("Audio/Battle_Audio/Dead/golem_die_0", Random.Range(1, 3)), num2, 3f);
			return;
		case DeadSound.Man:
			this.PlayAudio(PathDefine.Concat("Audio/Battle_Audio/Dead/man_die_0", Random.Range(1, 4)), num2, 3f);
			return;
		case DeadSound.Woman:
			this.PlayAudio(PathDefine.Concat("Audio/Battle_Audio/Dead/woman_die_0", Random.Range(1, 4)), num2, 3f);
			return;
		default:
			return;
		}
	}

	// Token: 0x0600005A RID: 90 RVA: 0x0000360C File Offset: 0x0000180C
	public void PlayHitAudio(RoleType roleType, Vector3 attackPos)
	{
		float num;
		if (!this.TryGetDistanceVolume(attackPos, out num))
		{
			return;
		}
		int playIndex;
		if (!this.TryConsumeAudioLimit<RoleType>(this.lastHitSoundTimes, roleType, 0.03f, ref this.lastHitSoundFrame, ref this.hitSoundFrameCount, 3, out playIndex))
		{
			return;
		}
		float volumeValue = num * this.GetStackVolume(playIndex);
		if (roleType == RoleType.Player)
		{
			this.PlayAudio(PathDefine.Concat("Audio/Battle_Audio/HitHero/SwordOnMetal_0", Random.Range(1, 6)), volumeValue, 3f);
			return;
		}
		this.PlayAudio(PathDefine.Concat("Audio/Battle_Audio/Hit/SwordOnFlesh_", Random.Range(0, 7)), volumeValue, 3f);
	}

	// Token: 0x0600005B RID: 91 RVA: 0x0000369D File Offset: 0x0000189D
	private bool TryConsumeFrameBudget(ref int lastFrame, ref int frameCount, int maxCount, out int playIndex)
	{
		if (lastFrame != Time.frameCount)
		{
			lastFrame = Time.frameCount;
			frameCount = 0;
		}
		playIndex = frameCount;
		if (frameCount >= maxCount)
		{
			return false;
		}
		frameCount++;
		return true;
	}

	// Token: 0x0600005C RID: 92 RVA: 0x000036C8 File Offset: 0x000018C8
	private bool TryConsumeAudioLimit<T>(Dictionary<T, float> lastTimes, T key, float cooldown, ref int lastFrame, ref int frameCount, int maxCount, out int playIndex)
	{
		playIndex = 0;
		float time = Time.time;
		float num;
		if (lastTimes.TryGetValue(key, out num) && time - num < cooldown)
		{
			return false;
		}
		if (!this.TryConsumeFrameBudget(ref lastFrame, ref frameCount, maxCount, out playIndex))
		{
			return false;
		}
		lastTimes[key] = time;
		return true;
	}

	// Token: 0x0600005D RID: 93 RVA: 0x00003710 File Offset: 0x00001910
	private bool TryGetDistanceVolume(Vector3 soundPos, out float volume)
	{
		volume = 0f;
		if (GameHelperClient.localPlayer == null)
		{
			return false;
		}
		float v2Distance = Util.GetV2Distance(soundPos, GameHelperClient.localPlayer.MyTransform.position);
		if (v2Distance > 20f)
		{
			return false;
		}
		float t = Mathf.Clamp01(v2Distance / 20f);
		volume = Mathf.Lerp(1f, 0.5f, t);
		return true;
	}

	// Token: 0x0600005E RID: 94 RVA: 0x00003773 File Offset: 0x00001973
	private float GetStackVolume(int playIndex)
	{
		if (playIndex <= 0)
		{
			return 1f;
		}
		if (playIndex == 1)
		{
			return 0.65f;
		}
		return 0.4f;
	}

	// Token: 0x0600005F RID: 95 RVA: 0x00003790 File Offset: 0x00001990
	public void PlayDropAudio(Vector3 attackPos)
	{
		if (GameHelperClient.localPlayer == null)
		{
			return;
		}
		if (Util.GetV2Distance(attackPos, GameHelperClient.localPlayer.MyTransform.position) > 20f)
		{
			return;
		}
		this.PlayAudio("Audio/Battle_Audio/Game/掉落物品", 1f, 3f);
	}

	// Token: 0x06000060 RID: 96 RVA: 0x000037E0 File Offset: 0x000019E0
	public void PlaySkillAudio(SkillSoundType skillSoundType, Vector3 attackPos)
	{
		if (GameHelperClient.localPlayer == null)
		{
			return;
		}
		if (Util.GetV2Distance(attackPos, GameHelperClient.localPlayer.MyTransform.position) > 20f)
		{
			return;
		}
		if (skillSoundType == SkillSoundType.Role)
		{
			this.PlayAudio("Audio/Battle_Audio/Skill/RoleSkillCast", 1f, 3f);
			return;
		}
		if (skillSoundType != SkillSoundType.Boss)
		{
			return;
		}
		this.PlayAudio(PathDefine.Concat("Audio/Battle_Audio/Enemy/BOSS_Skill_0", Random.Range(1, 4)), 1f, 3f);
	}

	// Token: 0x06000061 RID: 97 RVA: 0x00003860 File Offset: 0x00001A60
	public void PlayAudioOne(string musicPath)
	{
		if (string.IsNullOrEmpty(musicPath))
		{
			return;
		}
		if (!this.soundDic.ContainsKey(musicPath))
		{
			this.soundDic.Add(musicPath, this.PlayAudio(musicPath, 1f, 3f));
		}
		this.soundDic[musicPath].Play();
	}

	// Token: 0x06000062 RID: 98 RVA: 0x000038B4 File Offset: 0x00001AB4
	public Sound PlayAudio(string musicPath, float volumeValue = 1f, float lifeTime = 3f)
	{
		if (string.IsNullOrEmpty(musicPath))
		{
			return null;
		}
		AudioClip audioClip = this.LoadAudio(musicPath);
		if (audioClip == null)
		{
			Debug.LogError("该音频不存在" + musicPath);
		}
		AudioSource audioSource = this.GetAudioSource();
		audioSource.mute = this.musicIsMute;
		audioSource.volume = this.MusicVol * volumeValue;
		Sound sound = new Sound(this, audioClip, audioSource, musicPath, false);
		sound.lifeTime = lifeTime;
		this.soundList.Add(sound);
		return sound;
	}

	// Token: 0x06000063 RID: 99 RVA: 0x0000392C File Offset: 0x00001B2C
	public void PlayAudioByPos(string musicPath, Vector3 pos, float volumeValue = 1f)
	{
		if (Util.GetV2Distance(GameHelperClient.localPlayer.MyTransform.position, pos) < 20f)
		{
			this.PlayAudio(musicPath, volumeValue, 3f);
		}
	}

	// Token: 0x06000064 RID: 100 RVA: 0x00003958 File Offset: 0x00001B58
	public void Update()
	{
		for (int i = this.soundList.Count - 1; i >= 0; i--)
		{
			Sound sound = this.soundList[i];
			sound.Update();
			if (!sound.loop)
			{
				sound.lifeTime -= Time.deltaTime;
				if (sound.lifeTime <= 0f && sound.source != null)
				{
					sound.Finish();
				}
			}
			if (sound.source == null)
			{
				this.soundList.RemoveAt(i);
				if (!string.IsNullOrEmpty(sound.path) && this.soundDic.ContainsKey(sound.path))
				{
					this.soundDic.Remove(sound.path);
				}
			}
		}
	}

	// Token: 0x06000065 RID: 101 RVA: 0x00003A1D File Offset: 0x00001C1D
	public void PlayBGM(AudioClip clip, bool isLoop)
	{
		this.bgmSource.clip = clip;
		this.bgmSource.loop = isLoop;
		this.bgmSource.Play();
	}

	// Token: 0x06000066 RID: 102 RVA: 0x00003A44 File Offset: 0x00001C44
	public void PlayBGM(string musicPath, bool isLoop)
	{
		AudioClip audioClip = this.LoadAudio(musicPath);
		if (this.bgmName.Equals(musicPath))
		{
			return;
		}
		if (audioClip == null)
		{
			Debug.LogError("音乐路径无法加载");
		}
		this.bgmSource.clip = audioClip;
		this.bgmSource.loop = isLoop;
		this.bgmSource.Play();
		this.bgmName = musicPath;
	}

	// Token: 0x06000067 RID: 103 RVA: 0x00003AA8 File Offset: 0x00001CA8
	public void PauseBGM()
	{
		if (this.bgmSource.clip == null)
		{
			Debug.LogWarning("没有设置背景音乐");
			return;
		}
		if (this.bgmSource.isPlaying)
		{
			this.bgmSource.Pause();
			return;
		}
		this.bgmSource.UnPause();
	}

	// Token: 0x06000068 RID: 104 RVA: 0x00003AF7 File Offset: 0x00001CF7
	public void StopBGM()
	{
		this.bgmSource.Stop();
	}

	// Token: 0x06000069 RID: 105 RVA: 0x00003B04 File Offset: 0x00001D04
	private AudioClip LoadAudio(string path)
	{
		return Resources.Load<AudioClip>(path);
	}

	// Token: 0x0600006A RID: 106 RVA: 0x00003B0C File Offset: 0x00001D0C
	private AudioSource GetAudioSource()
	{
		AudioSource audioSource;
		if (this.audioSourcePool.Count == 0)
		{
			audioSource = this.gameObject.AddComponent<AudioSource>();
			audioSource.playOnAwake = false;
		}
		else
		{
			audioSource = this.audioSourcePool[0];
			this.audioSourcePool.Remove(audioSource);
		}
		audioSource.enabled = true;
		return audioSource;
	}

	// Token: 0x0600006B RID: 107 RVA: 0x00003B5D File Offset: 0x00001D5D
	public void ReleaseAudioSource(AudioSource audio)
	{
		audio.enabled = false;
		this.audioSourcePool.Add(audio);
	}

	// Token: 0x0600006C RID: 108 RVA: 0x00003B72 File Offset: 0x00001D72
	public void OnApplicationQuit()
	{
		Object.Destroy(this.gameObject);
	}

	// Token: 0x0400004F RID: 79
	private const int MaxAttackSoundPerFrame = 3;

	// Token: 0x04000050 RID: 80
	private const int MaxHitSoundPerFrame = 3;

	// Token: 0x04000051 RID: 81
	private const int MaxDeadSoundPerFrame = 2;

	// Token: 0x04000052 RID: 82
	private const float AttackSoundCooldown = 0.05f;

	// Token: 0x04000053 RID: 83
	private const float HitSoundCooldown = 0.03f;

	// Token: 0x04000054 RID: 84
	private const float DeadSoundCooldown = 0.05f;

	// Token: 0x04000055 RID: 85
	private GameObject gameObject;

	// Token: 0x04000056 RID: 86
	private List<AudioSource> audioSourcePool = new List<AudioSource>(16);

	// Token: 0x04000057 RID: 87
	private Dictionary<AttackHitSound, float> lastAttackSoundTimes = new Dictionary<AttackHitSound, float>();

	// Token: 0x04000058 RID: 88
	private Dictionary<RoleType, float> lastHitSoundTimes = new Dictionary<RoleType, float>();

	// Token: 0x04000059 RID: 89
	private Dictionary<DeadSound, float> lastDeadSoundTimes = new Dictionary<DeadSound, float>();

	// Token: 0x0400005A RID: 90
	private int lastAttackSoundFrame = -1;

	// Token: 0x0400005B RID: 91
	private int attackSoundFrameCount;

	// Token: 0x0400005C RID: 92
	private int lastHitSoundFrame = -1;

	// Token: 0x0400005D RID: 93
	private int hitSoundFrameCount;

	// Token: 0x0400005E RID: 94
	private int lastDeadSoundFrame = -1;

	// Token: 0x0400005F RID: 95
	private int deadSoundFrameCount;

	// Token: 0x04000060 RID: 96
	private float musicVol = 0.5f;

	// Token: 0x04000061 RID: 97
	public bool musicIsMute;

	// Token: 0x04000062 RID: 98
	public List<Sound> soundList = new List<Sound>();

	// Token: 0x04000063 RID: 99
	private Dictionary<string, Sound> soundDic = new Dictionary<string, Sound>();

	// Token: 0x04000064 RID: 100
	private AudioSource bgmSource;

	// Token: 0x04000065 RID: 101
	private string bgmName = "";
}
