using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// Token: 0x02000374 RID: 884
public class UI_PlayerState_Joy
{
	// Token: 0x0600143F RID: 5183 RVA: 0x0007DC02 File Offset: 0x0007BE02
	public UI_PlayerState_Joy(UI_PlayerState ps)
	{
		this.playerState = ps;
	}

	// Token: 0x06001440 RID: 5184 RVA: 0x0007DC14 File Offset: 0x0007BE14
	public void Open()
	{
		MySystemEvent.Instance.RegisterMessage(1, new Action<Body>(this.JoyA));
		MySystemEvent.Instance.RegisterMessage(9, new Action<Body>(this.JoyXPressed));
		MySystemEvent.Instance.RegisterMessage(10, new Action<Body>(this.JoyXReleased));
		MySystemEvent.Instance.RegisterMessage(11, new Action<Body>(this.JoyY));
		MySystemEvent.Instance.RegisterMessage(12, new Action<Body>(this.JoyB));
		MySystemEvent.Instance.RegisterMessage(13, new Action<Body>(this.JoyRightRight));
		MySystemEvent.Instance.RegisterMessage(14, new Action<Body>(this.JoyRightLeft));
		MySystemEvent.Instance.RegisterMessage(15, new Action<Body>(this.JoyRightUp));
		MySystemEvent.Instance.RegisterMessage(16, new Action<Body>(this.JoyRightDown));
		MySystemEvent.Instance.RegisterMessage(17, new Action<Body>(this.RightTriggerPressed));
		MySystemEvent.Instance.RegisterMessage(18, new Action<Body>(this.RightTriggerReleased));
	}

	// Token: 0x06001441 RID: 5185 RVA: 0x0007DD28 File Offset: 0x0007BF28
	public void Close()
	{
		MySystemEvent.Instance.UnregisterMessage(1, new Action<Body>(this.JoyA));
		MySystemEvent.Instance.UnregisterMessage(9, new Action<Body>(this.JoyXPressed));
		MySystemEvent.Instance.UnregisterMessage(10, new Action<Body>(this.JoyXReleased));
		MySystemEvent.Instance.UnregisterMessage(11, new Action<Body>(this.JoyY));
		MySystemEvent.Instance.UnregisterMessage(12, new Action<Body>(this.JoyB));
		MySystemEvent.Instance.UnregisterMessage(13, new Action<Body>(this.JoyRightRight));
		MySystemEvent.Instance.UnregisterMessage(14, new Action<Body>(this.JoyRightLeft));
		MySystemEvent.Instance.UnregisterMessage(15, new Action<Body>(this.JoyRightUp));
		MySystemEvent.Instance.UnregisterMessage(16, new Action<Body>(this.JoyRightDown));
		MySystemEvent.Instance.UnregisterMessage(17, new Action<Body>(this.RightTriggerPressed));
		MySystemEvent.Instance.UnregisterMessage(18, new Action<Body>(this.RightTriggerReleased));
	}

	// Token: 0x06001442 RID: 5186 RVA: 0x0007DE3C File Offset: 0x0007C03C
	private void JoyXPressed(Body body)
	{
		if (this.isRightTriggerPressed)
		{
			Debug.Log("升级2");
			return;
		}
		Game.UI.GetUI<UI_PlayerState>().UseSkill(1);
	}

	// Token: 0x06001443 RID: 5187 RVA: 0x0007DE61 File Offset: 0x0007C061
	private void JoyXReleased(Body body)
	{
		GameHelperClient.localPlayer.OnSkillKeyUp(1);
	}

	// Token: 0x06001444 RID: 5188 RVA: 0x0007DE6E File Offset: 0x0007C06E
	private void JoyY(Body body)
	{
		if (this.isRightTriggerPressed)
		{
			Debug.Log("升级3");
			return;
		}
		Game.UI.GetUI<UI_PlayerState>().UseSkill(0);
	}

	// Token: 0x06001445 RID: 5189 RVA: 0x0007DE94 File Offset: 0x0007C094
	private void JoyA(Body body)
	{
		if (this.isRightTriggerPressed)
		{
			Debug.Log("升级1");
			return;
		}
		if (EntityStatic.Get<EffectManager>().isUseSkill)
		{
			EntityStatic.Get<EffectManager>().UseMouseBtnDown();
		}
		if (this.joyButtonType == JoyStateType.Bag)
		{
			this.playerState.playerStateBag.bagList[this.bagIndex].GetComponent<DraggableItem>().ButtonRight();
			this.joyButtonType = JoyStateType.BagDetail;
			return;
		}
		if (this.joyButtonType == JoyStateType.BagDetail)
		{
			this.joyButtonType = JoyStateType.None;
			Button bagDetailBtn = this.playerState.GetBagDetailBtn(this.bagDetailIndex);
			if (bagDetailBtn != null)
			{
				bagDetailBtn.Select();
			}
		}
	}

	// Token: 0x06001446 RID: 5190 RVA: 0x0007DF34 File Offset: 0x0007C134
	private void JoyB(Body body)
	{
		if (EntityStatic.Get<EffectManager>().isUseSkill)
		{
			EntityStatic.Get<EffectManager>().HideSpellGroundTip();
		}
		if (this.joyButtonType == JoyStateType.Bag)
		{
			this.joyButtonType = JoyStateType.None;
			this.playerState.playerStateBag.bagList[this.bagIndex].transform.localScale = Vector3.one;
			return;
		}
		if (this.joyButtonType == JoyStateType.BagDetail)
		{
			this.joyButtonType = JoyStateType.None;
			Button bagDetailBtn = this.playerState.GetBagDetailBtn(3);
			if (bagDetailBtn != null)
			{
				bagDetailBtn.onClick.Invoke();
			}
			this.playerState.playerStateBag.bagList[this.bagIndex].transform.localScale = Vector3.one;
		}
	}

