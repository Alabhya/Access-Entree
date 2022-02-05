using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This script creates a day/night system which simulates the rising and setting of the sun by rotating a directional light
  and updaing the lighting and color values of provided materials at specified times.
*/
public class DayNightCycle : MonoBehaviour
{
	//Modifiable variables
	public int startHour = 6; //The ingame hour the simulation should start
	public int startMin = 0; //The ingame minute the simulation should start
	public float cycleRate = 1; //How quickly time should go in game (ingame minutes to IRL seconds)
	[SerializeField] private LightingTemplateObject[] lightingTemplates; //A list of lighting templates that determine when the lighting settings are changed
	public Light dirLight; //The directional light that lights the scene
	public float oriTime = 12; //What time corresponds with the Light Pivot pointing to 0 degrees. This is dependent on the starting position of the Directional Light (Updated setup would be 12 PM, original setup was about 9 AM) 

	//Non-modifiable variables
	[HideInInspector]
	public int degHours = 15; //How many degrees the light rotates in an ingame hour.
	private float degPerSec; //How many degrees the light rotates in an ingame second.
	private int curLightingTempIndex; //Keeps track of which Lighthing template to follow

	//Called when something is updated in the inspector
	[ExecuteInEditMode]
	void OnValidate()
	{
		//Sort the Lighting Templates
		System.Array.Sort(lightingTemplates, (a,b) => a.StartTime.CompareTo(b.StartTime));
		
		//Limit time inputs to actual time values
		startHour = Mathf.Clamp(startHour, 0, 23);
		startMin = Mathf.Clamp(startMin, 0, 59);
		
		//Update the system to reflect changes
		SetTime(startHour, startMin);
	}
	
    //Start is called before the first frame update
    void Start()
    {
		degPerSec = degHours / 60f; //Set the degrees per second
        SetTime(startHour, startMin); //Make sure the simulation is set to the provided time
    }

    //Update is called once per frame
    void Update()
    {
		//Rotate the light
        dirLight.transform.parent.Rotate(new Vector3((degPerSec * cycleRate), 0, 0)* Time.deltaTime);
		
		//Update the lighting
		float newDeg = UnityEditor.TransformUtils.GetInspectorRotation(dirLight.transform.parent).x; //Get the current rotation
		if(newDeg < 0){newDeg += 360f;} //Account for Unity's -180 to 180 degree range
		SetLighting((newDeg/degHours) + oriTime);
    }
	
	/**/
	//This function sets the day/night system to the requested time
	public void SetTime(int newHour, int newMin)
	{
		float newDegTime; //The requested time as a decimal number (i.e. 6:30 would be 6.5)
		float newDeg; //The requested time as an angle
		
		//Update the rotation of the light
		newDegTime = newHour + (newMin/60f); //Convert provided minutes to decimals
		newDeg = (newDegTime - oriTime)*degHours; //Account for the light's initial placing
		dirLight.transform.parent.eulerAngles = new Vector3(newDeg, 0, 0); //Set the light rotation

		//Update the lighting
		//Set the lighting template index correctly
		curLightingTempIndex = lightingTemplates.Length-1;
		for(int i = 0; i<lightingTemplates.Length-1; i++)
		{
			if(newDegTime >= lightingTemplates[i].StartTime && newDegTime < lightingTemplates[i+1].StartTime)
			{
				curLightingTempIndex = i;
				break;
			}
		}
		SetLighting(newDegTime); //Set the lighting values
	}
	
	/*Functions related to lighting*/
	//This function returns the percentage of the way the value 'x' is between points 'a' and 'b' where 'a' is the smallest value and 'b' is the largest.
	float GetLerp(float x, float a, float b)
	{
		return (x-a)/(b-a);
	}
	
	//This function updates the lighting values of the scene
	void SetLighting(float curTime)
	{
		bool canUpdate = false; //Tracks whether the lighting system needs to be updated 
		float lerpVal = 0; //Where in the linear interpolation range the system is currently
		
		if(curTime > 24) {curTime-=24;}//Make sure the provided time is in the proper range
		
		//Check the transition between the final template in the list and the first template
		if(curLightingTempIndex == lightingTemplates.Length-1)
		{
			if(lightingTemplates[curLightingTempIndex].StartTime <= curTime)
			{
				canUpdate = true;
				lerpVal = GetLerp(curTime, lightingTemplates[curLightingTempIndex].StartTime, lightingTemplates[0].StartTime);
			}				
			else if(lightingTemplates[0].StartTime > curTime)
			{
				canUpdate = true;
				lerpVal = GetLerp(curTime, 0f, lightingTemplates[0].StartTime);
			}
			else{curLightingTempIndex = 0;}
		}
		
		//Check the transition between the current and next template in the list
		else
		{
			if(lightingTemplates[curLightingTempIndex].StartTime <= curTime && lightingTemplates[curLightingTempIndex+1].StartTime > curTime)
			{
				canUpdate = true;
				lerpVal = GetLerp(curTime, lightingTemplates[curLightingTempIndex].StartTime, lightingTemplates[curLightingTempIndex+1].StartTime);
			}
			else{curLightingTempIndex += 1;}
		}
		
		//Update everything if necessary
		if(canUpdate)
		{
			UpdateLightSettings(lerpVal);
			UpdateMaterials(lerpVal);
		}
	}
	
	//This function provides the proper lighting template
	LightingTemplateObject GetNextLightingTemplate()
	{
		//If the current template is the final one, the next template is the first
		if(curLightingTempIndex == lightingTemplates.Length-1)
		{return lightingTemplates[0];}
		//Otherwise return the next template
		else{return lightingTemplates[curLightingTempIndex+1];}
	}
	
	//This function updates the settings of the directional light
	void UpdateLightSettings(float lerpVal)
	{
		//Variables for convinience
		LightingTemplateObject prevLightSettingTemp = lightingTemplates[curLightingTempIndex];
		LightingTemplateObject nextLightSettingTemp = GetNextLightingTemplate();
		LightSettingElement pLight = prevLightSettingTemp.LightSettings;
		LightSettingElement nLight = nextLightSettingTemp.LightSettings;
			
		//Update Color
		dirLight.color = Color.Lerp(pLight.lightColor, nLight.lightColor, lerpVal);
	
		//Update Intensity
		dirLight.intensity = Mathf.Lerp(pLight.lightIntensity, nLight.lightIntensity, lerpVal);
	}
	
	//This function updates the provided materials
	void UpdateMaterials(float lerpVal)
	{
		//Variables for convinience
		MaterialElement pMaterial;
		MaterialElement nMaterial;
		LightingTemplateObject prevLightTemp = lightingTemplates[curLightingTempIndex];
		LightingTemplateObject nextLightTemp = GetNextLightingTemplate();
		
		//Go through each material
		for(int i=0; i<prevLightTemp.MaterialList.Length; i++) 
		{
			pMaterial = prevLightTemp.MaterialList[i];
			nMaterial = nextLightTemp.MaterialList[i];
			
			//Update Color
			for(int j=0; j<pMaterial.materialColorNames.Length; j++)
			{
				pMaterial.material.SetColor(pMaterial.materialColorNames[j], Color.Lerp(pMaterial.materialColors[j], nMaterial.materialColors[j], lerpVal));
			}
		}	
	}
}
