using System;
using UnityEngine;

namespace namespace_animclip_offset
{
	// Token: 0x020004BA RID: 1210
	public class anim_clip_offset : MonoBehaviour
	{
		// Token: 0x06001AD9 RID: 6873 RVA: 0x000A5E87 File Offset: 0x000A4087
		private void Awake()
		{
			this.animator = base.GetComponent<Animator>();
			this.Play_Animationclip_offset();
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x000A5E9C File Offset: 0x000A409C
		private void Play_Animationclip_offset()
		{
			AnimationClip clip = this.animator.GetCurrentAnimatorClipInfo(0)[0].clip;
			float num = Random.Range(0f, clip.length);
			int shortNameHash = this.animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
			this.animator.Play(shortNameHash, 0, num / clip.length);
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x00002D1D File Offset: 0x00000F1D
		private void Update()
		{
		}

		// Token: 0x04001A41 RID: 6721
		private Animator animator;
	}
}
