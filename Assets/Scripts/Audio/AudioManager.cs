using FMODUnity;
using UnityEngine;
public class AudioManager : MonoBehaviour {
	[SerializeField] private StudioEventEmitter _windEmitter;
	
	private void Start() {
		InitWind();
	}
	
	private void InitWind() => Wind.WindTimeline = _windEmitter.EventInstance;

	public static void PlayerJoinedEvent(StudioEventEmitter soundEmitter, EventReference eventReference) {
		soundEmitter.EventReference = eventReference;
		soundEmitter.Play();
	}
}