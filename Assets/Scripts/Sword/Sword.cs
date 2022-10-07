using UnityEngine;
public class Sword : MonoBehaviour
{
	[SerializeField] private GrapplingGun _grapplingGun;

	public void LaunchHook() => _grapplingGun.LaunchHook();
	public void ReleaseHook() => _grapplingGun.ReleaseHook();
}

