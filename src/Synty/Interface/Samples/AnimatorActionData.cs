using System;
using UnityEngine;

namespace Synty.Interface.Samples
{
	// Token: 0x0200046E RID: 1134
	[Serializable]
	public class AnimatorActionData
	{
		// Token: 0x06001949 RID: 6473 RVA: 0x0009D000 File Offset: 0x0009B200
		public void Execute()
		{
			if (!this.animator)
			{
				return;
			}
			switch (this.type)
			{
			case AnimatorActionData.AnimatorActionType.Trigger:
				this.animator.SetTrigger(this.parameterName);
				return;
			case AnimatorActionData.AnimatorActionType.Bool:
				if (this.boolToggle)
				{
					bool @bool = this.animator.GetBool(this.parameterName);
					this.animator.SetBool(this.parameterName, !@bool);
					return;
				}
				this.animator.SetBool(this.parameterName, this.boolValue);
				return;
			case AnimatorActionData.AnimatorActionType.Float:
				this.animator.SetFloat(this.parameterName, this.floatValue);
				return;
			case AnimatorActionData.AnimatorActionType.Int:
				this.animator.SetInteger(this.parameterName, this.intValue);
				return;
			default:
				return;
			}
		}

		// Token: 0x040018B2 RID: 6322
		[Header("References")]
		public Animator animator;

		// Token: 0x040018B3 RID: 6323
		public AnimatorActionData.AnimatorActionType type;

		// Token: 0x040018B4 RID: 6324
		[Header("Parameters")]
		public string parameterName;

		// Token: 0x040018B5 RID: 6325
		public bool boolToggle;

		// Token: 0x040018B6 RID: 6326
		public bool boolValue;

		// Token: 0x040018B7 RID: 6327
		public float floatValue;

		// Token: 0x040018B8 RID: 6328
		public int intValue;

		// Token: 0x0200046F RID: 1135
		public enum AnimatorActionType
		{
			// Token: 0x040018BA RID: 6330
			Trigger,
			// Token: 0x040018BB RID: 6331
			Bool,
			// Token: 0x040018BC RID: 6332
			Float,
			// Token: 0x040018BD RID: 6333
			Int
		}
	}
}
