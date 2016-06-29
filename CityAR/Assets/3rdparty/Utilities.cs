using UnityEngine;
using System.Collections.Generic;

public static class Utilities
{
	#region Math helper
	public static Vector3 RandomCircle(Vector3 theCenter, float theRadius)
	{ 
		float anAngle = Random.value * 360; 
		Vector3 aPosOnCircle = new Vector3(
			theCenter.x + theRadius * Mathf.Sin(anAngle * Mathf.Deg2Rad),
			theCenter.y + theRadius * Mathf.Cos(anAngle * Mathf.Deg2Rad),
			theCenter.z
			);
		return aPosOnCircle; 
	}
	#endregion

	#region extension methods for: Color
	public static Color SetAlpha(this Color theColor, float theAlpha)
	{
		theColor.a = theAlpha;
		return theColor;
	}
	#endregion

	#region extension methods for: IList
	public static void RemoveNulls<T>(this IList<T> collection) where T : class
	{
		for (var i = collection.Count-1; i >= 0 ; i--)
		{
			if (collection[i] == null)
				collection.RemoveAt(i);
		}
	}
	#endregion

	#region calculate time
	public static string DisplayTime(float theTime)
	{
		int minutes = (int)Mathf.Floor(theTime / 60f);
		int seconds = (int)theTime - (minutes * 60);
		string minutesString = (minutes < 10) ? string.Format("0{0}", minutes) : minutes.ToString();
		string secondsString = (seconds < 10) ? string.Format("0{0}", seconds) : seconds.ToString();

		return string.Format("{0}:{1}", minutesString, secondsString);
	}
    #endregion

    #region RandomNumbers
    public static int RandomInt(int a, int b)
    {
        int i = Random.Range(a, b);
        return i;
    }

    public static float RandomFloat(float a, float b)
    {
        float i = Random.Range(a, b);
        return i;
    }
    #endregion
}