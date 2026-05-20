using System;
using UnityEngine;
using UnityEngine.InputSystem;

// Token: 0x02000143 RID: 323
public class InputManager : IUpdate
{
	// Token: 0x17000046 RID: 70
	// (get) Token: 0x06000624 RID: 1572 RVA: 0x00025649 File Offset: 0x00023849
	public InputType curInputType
	{
		get
		{
			return this.checkInput.curInputType;
		}
	}

	// Token: 0x06000625 RID: 1573 RVA: 0x00025658 File Offset: 0x00023858
	public InputManager()
	{
		this.controls = new GameControls();
		this.controls.Gameplay.Enable();
		this.LoadBindingOverrides();
		this.checkInput = new CheckInput();
		this.Clear();
		this.controls.Gameplay.Skill1.started += this.OnSkill1Down;
		this.controls.Gameplay.Skill1.canceled += this.OnSkill1Up;
		this.controls.Gameplay.Skill2.started += this.OnSkill2Down;
		this.controls.Gameplay.Skill2.canceled += this.OnSkill2Up;
		this.controls.Gameplay.Skill3.started += this.OnSkill3Down;
		this.controls.Gameplay.Skill3.canceled += this.OnSkill3Up;
		this.controls.Gameplay.Skill4.started += this.OnSkill4Down;
		this.controls.Gameplay.Skill4.canceled += this.OnSkill4Up;
		this.controls.Gameplay.Skill5.started += this.OnSkill5Down;
		this.controls.Gameplay.Skill5.canceled += this.OnSkill5Up;
		this.controls.Gameplay.Teleport1.started += this.OnTeleport1Down;
		this.controls.Gameplay.Teleport2.started += this.OnTeleport2Down;
		this.controls.Gameplay.Teleport3.started += this.OnTeleport3Down;
		this.controls.Gameplay.Teleport4.started += this.OnTeleport4Down;
		this.controls.Gameplay.Pick.started += this.OnPickItemDown;
		this.controls.Gameplay.PickAll.started += this.OnPickAllItemDown;
	}

	// Token: 0x06000626 RID: 1574 RVA: 0x000258E0 File Offset: 0x00023AE0
	public void Clear()
	{
		this.controls.Gameplay.Skill1.started -= this.OnSkill1Down;
		this.controls.Gameplay.Skill1.canceled -= this.OnSkill1Up;
		this.controls.Gameplay.Skill2.started -= this.OnSkill2Down;
		this.controls.Gameplay.Skill2.canceled -= this.OnSkill2Up;
		this.controls.Gameplay.Skill3.started -= this.OnSkill3Down;
		this.controls.Gameplay.Skill3.canceled -= this.OnSkill3Up;
		this.controls.Gameplay.Skill4.started -= this.OnSkill4Down;
		this.controls.Gameplay.Skill4.canceled -= this.OnSkill4Up;
		this.controls.Gameplay.Skill5.started -= this.OnSkill5Down;
		this.controls.Gameplay.Skill5.canceled -= this.OnSkill5Up;
		this.controls.Gameplay.Teleport1.started -= this.OnTeleport1Down;
		this.controls.Gameplay.Teleport2.started -= this.OnTeleport2Down;
		this.controls.Gameplay.Teleport3.started -= this.OnTeleport3Down;
		this.controls.Gameplay.Teleport4.started -= this.OnTeleport4Down;
		this.controls.Gameplay.Pick.started -= this.OnPickItemDown;
		this.controls.Gameplay.PickAll.started -= this.OnPickAllItemDown;
	}

	// Token: 0x06000627 RID: 1575 RVA: 0x00025B30 File Offset: 0x00023D30
	public void SaveBindingOverrides()
	{
		string value = this.controls.SaveBindingOverridesAsJson();
		PlayerPrefs.SetString("rebinds_config", value);
		PlayerPrefs.Save();
	}

	// Token: 0x06000628 RID: 1576 RVA: 0x00025B5C File Offset: 0x00023D5C
	public void LoadBindingOverrides()
	{
		if (PlayerPrefs.HasKey("rebinds_config"))
		{
			string @string = PlayerPrefs.GetString("rebinds_config");
			this.controls.LoadBindingOverridesFromJson(@string, true);
		}
	}

