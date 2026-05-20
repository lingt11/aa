using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

// Token: 0x0200006B RID: 107
public class GameControls : IInputActionCollection2, IInputActionCollection, IEnumerable<InputAction>, IEnumerable, IDisposable
{
	// Token: 0x17000024 RID: 36
	// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000B56D File Offset: 0x0000976D
	public InputActionAsset asset { get; }

	// Token: 0x060001F4 RID: 500 RVA: 0x0000B578 File Offset: 0x00009778
	public GameControls()
	{
		this.asset = InputActionAsset.FromJson("{\r\n    \"version\": 1,\r\n    \"name\": \"RebindUISampleActions\",\r\n    \"maps\": [\r\n        {\r\n            \"name\": \"Gameplay\",\r\n            \"id\": \"8e11a806-753d-41d8-af38-51876e9ea8b1\",\r\n            \"actions\": [\r\n                {\r\n                    \"name\": \"Move\",\r\n                    \"type\": \"Value\",\r\n                    \"id\": \"9d8fcbff-87d1-43ef-857e-931c84d5bd72\",\r\n                    \"expectedControlType\": \"Vector2\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": true\r\n                },\r\n                {\r\n                    \"name\": \"Skill1\",\r\n                    \"type\": \"Button\",\r\n                    \"id\": \"0573b16b-00e3-4a89-afe4-75068ec4a78b\",\r\n                    \"expectedControlType\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": false\r\n                },\r\n                {\r\n                    \"name\": \"Skill2\",\r\n                    \"type\": \"Button\",\r\n                    \"id\": \"3dd6f8ac-4dc3-41fe-80bf-8c7c443e98e2\",\r\n                    \"expectedControlType\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": false\r\n                },\r\n                {\r\n                    \"name\": \"Skill3\",\r\n                    \"type\": \"Button\",\r\n                    \"id\": \"3fafa6aa-76aa-4af8-a9df-4a1a29aa45ce\",\r\n                    \"expectedControlType\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": false\r\n                },\r\n                {\r\n                    \"name\": \"Skill4\",\r\n                    \"type\": \"Button\",\r\n                    \"id\": \"849d9b8e-2345-418e-8346-44a06a45b422\",\r\n                    \"expectedControlType\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": false\r\n                },\r\n                {\r\n                    \"name\": \"Skill5\",\r\n                    \"type\": \"Button\",\r\n                    \"id\": \"b9a627a1-7748-4317-bf92-bac31de7495a\",\r\n                    \"expectedControlType\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": false\r\n                },\r\n                {\r\n                    \"name\": \"Pick\",\r\n                    \"type\": \"Button\",\r\n                    \"id\": \"f15491ee-66d9-4f8c-b130-473503539f3d\",\r\n                    \"expectedControlType\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": false\r\n                },\r\n                {\r\n                    \"name\": \"PickAll\",\r\n                    \"type\": \"Button\",\r\n                    \"id\": \"7d661972-39a3-4707-a0e1-573899f66f1b\",\r\n                    \"expectedControlType\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": false\r\n                },\r\n                {\r\n                    \"name\": \"Teleport1\",\r\n                    \"type\": \"Button\",\r\n                    \"id\": \"b0a60c63-18eb-45a7-851e-70d0336f9e18\",\r\n                    \"expectedControlType\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": false\r\n                },\r\n                {\r\n                    \"name\": \"Teleport2\",\r\n                    \"type\": \"Button\",\r\n                    \"id\": \"e130dfd3-faae-4e05-8847-958eb7f9e182\",\r\n                    \"expectedControlType\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": false\r\n                },\r\n                {\r\n                    \"name\": \"Teleport3\",\r\n                    \"type\": \"Button\",\r\n                    \"id\": \"8e23be47-40e2-4b13-82ce-dff6ffdde3e5\",\r\n                    \"expectedControlType\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": false\r\n                },\r\n                {\r\n                    \"name\": \"Teleport4\",\r\n                    \"type\": \"Button\",\r\n                    \"id\": \"124ff827-0faa-41e6-b184-72ec034f0664\",\r\n                    \"expectedControlType\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": false\r\n                }\r\n            ],\r\n            \"bindings\": [\r\n                {\r\n                    \"name\": \"WASD\",\r\n                    \"id\": \"076f1159-4d00-4240-9bb2-d48719a9446e\",\r\n                    \"path\": \"2DVector\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"\",\r\n                    \"action\": \"Move\",\r\n                    \"isComposite\": true,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"up\",\r\n                    \"id\": \"b560f8cd-3a2a-425f-b404-daa491858c5d\",\r\n                    \"path\": \"<Keyboard>/w\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"Keyboard\",\r\n                    \"action\": \"Move\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": true\r\n                },\r\n                {\r\n                    \"name\": \"down\",\r\n                    \"id\": \"f79a6ed1-9a19-4fd2-b37f-50052393b62e\",\r\n                    \"path\": \"<Keyboard>/s\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"Keyboard\",\r\n                    \"action\": \"Move\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": true\r\n                },\r\n                {\r\n                    \"name\": \"left\",\r\n                    \"id\": \"3f392b14-78a0-49d0-b99e-e1aa0dd94b15\",\r\n                    \"path\": \"<Keyboard>/a\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"Keyboard\",\r\n                    \"action\": \"Move\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": true\r\n                },\r\n                {\r\n                    \"name\": \"right\",\r\n                    \"id\": \"7688f62b-0428-4d34-933e-b0b7fc783faf\",\r\n                    \"path\": \"<Keyboard>/d\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"Keyboard\",\r\n                    \"action\": \"Move\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": true\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"fe58f6b8-88b0-42a8-86a4-99f59376ac5c\",\r\n                    \"path\": \"<Gamepad>/leftStick\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"Gamepad\",\r\n                    \"action\": \"Move\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"904d9c29-c1ab-47f3-ad41-72ab9b9503a6\",\r\n                    \"path\": \"<Keyboard>/q\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \";Keyboard\",\r\n                    \"action\": \"Skill1\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"de7734e2-736f-4f8b-a307-3d20b16145f0\",\r\n                    \"path\": \"<Keyboard>/e\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \";Keyboard\",\r\n                    \"action\": \"Skill2\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"4a783857-5fb5-41de-8658-771d985700ef\",\r\n                    \"path\": \"<Keyboard>/r\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \";Keyboard\",\r\n                    \"action\": \"Skill3\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"06f534bc-4ec5-4234-a1f5-f9deb3ec7aa3\",\r\n                    \"path\": \"<Keyboard>/t\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \";Keyboard\",\r\n                    \"action\": \"Skill4\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"165f1d06-8e6f-40a4-998b-ffcbd7eef578\",\r\n                    \"path\": \"<Keyboard>/1\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \";Keyboard\",\r\n                    \"action\": \"Skill5\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"4dcdbd67-8af9-4d9e-873c-020ebc7bdbb6\",\r\n                    \"path\": \"<Keyboard>/f\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \";Keyboard\",\r\n                    \"action\": \"Pick\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"6490cc2f-5278-4f1d-818b-3a1aefcb75d8\",\r\n                    \"path\": \"<Keyboard>/c\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \";Keyboard\",\r\n                    \"action\": \"PickAll\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"e25a870d-fbb5-4187-bb78-edf54840d19b\",\r\n                    \"path\": \"<Keyboard>/f1\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \";Keyboard\",\r\n                    \"action\": \"Teleport1\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"10034d79-5c76-44f3-b006-107abd0416a2\",\r\n                    \"path\": \"<Keyboard>/f2\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \";Keyboard\",\r\n                    \"action\": \"Teleport2\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"41713ef9-c48b-4adc-a639-6c31775fdb8b\",\r\n                    \"path\": \"<Keyboard>/f3\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \";Keyboard\",\r\n                    \"action\": \"Teleport3\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"21219ccc-8bae-4118-acb9-3f7cbf4c3ea5\",\r\n                    \"path\": \"<Keyboard>/f4\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \";Keyboard\",\r\n                    \"action\": \"Teleport4\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                }\r\n            ]\r\n        }\r\n    ],\r\n    \"controlSchemes\": [\r\n        {\r\n            \"name\": \"Gamepad\",\r\n            \"bindingGroup\": \"Gamepad\",\r\n            \"devices\": [\r\n                {\r\n                    \"devicePath\": \"<Gamepad>\",\r\n                    \"isOptional\": false,\r\n                    \"isOR\": false\r\n                }\r\n            ]\r\n        },\r\n        {\r\n            \"name\": \"Keyboard\",\r\n            \"bindingGroup\": \"Keyboard\",\r\n            \"devices\": [\r\n                {\r\n                    \"devicePath\": \"<Keyboard>\",\r\n                    \"isOptional\": false,\r\n                    \"isOR\": false\r\n                },\r\n                {\r\n                    \"devicePath\": \"<Mouse>\",\r\n                    \"isOptional\": false,\r\n                    \"isOR\": false\r\n                }\r\n            ]\r\n        }\r\n    ]\r\n}");
		this.m_Gameplay = this.asset.FindActionMap("Gameplay", true);
		this.m_Gameplay_Move = this.m_Gameplay.FindAction("Move", true);
		this.m_Gameplay_Skill1 = this.m_Gameplay.FindAction("Skill1", true);
		this.m_Gameplay_Skill2 = this.m_Gameplay.FindAction("Skill2", true);
		this.m_Gameplay_Skill3 = this.m_Gameplay.FindAction("Skill3", true);
		this.m_Gameplay_Skill4 = this.m_Gameplay.FindAction("Skill4", true);
		this.m_Gameplay_Skill5 = this.m_Gameplay.FindAction("Skill5", true);
		this.m_Gameplay_Pick = this.m_Gameplay.FindAction("Pick", true);
		this.m_Gameplay_PickAll = this.m_Gameplay.FindAction("PickAll", true);
		this.m_Gameplay_Teleport1 = this.m_Gameplay.FindAction("Teleport1", true);
		this.m_Gameplay_Teleport2 = this.m_Gameplay.FindAction("Teleport2", true);
		this.m_Gameplay_Teleport3 = this.m_Gameplay.FindAction("Teleport3", true);
		this.m_Gameplay_Teleport4 = this.m_Gameplay.FindAction("Teleport4", true);
	}

	// Token: 0x060001F5 RID: 501 RVA: 0x0000B6E0 File Offset: 0x000098E0
	~GameControls()
	{
	}

	// Token: 0x060001F6 RID: 502 RVA: 0x0000B708 File Offset: 0x00009908
	public void Dispose()
	{
		Object.Destroy(this.asset);
	}

	// Token: 0x17000025 RID: 37
	// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000B715 File Offset: 0x00009915
	// (set) Token: 0x060001F8 RID: 504 RVA: 0x0000B722 File Offset: 0x00009922
	public InputBinding? bindingMask
	{
		get
		{
			return this.asset.bindingMask;
		}
		set
		{
			this.asset.bindingMask = value;
		}
	}

	// Token: 0x17000026 RID: 38
	// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000B730 File Offset: 0x00009930
	// (set) Token: 0x060001FA RID: 506 RVA: 0x0000B73D File Offset: 0x0000993D
	public ReadOnlyArray<InputDevice>? devices
	{
		get
		{
			return this.asset.devices;
		}
		set
		{
			this.asset.devices = value;
		}
	}

	// Token: 0x17000027 RID: 39
	// (get) Token: 0x060001FB RID: 507 RVA: 0x0000B74B File Offset: 0x0000994B
	public ReadOnlyArray<InputControlScheme> controlSchemes
	{
		get
		{
			return this.asset.controlSchemes;
		}
	}

	// Token: 0x060001FC RID: 508 RVA: 0x0000B758 File Offset: 0x00009958
	public bool Contains(InputAction action)
	{
		return this.asset.Contains(action);
	}

	// Token: 0x060001FD RID: 509 RVA: 0x0000B766 File Offset: 0x00009966
	public IEnumerator<InputAction> GetEnumerator()
	{
		return this.asset.GetEnumerator();
	}

	// Token: 0x060001FE RID: 510 RVA: 0x0000B773 File Offset: 0x00009973
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.GetEnumerator();
	}

	// Token: 0x060001FF RID: 511 RVA: 0x0000B77B File Offset: 0x0000997B
	public void Enable()
	{
		this.asset.Enable();
	}

	// Token: 0x06000200 RID: 512 RVA: 0x0000B788 File Offset: 0x00009988
	public void Disable()
	{
		this.asset.Disable();
	}

	// Token: 0x17000028 RID: 40
	// (get) Token: 0x06000201 RID: 513 RVA: 0x0000B795 File Offset: 0x00009995
	public IEnumerable<InputBinding> bindings
	{
		get
		{
			return this.asset.bindings;
		}
	}

	// Token: 0x06000202 RID: 514 RVA: 0x0000B7A2 File Offset: 0x000099A2
	public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
	{
		return this.asset.FindAction(actionNameOrId, throwIfNotFound);
	}

	// Token: 0x06000203 RID: 515 RVA: 0x0000B7B1 File Offset: 0x000099B1
	public int FindBinding(InputBinding bindingMask, out InputAction action)
	{
		return this.asset.FindBinding(bindingMask, out action);
	}

	// Token: 0x17000029 RID: 41
	// (get) Token: 0x06000204 RID: 516 RVA: 0x0000B7C0 File Offset: 0x000099C0
	public GameControls.GameplayActions Gameplay
	{
		get
		{
			return new GameControls.GameplayActions(this);
		}
	}

	// Token: 0x1700002A RID: 42
	// (get) Token: 0x06000205 RID: 517 RVA: 0x0000B7C8 File Offset: 0x000099C8
	public InputControlScheme GamepadScheme
	{
		get
		{
			if (this.m_GamepadSchemeIndex == -1)
			{
				this.m_GamepadSchemeIndex = this.asset.FindControlSchemeIndex("Gamepad");
			}
			return this.asset.controlSchemes[this.m_GamepadSchemeIndex];
		}
	}

	// Token: 0x1700002B RID: 43
	// (get) Token: 0x06000206 RID: 518 RVA: 0x0000B810 File Offset: 0x00009A10
	public InputControlScheme KeyboardScheme
	{
		get
		{
			if (this.m_KeyboardSchemeIndex == -1)
			{
				this.m_KeyboardSchemeIndex = this.asset.FindControlSchemeIndex("Keyboard");
			}
			return this.asset.controlSchemes[this.m_KeyboardSchemeIndex];
		}
	}

	// Token: 0x04000231 RID: 561
	private readonly InputActionMap m_Gameplay;

	// Token: 0x04000232 RID: 562
	private List<GameControls.IGameplayActions> m_GameplayActionsCallbackInterfaces = new List<GameControls.IGameplayActions>();

	// Token: 0x04000233 RID: 563
	private readonly InputAction m_Gameplay_Move;

	// Token: 0x04000234 RID: 564
	private readonly InputAction m_Gameplay_Skill1;

	// Token: 0x04000235 RID: 565
	private readonly InputAction m_Gameplay_Skill2;

	// Token: 0x04000236 RID: 566
	private readonly InputAction m_Gameplay_Skill3;

	// Token: 0x04000237 RID: 567
	private readonly InputAction m_Gameplay_Skill4;

	// Token: 0x04000238 RID: 568
	private readonly InputAction m_Gameplay_Skill5;

	// Token: 0x04000239 RID: 569
	private readonly InputAction m_Gameplay_Pick;

	// Token: 0x0400023A RID: 570
	private readonly InputAction m_Gameplay_PickAll;

	// Token: 0x0400023B RID: 571
	private readonly InputAction m_Gameplay_Teleport1;

	// Token: 0x0400023C RID: 572
	private readonly InputAction m_Gameplay_Teleport2;

	// Token: 0x0400023D RID: 573
	private readonly InputAction m_Gameplay_Teleport3;

	// Token: 0x0400023E RID: 574
	private readonly InputAction m_Gameplay_Teleport4;

	// Token: 0x0400023F RID: 575
	private int m_GamepadSchemeIndex = -1;

	// Token: 0x04000240 RID: 576
	private int m_KeyboardSchemeIndex = -1;

	// Token: 0x0200006C RID: 108
	public struct GameplayActions
	{
		// Token: 0x06000207 RID: 519 RVA: 0x0000B855 File Offset: 0x00009A55
		public GameplayActions(GameControls wrapper)
		{
			this.m_Wrapper = wrapper;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000208 RID: 520 RVA: 0x0000B85E File Offset: 0x00009A5E
		public InputAction Move
		{
			get
			{
				return this.m_Wrapper.m_Gameplay_Move;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000B86B File Offset: 0x00009A6B
		public InputAction Skill1
		{
			get
			{
				return this.m_Wrapper.m_Gameplay_Skill1;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600020A RID: 522 RVA: 0x0000B878 File Offset: 0x00009A78
		public InputAction Skill2
		{
			get
			{
				return this.m_Wrapper.m_Gameplay_Skill2;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000B885 File Offset: 0x00009A85
		public InputAction Skill3
		{
			get
			{
				return this.m_Wrapper.m_Gameplay_Skill3;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600020C RID: 524 RVA: 0x0000B892 File Offset: 0x00009A92
		public InputAction Skill4
		{
			get
			{
				return this.m_Wrapper.m_Gameplay_Skill4;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000B89F File Offset: 0x00009A9F
		public InputAction Skill5
		{
			get
			{
				return this.m_Wrapper.m_Gameplay_Skill5;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000B8AC File Offset: 0x00009AAC
		public InputAction Pick
		{
			get
			{
				return this.m_Wrapper.m_Gameplay_Pick;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000B8B9 File Offset: 0x00009AB9
		public InputAction PickAll
		{
			get
			{
				return this.m_Wrapper.m_Gameplay_PickAll;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000B8C6 File Offset: 0x00009AC6
		public InputAction Teleport1
		{
			get
			{
				return this.m_Wrapper.m_Gameplay_Teleport1;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000211 RID: 529 RVA: 0x0000B8D3 File Offset: 0x00009AD3
		public InputAction Teleport2
		{
			get
			{
				return this.m_Wrapper.m_Gameplay_Teleport2;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000B8E0 File Offset: 0x00009AE0
		public InputAction Teleport3
		{
			get
			{
				return this.m_Wrapper.m_Gameplay_Teleport3;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000B8ED File Offset: 0x00009AED
		public InputAction Teleport4
		{
			get
			{
				return this.m_Wrapper.m_Gameplay_Teleport4;
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000B8FA File Offset: 0x00009AFA
		public InputActionMap Get()
		{
			return this.m_Wrapper.m_Gameplay;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000B907 File Offset: 0x00009B07
		public void Enable()
		{
			this.Get().Enable();
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000B914 File Offset: 0x00009B14
		public void Disable()
		{
			this.Get().Disable();
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000B921 File Offset: 0x00009B21
		public bool enabled
		{
			get
			{
				return this.Get().enabled;
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000B92E File Offset: 0x00009B2E
		public static implicit operator InputActionMap(GameControls.GameplayActions set)
		{
			return set.Get();
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000B938 File Offset: 0x00009B38
		public void AddCallbacks(GameControls.IGameplayActions instance)
		{
			if (instance == null || this.m_Wrapper.m_GameplayActionsCallbackInterfaces.Contains(instance))
			{
				return;
			}
			this.m_Wrapper.m_GameplayActionsCallbackInterfaces.Add(instance);
			this.Move.started += instance.OnMove;
			this.Move.performed += instance.OnMove;
			this.Move.canceled += instance.OnMove;
			this.Skill1.started += instance.OnSkill1;
			this.Skill1.performed += instance.OnSkill1;
			this.Skill1.canceled += instance.OnSkill1;
			this.Skill2.started += instance.OnSkill2;
			this.Skill2.performed += instance.OnSkill2;
			this.Skill2.canceled += instance.OnSkill2;
			this.Skill3.started += instance.OnSkill3;
			this.Skill3.performed += instance.OnSkill3;
			this.Skill3.canceled += instance.OnSkill3;
			this.Skill4.started += instance.OnSkill4;
			this.Skill4.performed += instance.OnSkill4;
			this.Skill4.canceled += instance.OnSkill4;
			this.Skill5.started += instance.OnSkill5;
			this.Skill5.performed += instance.OnSkill5;
			this.Skill5.canceled += instance.OnSkill5;
			this.Pick.started += instance.OnPick;
			this.Pick.performed += instance.OnPick;
			this.Pick.canceled += instance.OnPick;
			this.PickAll.started += instance.OnPickAll;
			this.PickAll.performed += instance.OnPickAll;
			this.PickAll.canceled += instance.OnPickAll;
			this.Teleport1.started += instance.OnTeleport1;
			this.Teleport1.performed += instance.OnTeleport1;
			this.Teleport1.canceled += instance.OnTeleport1;
			this.Teleport2.started += instance.OnTeleport2;
			this.Teleport2.performed += instance.OnTeleport2;
			this.Teleport2.canceled += instance.OnTeleport2;
			this.Teleport3.started += instance.OnTeleport3;
			this.Teleport3.performed += instance.OnTeleport3;
			this.Teleport3.canceled += instance.OnTeleport3;
			this.Teleport4.started += instance.OnTeleport4;
			this.Teleport4.performed += instance.OnTeleport4;
			this.Teleport4.canceled += instance.OnTeleport4;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000BCD0 File Offset: 0x00009ED0
		private void UnregisterCallbacks(GameControls.IGameplayActions instance)
		{
			this.Move.started -= instance.OnMove;
			this.Move.performed -= instance.OnMove;
			this.Move.canceled -= instance.OnMove;
			this.Skill1.started -= instance.OnSkill1;
			this.Skill1.performed -= instance.OnSkill1;
			this.Skill1.canceled -= instance.OnSkill1;
			this.Skill2.started -= instance.OnSkill2;
			this.Skill2.performed -= instance.OnSkill2;
			this.Skill2.canceled -= instance.OnSkill2;
			this.Skill3.started -= instance.OnSkill3;
			this.Skill3.performed -= instance.OnSkill3;
			this.Skill3.canceled -= instance.OnSkill3;
			this.Skill4.started -= instance.OnSkill4;
			this.Skill4.performed -= instance.OnSkill4;
			this.Skill4.canceled -= instance.OnSkill4;
			this.Skill5.started -= instance.OnSkill5;
			this.Skill5.performed -= instance.OnSkill5;
			this.Skill5.canceled -= instance.OnSkill5;
			this.Pick.started -= instance.OnPick;
			this.Pick.performed -= instance.OnPick;
			this.Pick.canceled -= instance.OnPick;
			this.PickAll.started -= instance.OnPickAll;
			this.PickAll.performed -= instance.OnPickAll;
			this.PickAll.canceled -= instance.OnPickAll;
			this.Teleport1.started -= instance.OnTeleport1;
			this.Teleport1.performed -= instance.OnTeleport1;
			this.Teleport1.canceled -= instance.OnTeleport1;
			this.Teleport2.started -= instance.OnTeleport2;
			this.Teleport2.performed -= instance.OnTeleport2;
			this.Teleport2.canceled -= instance.OnTeleport2;
			this.Teleport3.started -= instance.OnTeleport3;
			this.Teleport3.performed -= instance.OnTeleport3;
			this.Teleport3.canceled -= instance.OnTeleport3;
			this.Teleport4.started -= instance.OnTeleport4;
			this.Teleport4.performed -= instance.OnTeleport4;
			this.Teleport4.canceled -= instance.OnTeleport4;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000C03D File Offset: 0x0000A23D
		public void RemoveCallbacks(GameControls.IGameplayActions instance)
		{
			if (this.m_Wrapper.m_GameplayActionsCallbackInterfaces.Remove(instance))
			{
				this.UnregisterCallbacks(instance);
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000C05C File Offset: 0x0000A25C
		public void SetCallbacks(GameControls.IGameplayActions instance)
		{
			foreach (GameControls.IGameplayActions instance2 in this.m_Wrapper.m_GameplayActionsCallbackInterfaces)
			{
				this.UnregisterCallbacks(instance2);
			}
			this.m_Wrapper.m_GameplayActionsCallbackInterfaces.Clear();
			this.AddCallbacks(instance);
		}

		// Token: 0x04000241 RID: 577
		private GameControls m_Wrapper;
	}

	// Token: 0x0200006D RID: 109
	public interface IGameplayActions
	{
		// Token: 0x0600021D RID: 541
		void OnMove(InputAction.CallbackContext context);

		// Token: 0x0600021E RID: 542
		void OnSkill1(InputAction.CallbackContext context);

		// Token: 0x0600021F RID: 543
		void OnSkill2(InputAction.CallbackContext context);

		// Token: 0x06000220 RID: 544
		void OnSkill3(InputAction.CallbackContext context);

		// Token: 0x06000221 RID: 545
		void OnSkill4(InputAction.CallbackContext context);

		// Token: 0x06000222 RID: 546
		void OnSkill5(InputAction.CallbackContext context);

		// Token: 0x06000223 RID: 547
		void OnPick(InputAction.CallbackContext context);

		// Token: 0x06000224 RID: 548
		void OnPickAll(InputAction.CallbackContext context);

		// Token: 0x06000225 RID: 549
		void OnTeleport1(InputAction.CallbackContext context);

		// Token: 0x06000226 RID: 550
		void OnTeleport2(InputAction.CallbackContext context);

		// Token: 0x06000227 RID: 551
		void OnTeleport3(InputAction.CallbackContext context);

		// Token: 0x06000228 RID: 552
		void OnTeleport4(InputAction.CallbackContext context);
	}
}
