using UnityEngine;

/*This script creates a template for lighting settings at a provided time
*/
[CreateAssetMenu(menuName = "LightingTemplate/LightingTemplateObject")]
public class LightingTemplateObject : ScriptableObject
{
	//Changeable elements
	[SerializeField] private float startTime; //When to start transitioning to the given settings
	[SerializeField] private LightSettingElement lightSettings; //The light variables to be updated at the provided time
	[SerializeField] private MaterialElement[] materialList; //The list of materials to be updated at the provided time
	
	//Getters
	public float StartTime => startTime;
	public LightSettingElement LightSettings => lightSettings;
	public MaterialElement[] MaterialList => materialList;
}

//Sets the light settings for a given time
[System.Serializable]
public class LightSettingElement
{
	public Color lightColor;
	public float lightIntensity;
}

//Set material settings for a given time
[System.Serializable]
public class MaterialElement
{
	public Material material;
	public string[] materialColorNames;
	public Color[] materialColors;
}
