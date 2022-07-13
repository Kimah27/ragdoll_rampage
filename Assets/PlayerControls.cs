// GENERATED AUTOMATICALLY FROM 'Assets/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""73d3327f-26f6-4737-ac87-9dd84621564d"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""c30ec5f3-bc21-4636-bc5d-d64bd9d936dd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""f9f0994f-51b6-441c-9019-f17ed7d85cef"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SprintStart"",
                    ""type"": ""Button"",
                    ""id"": ""a0ed0ab0-171a-4827-addd-bf00d0af2271"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SprintEnd"",
                    ""type"": ""Button"",
                    ""id"": ""185d7735-48f6-4f2a-8b83-874588250e51"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""Defend"",
                    ""type"": ""Button"",
                    ""id"": ""11e7f671-9338-4377-bee9-04ac2975d78f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Basic Attack"",
                    ""type"": ""Button"",
                    ""id"": ""deaa82fe-0adf-4290-ab8a-ac2c64b20725"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Special Attack"",
                    ""type"": ""Button"",
                    ""id"": ""f77229b1-1c05-4aab-b269-3354a5d39d60"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Super Attack"",
                    ""type"": ""Button"",
                    ""id"": ""e361d7c3-4392-4dbf-bceb-b10efa248f32"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Quit"",
                    ""type"": ""Button"",
                    ""id"": ""17d0834d-a2ca-4214-bddc-81741db217fa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Leave"",
                    ""type"": ""Button"",
                    ""id"": ""c65291a0-902b-444f-b9cf-40483d853a04"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Left Arrow"",
                    ""type"": ""Button"",
                    ""id"": ""528b8f88-0883-4421-8bd5-34132b8f5d1f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Right Arrow"",
                    ""type"": ""Button"",
                    ""id"": ""da785159-ac7f-49a2-8adc-62d664d87116"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Confirm"",
                    ""type"": ""Button"",
                    ""id"": ""975c6895-62cb-4784-b76a-504d6d94ff7f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""260fcb92-2ed6-435d-91a8-e10d72868266"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Basic Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""349e02dd-7c93-4f8a-bd45-7cd3dbc9411b"",
                    ""path"": ""<Keyboard>/u"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KB"",
                    ""action"": ""Basic Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""41e11003-456e-49ab-9167-926a7a838ab5"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Special Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1dd572d9-38b6-400d-a67f-c3f75c5bd2ab"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KB"",
                    ""action"": ""Special Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b995f2d8-0a5e-4b5b-b2f9-2a5ac579e22c"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Super Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1f5bb0d9-ef37-4754-ad1a-90022417e1ae"",
                    ""path"": ""<Keyboard>/o"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KB"",
                    ""action"": ""Super Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""726f816c-ebec-4c11-a707-4b44ace9a206"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b6ceed1a-ca73-44e1-9453-e6ab407d2944"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KB"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d1d31be9-87cc-4558-b201-3d6f0c060426"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""SprintStart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fec72441-5865-4591-809c-99ce9f084c4e"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KB"",
                    ""action"": ""SprintStart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""36923255-8a5f-4177-9764-2b205b78a403"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""07f6986e-73b8-4087-aa2e-f2505a0f45cd"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""27a5db39-4f72-4ce1-a182-92ab78d1474e"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ee3a8f02-739b-4c1c-bd01-5bafbeb1ea5a"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""cf09bfa4-0a8d-4176-8c88-d5e4e649eccd"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""1c47ee0e-c0d5-4a23-a072-33e4b07c20b0"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""03009307-73c2-4c84-8361-fc810e7f3f5c"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""549bf1a1-cf71-4d5e-898e-77ebe75a0e9b"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d65943fc-82e8-4d54-b698-546856c1a7c4"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""8e12ce2a-b70a-4061-a9f9-4d21c6117534"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""286e1a65-58f8-435e-b60a-6a734192a4f7"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KB"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""baa2b0ca-5eb5-4b1a-9ee8-9c5416b8059d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KB"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""2dee73cc-386c-43e4-8538-aae3ed84b6ad"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KB"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6639cef5-fc47-47a9-ae35-8dde1a78fa83"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KB"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""26562e7e-282d-43e3-9508-7ca5e0bf5bc8"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KB;Gamepad"",
                    ""action"": ""Quit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d5c0de1f-f9f5-4dc6-bd19-6dd02cc55e8d"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Left Arrow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2e7daf00-c4de-4564-bf86-17da6c27985d"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Left Arrow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0ec7600a-51b1-48d7-840c-050506c14cc6"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KB"",
                    ""action"": ""Left Arrow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""99ae4928-8c5d-4d4b-8300-26f8b5ddc054"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Right Arrow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cc916d05-f17d-4a56-a37a-460b431ef79d"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Right Arrow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fe80b0e3-7d80-4928-8fb8-3e622e3fbfb8"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KB"",
                    ""action"": ""Right Arrow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2f830559-0a33-4ea8-a6c7-9cb3394234a9"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Confirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ca3ccd72-f383-4fdc-8911-4b9c88642e10"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Confirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9ab63aa3-3a40-454d-9831-cbfccd3650d5"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KB"",
                    ""action"": ""Confirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dca86134-ffc9-42fd-bea6-11a1be968af0"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KB"",
                    ""action"": ""Confirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c7c16d82-d944-4ec6-866f-49d0884285bb"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Defend"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b5941dda-75e1-446b-b57f-aef4b7b0f3de"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KB"",
                    ""action"": ""Defend"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""988379dc-cb34-4f59-8d4b-022525116c9d"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""SprintEnd"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aa2eaa21-074c-4470-923e-9e1b3f27a6c5"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""KB"",
                    ""action"": ""SprintEnd"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a6643f68-66c1-4871-9fc7-9b18d3a5f971"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Leave"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2342dd09-1dea-4919-b3d4-0c20e775c076"",
                    ""path"": ""<Keyboard>/backspace"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KB"",
                    ""action"": ""Leave"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KB"",
            ""bindingGroup"": ""KB"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Move = m_Gameplay.FindAction("Move", throwIfNotFound: true);
        m_Gameplay_Jump = m_Gameplay.FindAction("Jump", throwIfNotFound: true);
        m_Gameplay_SprintStart = m_Gameplay.FindAction("SprintStart", throwIfNotFound: true);
        m_Gameplay_SprintEnd = m_Gameplay.FindAction("SprintEnd", throwIfNotFound: true);
        m_Gameplay_Defend = m_Gameplay.FindAction("Defend", throwIfNotFound: true);
        m_Gameplay_BasicAttack = m_Gameplay.FindAction("Basic Attack", throwIfNotFound: true);
        m_Gameplay_SpecialAttack = m_Gameplay.FindAction("Special Attack", throwIfNotFound: true);
        m_Gameplay_SuperAttack = m_Gameplay.FindAction("Super Attack", throwIfNotFound: true);
        m_Gameplay_Quit = m_Gameplay.FindAction("Quit", throwIfNotFound: true);
        m_Gameplay_Leave = m_Gameplay.FindAction("Leave", throwIfNotFound: true);
        m_Gameplay_LeftArrow = m_Gameplay.FindAction("Left Arrow", throwIfNotFound: true);
        m_Gameplay_RightArrow = m_Gameplay.FindAction("Right Arrow", throwIfNotFound: true);
        m_Gameplay_Confirm = m_Gameplay.FindAction("Confirm", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Move;
    private readonly InputAction m_Gameplay_Jump;
    private readonly InputAction m_Gameplay_SprintStart;
    private readonly InputAction m_Gameplay_SprintEnd;
    private readonly InputAction m_Gameplay_Defend;
    private readonly InputAction m_Gameplay_BasicAttack;
    private readonly InputAction m_Gameplay_SpecialAttack;
    private readonly InputAction m_Gameplay_SuperAttack;
    private readonly InputAction m_Gameplay_Quit;
    private readonly InputAction m_Gameplay_Leave;
    private readonly InputAction m_Gameplay_LeftArrow;
    private readonly InputAction m_Gameplay_RightArrow;
    private readonly InputAction m_Gameplay_Confirm;
    public struct GameplayActions
    {
        private @PlayerControls m_Wrapper;
        public GameplayActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Gameplay_Move;
        public InputAction @Jump => m_Wrapper.m_Gameplay_Jump;
        public InputAction @SprintStart => m_Wrapper.m_Gameplay_SprintStart;
        public InputAction @SprintEnd => m_Wrapper.m_Gameplay_SprintEnd;
        public InputAction @Defend => m_Wrapper.m_Gameplay_Defend;
        public InputAction @BasicAttack => m_Wrapper.m_Gameplay_BasicAttack;
        public InputAction @SpecialAttack => m_Wrapper.m_Gameplay_SpecialAttack;
        public InputAction @SuperAttack => m_Wrapper.m_Gameplay_SuperAttack;
        public InputAction @Quit => m_Wrapper.m_Gameplay_Quit;
        public InputAction @Leave => m_Wrapper.m_Gameplay_Leave;
        public InputAction @LeftArrow => m_Wrapper.m_Gameplay_LeftArrow;
        public InputAction @RightArrow => m_Wrapper.m_Gameplay_RightArrow;
        public InputAction @Confirm => m_Wrapper.m_Gameplay_Confirm;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @SprintStart.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSprintStart;
                @SprintStart.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSprintStart;
                @SprintStart.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSprintStart;
                @SprintEnd.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSprintEnd;
                @SprintEnd.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSprintEnd;
                @SprintEnd.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSprintEnd;
                @Defend.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDefend;
                @Defend.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDefend;
                @Defend.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDefend;
                @BasicAttack.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBasicAttack;
                @BasicAttack.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBasicAttack;
                @BasicAttack.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBasicAttack;
                @SpecialAttack.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpecialAttack;
                @SpecialAttack.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpecialAttack;
                @SpecialAttack.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpecialAttack;
                @SuperAttack.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSuperAttack;
                @SuperAttack.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSuperAttack;
                @SuperAttack.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSuperAttack;
                @Quit.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnQuit;
                @Quit.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnQuit;
                @Quit.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnQuit;
                @Leave.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeave;
                @Leave.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeave;
                @Leave.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeave;
                @LeftArrow.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftArrow;
                @LeftArrow.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftArrow;
                @LeftArrow.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftArrow;
                @RightArrow.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightArrow;
                @RightArrow.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightArrow;
                @RightArrow.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightArrow;
                @Confirm.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnConfirm;
                @Confirm.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnConfirm;
                @Confirm.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnConfirm;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @SprintStart.started += instance.OnSprintStart;
                @SprintStart.performed += instance.OnSprintStart;
                @SprintStart.canceled += instance.OnSprintStart;
                @SprintEnd.started += instance.OnSprintEnd;
                @SprintEnd.performed += instance.OnSprintEnd;
                @SprintEnd.canceled += instance.OnSprintEnd;
                @Defend.started += instance.OnDefend;
                @Defend.performed += instance.OnDefend;
                @Defend.canceled += instance.OnDefend;
                @BasicAttack.started += instance.OnBasicAttack;
                @BasicAttack.performed += instance.OnBasicAttack;
                @BasicAttack.canceled += instance.OnBasicAttack;
                @SpecialAttack.started += instance.OnSpecialAttack;
                @SpecialAttack.performed += instance.OnSpecialAttack;
                @SpecialAttack.canceled += instance.OnSpecialAttack;
                @SuperAttack.started += instance.OnSuperAttack;
                @SuperAttack.performed += instance.OnSuperAttack;
                @SuperAttack.canceled += instance.OnSuperAttack;
                @Quit.started += instance.OnQuit;
                @Quit.performed += instance.OnQuit;
                @Quit.canceled += instance.OnQuit;
                @Leave.started += instance.OnLeave;
                @Leave.performed += instance.OnLeave;
                @Leave.canceled += instance.OnLeave;
                @LeftArrow.started += instance.OnLeftArrow;
                @LeftArrow.performed += instance.OnLeftArrow;
                @LeftArrow.canceled += instance.OnLeftArrow;
                @RightArrow.started += instance.OnRightArrow;
                @RightArrow.performed += instance.OnRightArrow;
                @RightArrow.canceled += instance.OnRightArrow;
                @Confirm.started += instance.OnConfirm;
                @Confirm.performed += instance.OnConfirm;
                @Confirm.canceled += instance.OnConfirm;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    private int m_KBSchemeIndex = -1;
    public InputControlScheme KBScheme
    {
        get
        {
            if (m_KBSchemeIndex == -1) m_KBSchemeIndex = asset.FindControlSchemeIndex("KB");
            return asset.controlSchemes[m_KBSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IGameplayActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSprintStart(InputAction.CallbackContext context);
        void OnSprintEnd(InputAction.CallbackContext context);
        void OnDefend(InputAction.CallbackContext context);
        void OnBasicAttack(InputAction.CallbackContext context);
        void OnSpecialAttack(InputAction.CallbackContext context);
        void OnSuperAttack(InputAction.CallbackContext context);
        void OnQuit(InputAction.CallbackContext context);
        void OnLeave(InputAction.CallbackContext context);
        void OnLeftArrow(InputAction.CallbackContext context);
        void OnRightArrow(InputAction.CallbackContext context);
        void OnConfirm(InputAction.CallbackContext context);
    }
}
