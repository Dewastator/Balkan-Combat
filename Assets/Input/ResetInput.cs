//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/Input/ResetInput.inputactions
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

public partial class @ResetInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @ResetInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ResetInput"",
    ""maps"": [
        {
            ""name"": ""Reset"",
            ""id"": ""0f599867-4264-4872-890d-0de4a2763c0c"",
            ""actions"": [
                {
                    ""name"": ""ResetScene"",
                    ""type"": ""Button"",
                    ""id"": ""5992cdcd-1b07-486b-8bde-bbdb0ff8dbf6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""16c3eed6-145c-4476-b0fd-cb55522f90fc"",
                    ""path"": ""<Keyboard>/7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ResetScene"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Reset
        m_Reset = asset.FindActionMap("Reset", throwIfNotFound: true);
        m_Reset_ResetScene = m_Reset.FindAction("ResetScene", throwIfNotFound: true);
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

    // Reset
    private readonly InputActionMap m_Reset;
    private List<IResetActions> m_ResetActionsCallbackInterfaces = new List<IResetActions>();
    private readonly InputAction m_Reset_ResetScene;
    public struct ResetActions
    {
        private @ResetInput m_Wrapper;
        public ResetActions(@ResetInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @ResetScene => m_Wrapper.m_Reset_ResetScene;
        public InputActionMap Get() { return m_Wrapper.m_Reset; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ResetActions set) { return set.Get(); }
        public void AddCallbacks(IResetActions instance)
        {
            if (instance == null || m_Wrapper.m_ResetActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_ResetActionsCallbackInterfaces.Add(instance);
            @ResetScene.started += instance.OnResetScene;
            @ResetScene.performed += instance.OnResetScene;
            @ResetScene.canceled += instance.OnResetScene;
        }

        private void UnregisterCallbacks(IResetActions instance)
        {
            @ResetScene.started -= instance.OnResetScene;
            @ResetScene.performed -= instance.OnResetScene;
            @ResetScene.canceled -= instance.OnResetScene;
        }

        public void RemoveCallbacks(IResetActions instance)
        {
            if (m_Wrapper.m_ResetActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IResetActions instance)
        {
            foreach (var item in m_Wrapper.m_ResetActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_ResetActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public ResetActions @Reset => new ResetActions(this);
    public interface IResetActions
    {
        void OnResetScene(InputAction.CallbackContext context);
    }
}
