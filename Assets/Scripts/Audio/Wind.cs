public static class Wind
{
	private static FMOD.Studio.EventInstance windTimeline;
	public static float WindForce { set => windTimeline.setParameterByName("Wind Force", value); }
}