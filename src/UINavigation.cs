using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000067 RID: 103
public class UINavigation : MonoBehaviour
{
	// Token: 0x060001DF RID: 479 RVA: 0x0000B12D File Offset: 0x0000932D
	private void Start()
	{
		if (this.uiType == UIType.Input)
		{
			this.input = base.GetComponent<InputField>();
			return;
		}
		if (this.uiType == UIType.Button)
		{
			this.button = base.GetComponent<Button>();
		}
	}

	// Token: 0x060001E0 RID: 480 RVA: 0x0000B159 File Offset: 0x00009359
	public virtual void Selected()
	{
		Action action = this.selectSelectAction;
		if (action != null)
		{
			action();
		}
		if (this.input != null)
		{
			this.input.ActivateInputField();
		}
	}

	// Token: 0x060001E1 RID: 481 RVA: 0x0000B185 File Offset: 0x00009385
	public virtual void Normal()
	{
		Action action = this.selectNormalAction;
		if (action != null)
		{
			action();
		}
		if (this.input != null)
		{
			this.input.DeactivateInputField();
		}
	}

	// Token: 0x060001E2 RID: 482 RVA: 0x0000B1B1 File Offset: 0x000093B1
	public virtual void Pressed()
	{
		Action action = this.selectPressAction;
		if (action != null)
		{
			action();
		}
		if (this.button != null)
		{
			this.button.onClick.Invoke();
		}
	}

	// Token: 0x0400021E RID: 542
	public Action selectSelectAction;

	// Token: 0x0400021F RID: 543
	public Action selectNormalAction;

	// Token: 0x04000220 RID: 544
	public Action selectPressAction;

	// Token: 0x04000221 RID: 545
	[HideInInspector]
	public Button button;

	// Token: 0x04000222 RID: 546
	[HideInInspector]
	public InputField input;

	// Token: 0x04000223 RID: 547
	public UIType uiType;
}
