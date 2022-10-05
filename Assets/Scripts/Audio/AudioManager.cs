using FMODUnity;
using UnityEngine;
public class AudioManager : MonoBehaviour
{
	public static void PlayerJoinedEvent(StudioEventEmitter soundEmitter, EventReference eventReference)
	{
		soundEmitter.EventReference = eventReference;
		soundEmitter.Play();
	}
}