	// Token: 0x06000629 RID: 1577 RVA: 0x00025B8D File Offset: 0x00023D8D
	public void RestoreDefaults()
	{
		this.controls.RemoveAllBindingOverrides();
		PlayerPrefs.DeleteKey("rebinds_config");
		MySystemEvent.Instance.DispatchMessage(36);
	}

	// Token: 0x0600062A RID: 1578 RVA: 0x00025BB0 File Offset: 0x00023DB0
	public void Update()
	{
		this.checkInput.Update();
		if (GameHelperClient.localPlayer == null || GameHelperClient.IsInputChat)
		{
			return;
		}
		if (GameHelperClient.isFreeCamera)
		{
			InputManager.Horizontal = 0f;
			InputManager.Vertical = 0f;
		}
		else
		{
			Vector2 vector = this.controls.Gameplay.Move.ReadValue<Vector2>();
			InputManager.Horizontal = vector.x;
			InputManager.Vertical = vector.y;
		}
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			if (Game.UI.GetUI<UI_Shop>() == null)
			{
				Game.UI.OpenUI<UI_Shop>(null);
				return;
			}
			if (Game.UI.GetUI<UI_Shop>().isOpenShop)
			{
				Game.UI.GetUI<UI_Shop>().CloseAnim(false, true);
			}
			else
			{
				Game.UI.GetUI<UI_Shop>().OpenAnim(true);
			}
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (Game.UI.GetUI<UI_Shop>() != null && Game.UI.GetUI<UI_Shop>().isOpenShop)
			{
				Game.UI.GetUI<UI_Shop>().CloseAnim(false, true);
				return;
			}
			if (Game.UI.GetUI<UI_BattleSetting>() == null)
			{
				Game.UI.OpenUI<UI_BattleSetting>(null);
				return;
			}
			if (Game.UI.GetUI<UI_BattleSetting>().isOpenSetting)
			{
				Game.UI.GetUI<UI_BattleSetting>().CloseAnim();
				return;
			}
			Game.UI.GetUI<UI_BattleSetting>().OpenAnim();
		}
	}

	// Token: 0x0600062B RID: 1579 RVA: 0x00025CFF File Offset: 0x00023EFF
	private void OnSkill1Down(InputAction.CallbackContext ctx)
	{
		this.OnUseSkill(0);
	}

	// Token: 0x0600062C RID: 1580 RVA: 0x00025D08 File Offset: 0x00023F08
	private void OnSkill1Up(InputAction.CallbackContext ctx)
	{
		this.OnSkillUp(0);
	}

	// Token: 0x0600062D RID: 1581 RVA: 0x00025D11 File Offset: 0x00023F11
	private void OnSkill2Down(InputAction.CallbackContext ctx)
	{
		this.OnUseSkill(1);
	}

	// Token: 0x0600062E RID: 1582 RVA: 0x00025D1A File Offset: 0x00023F1A
	private void OnSkill2Up(InputAction.CallbackContext ctx)
	{
		this.OnSkillUp(1);
	}

	// Token: 0x0600062F RID: 1583 RVA: 0x00025D23 File Offset: 0x00023F23
	private void OnSkill3Down(InputAction.CallbackContext ctx)
	{
		this.OnUseSkill(2);
	}

	// Token: 0x06000630 RID: 1584 RVA: 0x00025D2C File Offset: 0x00023F2C
	private void OnSkill3Up(InputAction.CallbackContext ctx)
	{
		this.OnSkillUp(2);
	}

	// Token: 0x06000631 RID: 1585 RVA: 0x00025D35 File Offset: 0x00023F35
	private void OnSkill4Down(InputAction.CallbackContext ctx)
	{
		this.OnUseSkill(3);
	}

	// Token: 0x06000632 RID: 1586 RVA: 0x00025D3E File Offset: 0x00023F3E
	private void OnSkill4Up(InputAction.CallbackContext ctx)
	{
		this.OnSkillUp(3);
	}

	// Token: 0x06000633 RID: 1587 RVA: 0x00025D47 File Offset: 0x00023F47
	private void OnSkill5Down(InputAction.CallbackContext ctx)
	{
		this.OnUseSkill(4);
	}

	// Token: 0x06000634 RID: 1588 RVA: 0x00025D50 File Offset: 0x00023F50
	private void OnUseSkill(int index)
	{
		if (GameHelperClient.IsInputChat)
		{
			return;
		}
		if (GameHelperClient.localPlayer == null)
		{
			return;
		}
		Game.UI.GetUI<UI_PlayerState>().UseSkill(index);
	}

	// Token: 0x06000635 RID: 1589 RVA: 0x00025D78 File Offset: 0x00023F78
	private void OnSkillUp(int index)
	{
		if (GameHelperClient.IsInputChat)
		{
			return;
		}
		if (GameHelperClient.localPlayer == null)
		{
			return;
		}
		GameHelperClient.localPlayer.OnSkillKeyUp(index);
	}

	// Token: 0x06000636 RID: 1590 RVA: 0x00025D9B File Offset: 0x00023F9B
	private void OnSkill5Up(InputAction.CallbackContext ctx)
	{
		this.OnSkillUp(4);
	}

	// Token: 0x06000637 RID: 1591 RVA: 0x00025DA4 File Offset: 0x00023FA4
	private void OnTeleport1Down(InputAction.CallbackContext ctx)
	{
		this.StartCmdTeleport(1U);
	}

	// Token: 0x06000638 RID: 1592 RVA: 0x00025DAD File Offset: 0x00023FAD
	private void OnTeleport2Down(InputAction.CallbackContext ctx)
	{
		this.StartCmdTeleport(2U);
	}

	// Token: 0x06000639 RID: 1593 RVA: 0x00025DB6 File Offset: 0x00023FB6
	private void OnTeleport3Down(InputAction.CallbackContext ctx)
	{
		this.StartCmdTeleport(3U);
	}

	// Token: 0x0600063A RID: 1594 RVA: 0x00025DBF File Offset: 0x00023FBF
	private void OnTeleport4Down(InputAction.CallbackContext ctx)
	{
		this.StartCmdTeleport(4U);
	}

	// Token: 0x0600063B RID: 1595 RVA: 0x00025DC8 File Offset: 0x00023FC8
	private void StartCmdTeleport(uint index)
	{
		if (GameHelperClient.IsInputChat)
		{
			return;
		}
		if (GameHelperClient.localPlayer == null)
		{
			return;
		}
		if (GameHelperClient.localPlayer.CanAction() && Time.time > InputManager.LastTpTime)
		{
			InputManager.LastTpTime = Time.time + 0.2f;
			GameHelperClient.localPlayer.CmdTeleport(index);
		}
	}

	// Token: 0x0600063C RID: 1596 RVA: 0x00025E1E File Offset: 0x0002401E
	private void OnPickItemDown(InputAction.CallbackContext ctx)
	{
		if (GameHelperClient.IsInputChat || GameHelperClient.localPlayer == null)
		{
			return;
		}
		MySystemEvent.Instance.DispatchMessage(33);
	}

	// Token: 0x0600063D RID: 1597 RVA: 0x00025E41 File Offset: 0x00024041
	private void OnPickAllItemDown(InputAction.CallbackContext ctx)
	{
		if (GameHelperClient.IsInputChat || GameHelperClient.localPlayer == null)
		{
			return;
		}
		MySystemEvent.Instance.DispatchMessage(34);
	}

	// Token: 0x0600063E RID: 1598 RVA: 0x00025E64 File Offset: 0x00024064
	public static string GetKeyReadableName(InputAction action, int bindingIndex)
	{
		return action.GetBindingDisplayString(bindingIndex, InputBinding.DisplayStringOptions.DontUseShortDisplayNames).Replace("Alpha", "").Replace("Left Button", "LMB").Replace("Right Button", "RMB").ToUpper();
	}

	// Token: 0x0400090C RID: 2316
	public GameControls controls;

	// Token: 0x0400090D RID: 2317
	private const string RebindsKey = "rebinds_config";

	// Token: 0x0400090E RID: 2318
	public static float LastTpTime;

	// Token: 0x0400090F RID: 2319
	public static float Horizontal;

	// Token: 0x04000910 RID: 2320
	public static float Vertical;

	// Token: 0x04000911 RID: 2321
	private CheckInput checkInput;
}
