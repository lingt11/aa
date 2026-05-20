using System;
using UnityEngine;

namespace Synty.Interface.FantasyWarriorHUD.Samples
{
	// Token: 0x02000458 RID: 1112
	[Serializable]
	public class SampleAnimatorActionData
	{
		// Token: 0x060018D8 RID: 6360 RVA: 0x0009BD7C File Offset: 0x00099F7C
		public void Execute()
		{
			if (!this.animator)
			{
				return;
			}
			this.animator.gameObject.SetActive(true);
			switch (this.type)
			{
			case SampleAnimatorActionData.AnimatorActionType.Trigger:
				this.animator.SetTrigger(this.parameterName);
				return;
			case SampleAnimatorActionData.AnimatorActionType.Bool:
				if (this.boolToggle)
				{
					bool @bool = this.animator.GetBool(this.parameterName);
					this.animator.SetBool(this.parameterName, !@bool);
					return;
				}
				this.animator.SetBool(this.parameterName, this.boolValue);
				return;
			case SampleAnimatorActionData.AnimatorActionType.Float:
				this.animator.SetFloat(this.parameterName, this.floatValue);
				return;
			case SampleAnimatorActionData.AnimatorActionType.Int:
				this.animator.SetInteger(this.parameterName, this.intValue);
				return;
			default:
				return;
			}
		}

		// Token: 0x04001848 RID: 6216
		[Header("References")]
		public Animator animator;

		// Token: 0x04001849 RID: 6217
		public SampleAnimatorActionData.AnimatorActionType type;

		// Token: 0x0400184A RID: 6218
		[Header("Parameters")]
		public string parameterName;

		// Token: 0x0400184B RID: 6219
		public bool boolToggle;

		// Token: 0x0400184C RID: 6220
		public bool boolValue;

		// Token: 0x0400184D RID: 6221
		public float floatValue;

		// Token: 0x0400184E RID: 6222
		public int intValue;

		// Token: 0x02000459 RID: 1113
		public enum AnimatorActionType
		{
			// Token: 0x04001850 RID: 6224
			Trigger,
			// Token: 0x04001851 RID: 6225
			Bool,
			// Token: 0x04001852 RID: 6226
			Float,
			// Token: 0x04001853 RID: 6227
			Int
		}
	}
}
