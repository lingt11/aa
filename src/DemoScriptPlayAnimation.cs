using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000014 RID: 20
public class DemoScriptPlayAnimation : MonoBehaviour
{
	// Token: 0x0600003D RID: 61 RVA: 0x00002DFC File Offset: 0x00000FFC
	private void Awake()
	{
		this.animator = base.GetComponent<Animator>();
		this.startingPosition = base.transform.position;
		this.startingRotation = base.transform.rotation;
		foreach (AnimationClip animationClip in this.animator.runtimeAnimatorController.animationClips)
		{
			this.animations.Add(animationClip.name);
		}
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00002E6B File Offset: 0x0000106B
	private void Start()
	{
		this.PlayNextAnimation();
	}

	// Token: 0x0600003F RID: 63 RVA: 0x00002E73 File Offset: 0x00001073
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			this.PlayPeviousAimation();
			return;
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			this.PlayNextAnimation();
		}
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00002E9C File Offset: 0x0000109C
	public void PlayNextAnimation()
	{
		base.transform.rotation = this.startingRotation;
		base.transform.position = this.startingPosition;
		this.animationListIndex++;
		if (this.animationListIndex >= this.animations.Count)
		{
			this.animationListIndex = 0;
		}
		this.isPerformingAnimation = true;
		this.animator.Play(this.animations[this.animationListIndex]);
		this.currentAnimationName.text = this.animations[this.animationListIndex];
	}

	// Token: 0x06000041 RID: 65 RVA: 0x00002F34 File Offset: 0x00001134
	public void PlayPeviousAimation()
	{
		base.transform.rotation = this.startingRotation;
		base.transform.position = this.startingPosition;
		this.animationListIndex--;
		if (this.animationListIndex < 0)
		{
			this.animationListIndex = this.animations.Count - 1;
		}
		this.isPerformingAnimation = true;
		this.animator.Play(this.animations[this.animationListIndex]);
		this.currentAnimationName.text = this.animations[this.animationListIndex];
	}

	// Token: 0x04000040 RID: 64
	private Animator animator;

	// Token: 0x04000041 RID: 65
	private Vector3 startingPosition;

	// Token: 0x04000042 RID: 66
	private Quaternion startingRotation;

	// Token: 0x04000043 RID: 67
	[Header("Current Animation")]
	public bool isPerformingAnimation;

	// Token: 0x04000044 RID: 68
	[SerializeField]
	private string currentAnimation;

	// Token: 0x04000045 RID: 69
	[Header("Animations")]
	[SerializeField]
	private List<string> animations = new List<string>();

	// Token: 0x04000046 RID: 70
	[SerializeField]
	private Text currentAnimationName;

	// Token: 0x04000047 RID: 71
	private int animationListIndex = -1;
}
