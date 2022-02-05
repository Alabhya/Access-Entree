using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Prints out the dialogue text one letter at a time.
public class TypewriterEffect : MonoBehaviour
{
	private float textSpeed = 25f; //How quickly the typing goes
	private float pauseTime = 0.6f; //How long a full punctuation pause lasts
	[SerializeField] private AudioSource audioSource; //The sound that plays when text is written
	
	public bool IsRunning {get; private set;} //Tracks whether the typingCoroutine is running
	private Coroutine typingCoroutine;
	
	//Determines what characters to pause at and for how long.
	private readonly List<Punctuation> punctuations = new List<Punctuation>()
	{
		new Punctuation(new HashSet<char>(){'.', '!', '?'}, 1),
		new Punctuation(new HashSet<char>(){',', ';', ':'}, 0.5f)
	};
	
	//Starts the TypeText coroutine
	//textToType: The string to be written
	//textLabel: What the string is written on
	//sfx: What sound is played when characters are written
	//tSpd: How quickly characters are written
	//pTime: How long a pause lasts.
	public void Run(string textToType, TMP_Text textLabel, AudioClip sfx, float tSpd, float pTime)
	{
		//Set typing variables
		textSpeed = tSpd;
		pauseTime = pTime;
		audioSource.clip = sfx;
		
		//Start typing
		typingCoroutine = StartCoroutine(TypeText(textToType, textLabel));
	}
	
	//Stops the TypeText coroutine
	public void Stop()
	{
		//Stop audio stuff
		audioSource.clip = null;
		audioSource.Stop();
		
		//Stop coroutine
		StopCoroutine(typingCoroutine);
		IsRunning = false;
	}
	
	//Writes out the text
	//textToType: The string to be written
	//textLabel: What the string is written on
	private IEnumerator TypeText(string textToType, TMP_Text textLabel)
	{
		IsRunning = true;
		textLabel.text = string.Empty; //Make sure textbox is empty first
		
		float t = 0; //Elapsed time since we started writing
		int charIndex = 0; //How many characters to type on a given frame
		
		//Write the text
		while(charIndex < textToType.Length)
		{
			int lastCharIndex = charIndex; //Track last character to prevent unnecessarily long pauses.
			
			t += Time.deltaTime*textSpeed;
			charIndex = Mathf.FloorToInt(t);
			charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);//Prevent from being larger than textToType
	
			//Used to keep framerate consistancy
			for(int i = lastCharIndex; i < charIndex; i++)
			{
				audioSource.Play();
				
				bool isLast = i >= textToType.Length - 1;
				textLabel.text = textToType.Substring(0, i+1);
				
				//Pause if punctuation (only once in the case of repeats or ellipses
				//Also make sure not to pause on final character (no point to).
				if(IsPunctuation(textToType[i], out float waitTime) && !isLast && !IsPunctuation(textToType[i + 1], out _))
				{
					audioSource.Stop();
					yield return new WaitForSeconds(waitTime);
				}
			}
			
			yield return null; //Wait one frame
		}
		
		audioSource.Stop();
		audioSource.clip = null;
		IsRunning = false;
	}
	
	//Checks if character is a punctuation
	//character: Character being checked
	//waitTime: The returned amount of time to waitTime
	//Returns whether the character is in the pause list
	private bool IsPunctuation(char character, out float waitTime)
	{
		//If selected character is a chosen punctuation pause for given time
		foreach(Punctuation punctuationCategory in punctuations)
		{
			if(punctuationCategory.Punctuations.Contains(character))
			{
				waitTime = pauseTime * punctuationCategory.WaitMultiple;
				return true;
			}
		}
		
		waitTime = default;
		return false;
	}
	
	//Define punctuation struct
	private readonly struct Punctuation
	{
		public readonly HashSet<char> Punctuations; //Characters that are considered punctuations.
		public readonly float WaitMultiple;	//How long to multiply pause time for.
		
		public Punctuation(HashSet<char> punctuations, float pauseMultiple)
		{
			Punctuations = punctuations;
			WaitMultiple = pauseMultiple;
		}
	}
}