	// Token: 0x06001447 RID: 5191 RVA: 0x0007DFF0 File Offset: 0x0007C1F0
	public void Update()
	{
		if (EntityStatic.Get<EffectManager>().isUseSkill && GameHelperClient.IsJoyStick)
		{
			Vector3 a = Gamepad.current.rightStick.ReadValue();
			Util.visualMouse += a * 10f;
		}
	}

	// Token: 0x06001448 RID: 5192 RVA: 0x0007E040 File Offset: 0x0007C240
	private void JoyRightRight(Body body)
	{
		if (this.joyButtonType == JoyStateType.BagDetail)
		{
			return;
		}
		int num = this.playerState.playerStateBag.bagList.Count - 1;
		this.playerState.playerStateBag.bagList[this.bagIndex].transform.localScale = Vector3.one;
		this.bagIndex++;
		if (this.bagIndex > num)
		{
			this.bagIndex = num;
		}
		this.playerState.playerStateBag.bagList[this.bagIndex].transform.localScale = Vector3.one * 1.2f;
		this.joyButtonType = JoyStateType.Bag;
	}

	// Token: 0x06001449 RID: 5193 RVA: 0x0007E0F4 File Offset: 0x0007C2F4
	private void JoyRightLeft(Body body)
	{
		if (this.joyButtonType == JoyStateType.BagDetail)
		{
			return;
		}
		this.playerState.playerStateBag.bagList[this.bagIndex].transform.localScale = Vector3.one;
		this.bagIndex--;
		if (this.bagIndex < 0)
		{
			this.bagIndex = 0;
		}
		this.playerState.playerStateBag.bagList[this.bagIndex].transform.localScale = Vector3.one * 1.2f;
		this.joyButtonType = JoyStateType.Bag;
	}

	// Token: 0x0600144A RID: 5194 RVA: 0x0007E190 File Offset: 0x0007C390
	private void JoyRightUp(Body body)
	{
		if (this.joyButtonType == JoyStateType.BagDetail)
		{
			this.bagDetailIndex--;
			this.BagDetailSelect();
			return;
		}
		this.playerState.playerStateBag.bagList[this.bagIndex].transform.localScale = Vector3.one;
		this.bagIndex -= 3;
		if (this.bagIndex < 0)
		{
			this.bagIndex = 0;
		}
		this.playerState.playerStateBag.bagList[this.bagIndex].transform.localScale = Vector3.one * 1.2f;
		this.joyButtonType = JoyStateType.Bag;
	}

	// Token: 0x0600144B RID: 5195 RVA: 0x0007E240 File Offset: 0x0007C440
	private void JoyRightDown(Body body)
	{
		if (this.joyButtonType == JoyStateType.BagDetail)
		{
			this.bagDetailIndex++;
			this.BagDetailSelect();
			return;
		}
		int num = this.playerState.playerStateBag.bagList.Count - 1;
		this.playerState.playerStateBag.bagList[this.bagIndex].transform.localScale = Vector3.one;
		this.bagIndex += 3;
		if (this.bagIndex > num)
		{
			this.bagIndex = num;
		}
		this.playerState.playerStateBag.bagList[this.bagIndex].transform.localScale = Vector3.one * 1.2f;
		this.joyButtonType = JoyStateType.Bag;
	}

	// Token: 0x0600144C RID: 5196 RVA: 0x0007E308 File Offset: 0x0007C508
	private void BagDetailSelect()
	{
		if (this.bagDetailIndex < 0)
		{
			this.bagDetailIndex = 0;
		}
		if (this.bagDetailIndex > 3)
		{
			this.bagDetailIndex = 3;
		}
		Debug.Log(this.bagDetailIndex);
		Button bagDetailBtn = this.playerState.GetBagDetailBtn(this.bagDetailIndex);
		if (bagDetailBtn != null)
		{
			bagDetailBtn.Select();
		}
	}

	// Token: 0x0600144D RID: 5197 RVA: 0x0007E366 File Offset: 0x0007C566
	private void RightTriggerPressed(Body body)
	{
		this.isRightTriggerPressed = true;
		Debug.Log("按下扳机");
		this.playerState.ShowEquipJoyBtn();
	}

	// Token: 0x0600144E RID: 5198 RVA: 0x0007E384 File Offset: 0x0007C584
	private void RightTriggerReleased(Body body)
	{
		this.isRightTriggerPressed = false;
		Debug.Log("抬起扳机");
		this.playerState.HideEquipJoyBtn();
	}

	// Token: 0x040012D1 RID: 4817
	public JoyStateType joyButtonType;

	// Token: 0x040012D2 RID: 4818
	private UI_PlayerState playerState;

	// Token: 0x040012D3 RID: 4819
	public int bagIndex;

	// Token: 0x040012D4 RID: 4820
	public int bagDetailIndex;

	// Token: 0x040012D5 RID: 4821
	private bool isRightTriggerPressed;
}
