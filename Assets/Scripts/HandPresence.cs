using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using CommonUsages = UnityEngine.XR.CommonUsages;
using InputDevice = UnityEngine.XR.InputDevice;

public class HandPresence : MonoBehaviour
{
	[SerializeField] private InputDeviceCharacteristics controllerCharacteristics;
	private InputDevice _targetDevice;

	private bool _inputEnabled;
	
	public UnityEvent OnTriggerPress;
	public UnityEvent OnTriggerRelease;
	private InputDevice TargetDevice { 
		get {
			if (!_targetDevice.isValid) _targetDevice = TryInitializeController();
			return _targetDevice;
		}
	}

	private void Update()
	{
		if(!_inputEnabled) return;
		CheckInput();
	}
	private void CheckInput()
	{
		TargetDevice.TryGetFeatureValue(CommonUsages.trigger, out var trigger);
		if (trigger > 0.7f)
			OnTriggerPress?.Invoke();
		if (trigger < 0.7f)
			OnTriggerRelease?.Invoke();
	}

	private void BlockInput()
	{
		_inputEnabled = false;
	}

	private InputDevice TryInitializeController()
	{
		var inputDevices = new List<InputDevice>();
		InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, inputDevices);
		
		return inputDevices[0];
	}
}
