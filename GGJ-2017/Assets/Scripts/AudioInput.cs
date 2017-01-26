using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AudioInput : MonoBehaviour {

	public static float MicLoudness;

	//mic initialization
	void InitMic(){
		_clipRecord = Microphone.Start(null, true, 999, 44100);
		iPhoneSpeaker.ForceToSpeaker ();
	}

	void StopMicrophone()
	{
		Microphone.End(null);
	}


	AudioClip _clipRecord = new AudioClip();
	int _sampleWindow = 128;

	//get data from microphone into audioclip
	float  LevelMax()
	{
		float levelMax = 0;
		float[] waveData = new float[_sampleWindow];
		int micPosition = Microphone.GetPosition(null)-(_sampleWindow+1); // null means the first microphone
		if (micPosition < 0) return 0;
		_clipRecord.GetData(waveData, micPosition);
		// Getting a peak on the last 128 samples
		for (int i = 0; i < _sampleWindow; i++) {
			float wavePeak = waveData[i] * waveData[i];
			if (levelMax < wavePeak) {
				levelMax = wavePeak;
			}
		}
		return levelMax;
	}



	void Update()
	{
		// levelMax equals to the highest normalized value power 2, a small number because < 1
		// pass the value to a static var so we can access it from anywhere
		MicLoudness = LevelMax ();
		#if UNITY_ANDROID
		if (!DetectHeadset.Detect ()) {
			MicLoudness = LevelMax ()*5;
		} else {
			MicLoudness = LevelMax ();
		}
			
		#endif
	}

	bool _isInitialized;
	// start mic when scene starts
	void OnEnable()
	{
		InitMic();
		_isInitialized=true;
	}

	//stop mic when loading a new level or quit application
	void OnDisable()
	{
		StopMicrophone();
	}

	void OnDestroy()
	{
		StopMicrophone();
	}


	// make sure the mic gets started & stopped when application gets focused
	void OnApplicationFocus(bool focus) {
		if (focus)
		{
//			Debug.Log("Focus");

			if(!_isInitialized){
//				Debug.Log("Init Mic");
				InitMic();
				_isInitialized=true;
			}
		}      
		if (!focus)
		{
//			Debug.Log("Pause");
			StopMicrophone();
//			Debug.Log("Stop Mic");
			_isInitialized=false;

		}
	}

}
