using System;
using UnityEngine;

// Token: 0x0200010D RID: 269
public class AnimDefine
{
	// Token: 0x040005C3 RID: 1475
	public static readonly int Empty = Animator.StringToHash("Empty");

	// Token: 0x040005C4 RID: 1476
	public static readonly int Idle = Animator.StringToHash("idle");

	// Token: 0x040005C5 RID: 1477
	public static readonly int Run = Animator.StringToHash("run");

	// Token: 0x040005C6 RID: 1478
	private static readonly int Attack = Animator.StringToHash("attack");

	// Token: 0x040005C7 RID: 1479
	private static readonly int Attack1 = Animator.StringToHash("attack1");

	// Token: 0x040005C8 RID: 1480
	private static readonly int Attack2 = Animator.StringToHash("attack2");

	// Token: 0x040005C9 RID: 1481
	public static readonly int Hit = Animator.StringToHash("Hit");

	// Token: 0x040005CA RID: 1482
	public static readonly int Active = Animator.StringToHash("Active");

	// Token: 0x040005CB RID: 1483
	public static readonly int[] AttackAry = new int[]
	{
		AnimDefine.Attack,
		AnimDefine.Attack1,
		AnimDefine.Attack2
	};

	// Token: 0x040005CC RID: 1484
	public static readonly int Dead = Animator.StringToHash("dead");

	// Token: 0x040005CD RID: 1485
	public static readonly int Skill = Animator.StringToHash("skill");

	// Token: 0x040005CE RID: 1486
	public static readonly int Skill2 = Animator.StringToHash("skill2");

	// Token: 0x040005CF RID: 1487
	public static readonly int Skill3 = Animator.StringToHash("skill3");

	// Token: 0x040005D0 RID: 1488
	public static readonly int Skill3_2 = Animator.StringToHash("skill3_2");

	// Token: 0x040005D1 RID: 1489
	public static readonly int Skill3_3 = Animator.StringToHash("skill3_3");

	// Token: 0x040005D2 RID: 1490
	public static readonly int Skill3End = Animator.StringToHash("skill3end");

	// Token: 0x040005D3 RID: 1491
	public static readonly int IsCheck = Animator.StringToHash("IsCheck");

	// Token: 0x040005D4 RID: 1492
	public static readonly int ShowPose = Animator.StringToHash("showpose");

	// Token: 0x040005D5 RID: 1493
	public static readonly string IdleAnimationName = "H_Idle2";

	// Token: 0x040005D6 RID: 1494
	public static readonly int LevelUp = Animator.StringToHash("LevelUp");
}
