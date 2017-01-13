using UnityEngine;
using UnityEngine.UI;
//using System.Collections;

namespace AC.TimeOfDaySystemFree
{

	public class DisplayTime : MonoBehaviour 
	{

		public Text timeText;
		private TimeOfDayManager m_TOD_Manager = null;

		void Start()
		{
			m_TOD_Manager = GetComponent<TimeOfDayManager> ();
		}
			
		public void Update()
		{

			if (timeText != null) 
			{

				timeText.text = GetTimeString (m_TOD_Manager.Hour, m_TOD_Manager.Minute); 
			}
		}
			
		protected string GetTimeString(float hour, float minute)
		{
			string h   = hour   < 10 ? "0" + hour.ToString()   : hour.ToString();
			string m   = minute < 10 ? "0" + minute.ToString() : minute.ToString();
			//----------------------------------------------------------------------

			return h   + ":" + m;
		}
	}


}
