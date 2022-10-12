public static class Wind {
	public static FMOD.Studio.EventInstance WindTimeline;
	
	public static float WindForce { set => WindTimeline.setParameterByName("Wind Force", value); }
}