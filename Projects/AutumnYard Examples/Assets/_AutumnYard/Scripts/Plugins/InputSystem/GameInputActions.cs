//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/_Example1/Configurations/GameInputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace AutumnYard.Plugins.InputSystem
{
    public partial class @GameInputActions : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @GameInputActions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameInputActions"",
    ""maps"": [
        {
            ""name"": ""Game Player"",
            ""id"": ""ee3fa75b-602e-455f-ab3b-921c17d90a53"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""af4c9d79-22f3-416e-819d-6581f489894a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""fc5e1ade-0de9-4845-9bb3-04fb3bb8faf4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Defense"",
                    ""type"": ""Button"",
                    ""id"": ""20c06f99-ddf0-4dc9-9e1e-125650ed2661"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""2ef071ac-622d-4b7d-b8e5-2d95d7117f37"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""dddcabc3-89a2-4fb0-8217-d8c9b517c92d"",
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
                    ""id"": ""fe51a972-913f-4838-9ac2-c7a1f00937f7"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""cc45e498-6e37-4808-862b-6ebbc0cb0b8a"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""8be40490-0252-4548-98e9-d1e3d276e183"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""8700834d-7637-43e7-821a-ba1a790073da"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7f1c3e21-e8fe-4ee9-b40e-a91221ce5a98"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ab75bf07-59f8-4e2e-80b1-b12c044480da"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""Defense"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""d54a3ec5-72a8-41b7-ba32-d9138a1ab0c2"",
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
                    ""id"": ""73239483-c909-4c3a-8110-f99150c64451"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""07081c5d-d0f9-4ab7-b66c-93a095007bdd"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""18a5a922-0ad6-491a-a91a-1b41d7624dba"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""27851835-9e51-49b6-b50c-2cf282452d9e"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""30ff90b2-3cc2-4bdb-bb49-8caf9e76b402"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""472c9964-699a-4b8b-b90e-3edb40a224b6"",
            ""actions"": [
                {
                    ""name"": ""Accept"",
                    ""type"": ""Button"",
                    ""id"": ""4a7cb6c5-bc0f-4a9c-a33a-fc9269b7a22a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""848db0a2-634d-490a-adf5-a2a68bd2da54"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d122d467-b514-42a3-bd89-4831b7b1d978"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Accept"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f3c51100-9ccb-457a-bcc7-e713addfe8e0"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Accept"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""50c079ae-0117-45ba-b0e6-11335e9ba65e"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d29c2e7e-f3ae-482d-b69f-08db038bafd2"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Game Commands"",
            ""id"": ""36bf58fd-84f1-4943-bb8e-6eb6ec8a4808"",
            ""actions"": [
                {
                    ""name"": ""Open Menu Pause"",
                    ""type"": ""Button"",
                    ""id"": ""972b084a-cbc5-4346-ba4b-4279146e1f1a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Open Menu Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""8b9b7cbd-5c83-40c8-8855-52e3c99f4a3b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Open Menu Status"",
                    ""type"": ""Button"",
                    ""id"": ""6e214c4c-cc2f-452c-a923-4d433816746c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""77e88f91-b532-4c4e-a8f5-45ab6a0e0efd"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Open Menu Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c8f77337-3999-4080-b7a4-4019314a06c6"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Open Menu Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""10bd12f2-8f17-46f5-82c1-2ea2ab73578f"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Open Menu Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fe4938cf-dae3-44f2-a0ea-c3ee84004ef8"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Open Menu Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0b055e54-ad38-4209-8726-d7ebdf59e816"",
                    ""path"": ""<Keyboard>/o"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Open Menu Status"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Player1"",
            ""bindingGroup"": ""Player1"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // Game Player
            m_GamePlayer = asset.FindActionMap("Game Player", throwIfNotFound: true);
            m_GamePlayer_Move = m_GamePlayer.FindAction("Move", throwIfNotFound: true);
            m_GamePlayer_Attack = m_GamePlayer.FindAction("Attack", throwIfNotFound: true);
            m_GamePlayer_Defense = m_GamePlayer.FindAction("Defense", throwIfNotFound: true);
            m_GamePlayer_Jump = m_GamePlayer.FindAction("Jump", throwIfNotFound: true);
            // Menu
            m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
            m_Menu_Accept = m_Menu.FindAction("Accept", throwIfNotFound: true);
            m_Menu_Cancel = m_Menu.FindAction("Cancel", throwIfNotFound: true);
            // Game Commands
            m_GameCommands = asset.FindActionMap("Game Commands", throwIfNotFound: true);
            m_GameCommands_OpenMenuPause = m_GameCommands.FindAction("Open Menu Pause", throwIfNotFound: true);
            m_GameCommands_OpenMenuInventory = m_GameCommands.FindAction("Open Menu Inventory", throwIfNotFound: true);
            m_GameCommands_OpenMenuStatus = m_GameCommands.FindAction("Open Menu Status", throwIfNotFound: true);
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
        public IEnumerable<InputBinding> bindings => asset.bindings;

        public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
        {
            return asset.FindAction(actionNameOrId, throwIfNotFound);
        }
        public int FindBinding(InputBinding bindingMask, out InputAction action)
        {
            return asset.FindBinding(bindingMask, out action);
        }

        // Game Player
        private readonly InputActionMap m_GamePlayer;
        private IGamePlayerActions m_GamePlayerActionsCallbackInterface;
        private readonly InputAction m_GamePlayer_Move;
        private readonly InputAction m_GamePlayer_Attack;
        private readonly InputAction m_GamePlayer_Defense;
        private readonly InputAction m_GamePlayer_Jump;
        public struct GamePlayerActions
        {
            private @GameInputActions m_Wrapper;
            public GamePlayerActions(@GameInputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_GamePlayer_Move;
            public InputAction @Attack => m_Wrapper.m_GamePlayer_Attack;
            public InputAction @Defense => m_Wrapper.m_GamePlayer_Defense;
            public InputAction @Jump => m_Wrapper.m_GamePlayer_Jump;
            public InputActionMap Get() { return m_Wrapper.m_GamePlayer; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(GamePlayerActions set) { return set.Get(); }
            public void SetCallbacks(IGamePlayerActions instance)
            {
                if (m_Wrapper.m_GamePlayerActionsCallbackInterface != null)
                {
                    @Move.started -= m_Wrapper.m_GamePlayerActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_GamePlayerActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_GamePlayerActionsCallbackInterface.OnMove;
                    @Attack.started -= m_Wrapper.m_GamePlayerActionsCallbackInterface.OnAttack;
                    @Attack.performed -= m_Wrapper.m_GamePlayerActionsCallbackInterface.OnAttack;
                    @Attack.canceled -= m_Wrapper.m_GamePlayerActionsCallbackInterface.OnAttack;
                    @Defense.started -= m_Wrapper.m_GamePlayerActionsCallbackInterface.OnDefense;
                    @Defense.performed -= m_Wrapper.m_GamePlayerActionsCallbackInterface.OnDefense;
                    @Defense.canceled -= m_Wrapper.m_GamePlayerActionsCallbackInterface.OnDefense;
                    @Jump.started -= m_Wrapper.m_GamePlayerActionsCallbackInterface.OnJump;
                    @Jump.performed -= m_Wrapper.m_GamePlayerActionsCallbackInterface.OnJump;
                    @Jump.canceled -= m_Wrapper.m_GamePlayerActionsCallbackInterface.OnJump;
                }
                m_Wrapper.m_GamePlayerActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @Attack.started += instance.OnAttack;
                    @Attack.performed += instance.OnAttack;
                    @Attack.canceled += instance.OnAttack;
                    @Defense.started += instance.OnDefense;
                    @Defense.performed += instance.OnDefense;
                    @Defense.canceled += instance.OnDefense;
                    @Jump.started += instance.OnJump;
                    @Jump.performed += instance.OnJump;
                    @Jump.canceled += instance.OnJump;
                }
            }
        }
        public GamePlayerActions @GamePlayer => new GamePlayerActions(this);

        // Menu
        private readonly InputActionMap m_Menu;
        private IMenuActions m_MenuActionsCallbackInterface;
        private readonly InputAction m_Menu_Accept;
        private readonly InputAction m_Menu_Cancel;
        public struct MenuActions
        {
            private @GameInputActions m_Wrapper;
            public MenuActions(@GameInputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Accept => m_Wrapper.m_Menu_Accept;
            public InputAction @Cancel => m_Wrapper.m_Menu_Cancel;
            public InputActionMap Get() { return m_Wrapper.m_Menu; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
            public void SetCallbacks(IMenuActions instance)
            {
                if (m_Wrapper.m_MenuActionsCallbackInterface != null)
                {
                    @Accept.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnAccept;
                    @Accept.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnAccept;
                    @Accept.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnAccept;
                    @Cancel.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnCancel;
                    @Cancel.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnCancel;
                    @Cancel.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnCancel;
                }
                m_Wrapper.m_MenuActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Accept.started += instance.OnAccept;
                    @Accept.performed += instance.OnAccept;
                    @Accept.canceled += instance.OnAccept;
                    @Cancel.started += instance.OnCancel;
                    @Cancel.performed += instance.OnCancel;
                    @Cancel.canceled += instance.OnCancel;
                }
            }
        }
        public MenuActions @Menu => new MenuActions(this);

        // Game Commands
        private readonly InputActionMap m_GameCommands;
        private IGameCommandsActions m_GameCommandsActionsCallbackInterface;
        private readonly InputAction m_GameCommands_OpenMenuPause;
        private readonly InputAction m_GameCommands_OpenMenuInventory;
        private readonly InputAction m_GameCommands_OpenMenuStatus;
        public struct GameCommandsActions
        {
            private @GameInputActions m_Wrapper;
            public GameCommandsActions(@GameInputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @OpenMenuPause => m_Wrapper.m_GameCommands_OpenMenuPause;
            public InputAction @OpenMenuInventory => m_Wrapper.m_GameCommands_OpenMenuInventory;
            public InputAction @OpenMenuStatus => m_Wrapper.m_GameCommands_OpenMenuStatus;
            public InputActionMap Get() { return m_Wrapper.m_GameCommands; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(GameCommandsActions set) { return set.Get(); }
            public void SetCallbacks(IGameCommandsActions instance)
            {
                if (m_Wrapper.m_GameCommandsActionsCallbackInterface != null)
                {
                    @OpenMenuPause.started -= m_Wrapper.m_GameCommandsActionsCallbackInterface.OnOpenMenuPause;
                    @OpenMenuPause.performed -= m_Wrapper.m_GameCommandsActionsCallbackInterface.OnOpenMenuPause;
                    @OpenMenuPause.canceled -= m_Wrapper.m_GameCommandsActionsCallbackInterface.OnOpenMenuPause;
                    @OpenMenuInventory.started -= m_Wrapper.m_GameCommandsActionsCallbackInterface.OnOpenMenuInventory;
                    @OpenMenuInventory.performed -= m_Wrapper.m_GameCommandsActionsCallbackInterface.OnOpenMenuInventory;
                    @OpenMenuInventory.canceled -= m_Wrapper.m_GameCommandsActionsCallbackInterface.OnOpenMenuInventory;
                    @OpenMenuStatus.started -= m_Wrapper.m_GameCommandsActionsCallbackInterface.OnOpenMenuStatus;
                    @OpenMenuStatus.performed -= m_Wrapper.m_GameCommandsActionsCallbackInterface.OnOpenMenuStatus;
                    @OpenMenuStatus.canceled -= m_Wrapper.m_GameCommandsActionsCallbackInterface.OnOpenMenuStatus;
                }
                m_Wrapper.m_GameCommandsActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @OpenMenuPause.started += instance.OnOpenMenuPause;
                    @OpenMenuPause.performed += instance.OnOpenMenuPause;
                    @OpenMenuPause.canceled += instance.OnOpenMenuPause;
                    @OpenMenuInventory.started += instance.OnOpenMenuInventory;
                    @OpenMenuInventory.performed += instance.OnOpenMenuInventory;
                    @OpenMenuInventory.canceled += instance.OnOpenMenuInventory;
                    @OpenMenuStatus.started += instance.OnOpenMenuStatus;
                    @OpenMenuStatus.performed += instance.OnOpenMenuStatus;
                    @OpenMenuStatus.canceled += instance.OnOpenMenuStatus;
                }
            }
        }
        public GameCommandsActions @GameCommands => new GameCommandsActions(this);
        private int m_Player1SchemeIndex = -1;
        public InputControlScheme Player1Scheme
        {
            get
            {
                if (m_Player1SchemeIndex == -1) m_Player1SchemeIndex = asset.FindControlSchemeIndex("Player1");
                return asset.controlSchemes[m_Player1SchemeIndex];
            }
        }
        public interface IGamePlayerActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnAttack(InputAction.CallbackContext context);
            void OnDefense(InputAction.CallbackContext context);
            void OnJump(InputAction.CallbackContext context);
        }
        public interface IMenuActions
        {
            void OnAccept(InputAction.CallbackContext context);
            void OnCancel(InputAction.CallbackContext context);
        }
        public interface IGameCommandsActions
        {
            void OnOpenMenuPause(InputAction.CallbackContext context);
            void OnOpenMenuInventory(InputAction.CallbackContext context);
            void OnOpenMenuStatus(InputAction.CallbackContext context);
        }
    }
}
