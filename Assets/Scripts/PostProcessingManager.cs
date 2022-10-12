using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class PostProcessingManager : MonoBehaviour {
	[SerializeField] private PostProcessVolume _volume;

	public static Vignette Vignette { get; private set; }

	private void Start() => Vignette = ScriptableObject.CreateInstance<Vignette>();
}