using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdsManager : MonoBehaviour {
	public InterstitialAd interstitial;
	public BannerView bannerView;
	// Use this for initialization
	void Start () {

		#if UNITY_EDITOR
		string adUnitId = "unused";
		#elif UNITY_ANDROID
		string adUnitId = "ca-app-pub-1968859549426259/6955415122";
		#elif UNITY_IPHONE
		string adUnitId = "ca-app-pub-1968859549426259/9350478329";
		#else
		string adUnitId = "unexpected_platform";
		#endif

		var size = new AdSize (280, 50);
		bannerView = new BannerView(adUnitId, size, AdPosition.BottomLeft);
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().AddTestDevice (AdRequest.TestDeviceSimulator).Build();
		// Load the banner with the request.
		bannerView.LoadAd(request);
		bannerView.Show ();
		RequestInterstitial ();
		}

		public void ShowBanner() {
		bannerView.Show ();
		}

		public void HideBanner() {
		bannerView.Hide ();
		}

		// Update is called once per frame
		void Update () {

		}


		private void RequestInterstitial()
		{
		#if UNITY_ANDROID
		string adUnitId = "ca-app-pub-1968859549426259/8292547529";
		#elif UNITY_IPHONE
		string adUnitId = "ca-app-pub-1968859549426259/1827211525";
		#else
		string adUnitId = "unexpected_platform";
		#endif

		// Initialize an InterstitialAd.
		interstitial = new InterstitialAd(adUnitId);
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the interstitial with the request.
		interstitial.LoadAd(request);
		}

		public void ShowInterstitial() {

		if (interstitial.IsLoaded() && !UIScript.vrEnabled) {
		interstitial.Show();
		}
		}
		}
