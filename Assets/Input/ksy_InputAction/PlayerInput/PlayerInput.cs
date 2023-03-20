//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Input/PlayerInput.inputactions
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

public partial class @PlayerInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""CharacterMove"",
            ""id"": ""2874ed7d-0ae9-447e-ad72-4226c2ae7ed8"",
            ""actions"": [
                {
                    ""name"": ""Activity"",
                    ""type"": ""Button"",
                    ""id"": ""a36288be-9c0e-421f-9d94-461c26a9a88e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interaction_Item"",
                    ""type"": ""Button"",
                    ""id"": ""00f3efed-a7a3-4773-8ee1-13b1b89a27d7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""b41a9af8-908b-4d07-8cc4-79d3cee7c9a7"",
                    ""expectedControlType"": ""Vector3"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Interaction_Place"",
                    ""type"": ""Button"",
                    ""id"": ""2db08ba2-3dff-4469-9786-093a5605b1b6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d492bb15-170e-4063-a1ed-bbfcda6ea9f0"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoardAndMouse"",
                    ""action"": ""Activity"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eb32017b-71f2-4cca-bb7d-8ccf8f213af3"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoardAndMouse"",
                    ""action"": ""Interaction_Item"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""3D Vector"",
                    ""id"": ""09d00463-eff9-4338-9602-aa5ed6d0131c"",
                    ""path"": ""3DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""aa78e49d-9af9-4d36-8705-0b365c74b502"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""11afa382-6347-455c-b392-e4a7abb1a97f"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""d976dfdd-a576-40ae-8a3c-af92b2c5a7ca"",
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
                    ""id"": ""5855e2ec-844e-459d-ac71-0b4532add2cd"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""forward"",
                    ""id"": ""d2a3daec-0adf-4ebc-8ea2-a1bccee74cee"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""backward"",
                    ""id"": ""28e399bd-7297-4070-b13d-caf53441cb12"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""de0a7d30-f966-4f97-a979-20294dc302c5"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interaction_Place"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyBoardAndMouse"",
            ""bindingGroup"": ""KeyBoardAndMouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // CharacterMove
        m_CharacterMove = asset.FindActionMap("CharacterMove", throwIfNotFound: true);
        m_CharacterMove_Activity = m_CharacterMove.FindAction("Activity", throwIfNotFound: true);
        m_CharacterMove_Interaction_Item = m_CharacterMove.FindAction("Interaction_Item", throwIfNotFound: true);
        m_CharacterMove_Move = m_CharacterMove.FindAction("Move", throwIfNotFound: true);
        m_CharacterMove_Interaction_Place = m_CharacterMove.FindAction("Interaction_Place", throwIfNotFound: true);
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

    // CharacterMove
    private readonly InputActionMap m_CharacterMove;
    private ICharacterMoveActions m_CharacterMoveActionsCallbackInterface;
    private readonly InputAction m_CharacterMove_Activity;
    private readonly InputAction m_CharacterMove_Interaction_Item;
    private readonly InputAction m_CharacterMove_Move;
    private readonly InputAction m_CharacterMove_Interaction_Place;
    public struct CharacterMoveActions
    {
        private @PlayerInput m_Wrapper;
        public CharacterMoveActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Activity => m_Wrapper.m_CharacterMove_Activity;
        public InputAction @Interaction_Item => m_Wrapper.m_CharacterMove_Interaction_Item;
        public InputAction @Move => m_Wrapper.m_CharacterMove_Move;
        public InputAction @Interaction_Place => m_Wrapper.m_CharacterMove_Interaction_Place;
        public InputActionMap Get() { return m_Wrapper.m_CharacterMove; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharacterMoveActions set) { return set.Get(); }
        public void SetCallbacks(ICharacterMoveActions instance)
        {
            if (m_Wrapper.m_CharacterMoveActionsCallbackInterface != null)
            {
                @Activity.started -= m_Wrapper.m_CharacterMoveActionsCallbackInterface.OnActivity;
                @Activity.performed -= m_Wrapper.m_CharacterMoveActionsCallbackInterface.OnActivity;
                @Activity.canceled -= m_Wrapper.m_CharacterMoveActionsCallbackInterface.OnActivity;
                @Interaction_Item.started -= m_Wrapper.m_CharacterMoveActionsCallbackInterface.OnInteraction_Item;
                @Interaction_Item.performed -= m_Wrapper.m_CharacterMoveActionsCallbackInterface.OnInteraction_Item;
                @Interaction_Item.canceled -= m_Wrapper.m_CharacterMoveActionsCallbackInterface.OnInteraction_Item;
                @Move.started -= m_Wrapper.m_CharacterMoveActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_CharacterMoveActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_CharacterMoveActionsCallbackInterface.OnMove;
                @Interaction_Place.started -= m_Wrapper.m_CharacterMoveActionsCallbackInterface.OnInteraction_Place;
                @Interaction_Place.performed -= m_Wrapper.m_CharacterMoveActionsCallbackInterface.OnInteraction_Place;
                @Interaction_Place.canceled -= m_Wrapper.m_CharacterMoveActionsCallbackInterface.OnInteraction_Place;
            }
            m_Wrapper.m_CharacterMoveActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Activity.started += instance.OnActivity;
                @Activity.performed += instance.OnActivity;
                @Activity.canceled += instance.OnActivity;
                @Interaction_Item.started += instance.OnInteraction_Item;
                @Interaction_Item.performed += instance.OnInteraction_Item;
                @Interaction_Item.canceled += instance.OnInteraction_Item;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Interaction_Place.started += instance.OnInteraction_Place;
                @Interaction_Place.performed += instance.OnInteraction_Place;
                @Interaction_Place.canceled += instance.OnInteraction_Place;
            }
        }
    }
    public CharacterMoveActions @CharacterMove => new CharacterMoveActions(this);
    private int m_KeyBoardAndMouseSchemeIndex = -1;
    public InputControlScheme KeyBoardAndMouseScheme
    {
        get
        {
            if (m_KeyBoardAndMouseSchemeIndex == -1) m_KeyBoardAndMouseSchemeIndex = asset.FindControlSchemeIndex("KeyBoardAndMouse");
            return asset.controlSchemes[m_KeyBoardAndMouseSchemeIndex];
        }
    }
    public interface ICharacterMoveActions
    {
        void OnActivity(InputAction.CallbackContext context);
        void OnInteraction_Item(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnInteraction_Place(InputAction.CallbackContext context);
    }
}
