using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    [Serializable]
    private class Player
    {
        [Serializable]
        private struct ButtonEvent
        {
            [SerializeField] private bool _continuousCallback;
            [SerializeField] private UnityEvent<bool> _event;
            public bool ContinuousCallback => _continuousCallback;
            public UnityEvent<bool> Event => _event;
        }
        [SerializeField] private ButtonEvent[] _buttonEvents;
        [SerializeField] private UnityEvent<Vector2> _joystickEvent;
        [SerializeField] private bool _readAxisAsDiscrete; // 
        private float _discreteAxisThreshold = 0.5f; //number between 0 and 1
        private bool _pastThresholdHorizontal = false;
        private bool _pastThresholdVertical = false;
        private string _playerPrefix;
        private Vector2 _joystick => new Vector2(Input.GetAxis(_playerPrefix + "Horizontal"), Input.GetAxis(_playerPrefix + "Vertical"));

        public void SetPrefix(string value)
        {
            _playerPrefix = value;
        }

        private bool GetButton(int index, bool continuous)
        {
            string action = _playerPrefix;
            switch (index)
            {
                case 0:
                    action += "Fire1";
                    break;
                case 1:
                    action += "Fire2";
                    break;
                default:
                    Debug.LogAssertion("El input ingresado no está registrado.");
                    return false;
            }
            bool debugValue = continuous ? Input.GetButton(action) : Input.GetButtonDown(action);
            Debug.Log($"{action}, { debugValue}");
            return continuous ? Input.GetButton(action) : Input.GetButtonDown(action);
        }

        public void CheckButtons()
        {
            for (int i = 0; i < _buttonEvents.Length; i++)
            {
                ButtonEvent buttonEvent = _buttonEvents[i];
                buttonEvent.Event?.Invoke(GetButton(i, buttonEvent.ContinuousCallback));
            }
        }

        public void CheckJoystick()
        {
            Vector2 joystickInput = _joystick;
            if(_readAxisAsDiscrete){
                if(!_pastThresholdHorizontal && Mathf.Abs(_joystick.x) > _discreteAxisThreshold){
                    _pastThresholdHorizontal = true;
                    joystickInput.x = Mathf.Sign(_joystick.x);
                }
                else if(Mathf.Abs(_joystick.x) > _discreteAxisThreshold){
                    joystickInput.x = 0;
                }
                else{
                    _pastThresholdHorizontal = false;
                    joystickInput.x = 0;
                }

                if(!_pastThresholdVertical && Mathf.Abs(_joystick.y) > _discreteAxisThreshold){
                    _pastThresholdVertical = true;
                    joystickInput.y = Mathf.Sign(_joystick.y);
                }
                else if(Mathf.Abs(_joystick.y) > _discreteAxisThreshold){
                    joystickInput.y = 0;
                }
                else{
                    _pastThresholdVertical = false;
                    joystickInput.y = 0;
                }
            }
            _joystickEvent?.Invoke(joystickInput);
        }
    }
    [SerializeField] private bool _canDebug;
    [SerializeField] private Player[] _players;

    private void OnEnable()
    {
        for (int i = 0; i < _players.Length; i++)
        {
            _players[i].SetPrefix($"P{i+1}_");
        }
    }
    private void Update()
    {
        for(int i=0; i< _players.Length; i++)
        {
            _players[i].CheckJoystick();
            _players[i].CheckButtons();
        }
    }
    public void TestInput(Vector2 input)
    {
        if(_canDebug) Debug.Log(input);
    }
    public void TestInput(bool input)
    {
        if(_canDebug) Debug.Log($"{input}");
    }
}
