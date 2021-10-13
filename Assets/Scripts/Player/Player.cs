// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Player.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Player : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Player()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Player"",
    ""maps"": [
        {
            ""name"": ""Player_Test"",
            ""id"": ""41e351db-9036-4ff9-a408-d503424d2029"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""524c6d9a-c8d8-4e0f-a077-10aaeee03d8e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""6d4383d7-8e30-4133-89d8-c3552c5a930d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""08fdc284-0ec7-4122-98a4-585d6ce939c3"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ea6b4dd2-981a-432b-8ced-393701053abc"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""c329b924-b20a-4ad5-8556-cafdec7f806d"",
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
                    ""id"": ""4b4db918-b188-4222-9f93-fae3584c4c7e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""81e347fe-9eeb-4723-a207-01b911b0976d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""61072835-2b1e-4964-bbb6-4f635f7bbad3"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""94ee17f8-dee4-40fc-8717-1a657203e5b5"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b420352e-1552-4029-8f06-250a67345418"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bd2c49db-5d5e-464e-b595-819c9a6c83b2"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f10e58f1-9862-44b5-b517-1dfe85d51a73"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player_Test
        m_Player_Test = asset.FindActionMap("Player_Test", throwIfNotFound: true);
        m_Player_Test_Move = m_Player_Test.FindAction("Move", throwIfNotFound: true);
        m_Player_Test_Jump = m_Player_Test.FindAction("Jump", throwIfNotFound: true);
        m_Player_Test_Look = m_Player_Test.FindAction("Look", throwIfNotFound: true);
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

    // Player_Test
    private readonly InputActionMap m_Player_Test;
    private IPlayer_TestActions m_Player_TestActionsCallbackInterface;
    private readonly InputAction m_Player_Test_Move;
    private readonly InputAction m_Player_Test_Jump;
    private readonly InputAction m_Player_Test_Look;
    public struct Player_TestActions
    {
        private @Player m_Wrapper;
        public Player_TestActions(@Player wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Test_Move;
        public InputAction @Jump => m_Wrapper.m_Player_Test_Jump;
        public InputAction @Look => m_Wrapper.m_Player_Test_Look;
        public InputActionMap Get() { return m_Wrapper.m_Player_Test; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player_TestActions set) { return set.Get(); }
        public void SetCallbacks(IPlayer_TestActions instance)
        {
            if (m_Wrapper.m_Player_TestActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_Player_TestActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_Player_TestActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_Player_TestActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_Player_TestActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_Player_TestActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_Player_TestActionsCallbackInterface.OnJump;
                @Look.started -= m_Wrapper.m_Player_TestActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_Player_TestActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_Player_TestActionsCallbackInterface.OnLook;
            }
            m_Wrapper.m_Player_TestActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
            }
        }
    }
    public Player_TestActions @Player_Test => new Player_TestActions(this);
    public interface IPlayer_TestActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
    }
}
