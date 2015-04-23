using UnityEngine;
using System.Collections;
using AR.Drone.Client;
using AR.Drone.Video;
using AR.Drone.Data;
using AR.Drone.Data.Navigation;
using FFmpeg.AutoGen;
using XInputDotNetPure;
using NativeWifi;


using System;
using jp.nyatla.nyartoolkit.cs.markersystem;
using jp.nyatla.nyartoolkit.cs.core;
using NyARUnityUtils;
using System.IO;



public class DroneController : MonoBehaviour {


	
	private GameUI gameUI;

	// Indicates that the drone is landed
	private bool isLanded = true;
	// Indicates that the startButton is Pressed
	private bool startButtonPressed = false;
	// Texture used for the camera content
	private Texture2D cameraTexture;
	// A black texture used for the inactive plane
	private Texture2D blackTexture;
	// byte array which will be filled by the camera data
	private byte[] data;
	// Drone variables
	private VideoPacketDecoderWorker videoPacketDecoderWorker;
	private DroneClient droneClient;
	private NavigationData navigationData;

	// Width and height if the camera
	private int width = 640;
	private int height = 360;

	// wlanclient for signal strength
	private WlanClient client;


	//drone controller 
	float pitch,roll,gaz,yaw;

	// pick up
	public NyARUnityMarkerSystem _ms;
	private NyARUnityWebCam _ss;
	private int mid;//marker id
	private GameObject _bg_panel;


	// Use this for initialization
	void Awake () {
		Debug.Log("Start");
		// initialize data array
		data = new byte[width*height*3];
		cameraTexture = new Texture2D (width, height);

		// Initialize drone
		videoPacketDecoderWorker = new VideoPacketDecoderWorker(PixelFormat.BGR24, true, OnVideoPacketDecoded);
		videoPacketDecoderWorker.Start();
		droneClient = new DroneClient("192.168.1.1");
		droneClient.UnhandledException += HandleUnhandledException;
		droneClient.VideoPacketAcquired += OnVideoPacketAcquired;
		droneClient.NavigationDataAcquired += navData => navigationData = navData;
		videoPacketDecoderWorker.UnhandledException += HandleUnhandledException;
		droneClient.Start ();

		// activate main drone camera
		switchDroneCamera (AR.Drone.Client.Configuration.VideoChannelType.Horizontal);

		// determine connection
		client = new WlanClient();

		//Make WebcamTexture wrapped Sensor.
		this._ss=NyARUnityWebCam.createInstance(cameraTexture);
		//Make configulation by Sensor size.
		NyARMarkerSystemConfig config = new NyARMarkerSystemConfig(this._ss.width,this._ss.height);
		
		this._ms=new NyARUnityMarkerSystem(config);

		mid=this._ms.addARMarker((Texture2D)(Resources.Load("Marker1", typeof(Texture2D))),16,25,80);
		mid=this._ms.addARMarker((Texture2D)(Resources.Load("Marker2", typeof(Texture2D))),16,25,80);
		mid=this._ms.addARMarker((Texture2D)(Resources.Load("Marker3", typeof(Texture2D))),16,25,80);
		mid=this._ms.addARMarker((Texture2D)(Resources.Load("Marker4", typeof(Texture2D))),16,25,80);
		mid=this._ms.addARMarker((Texture2D)(Resources.Load("Marker5", typeof(Texture2D))),16,25,80);
		mid=this._ms.addARMarker((Texture2D)(Resources.Load("Marker6", typeof(Texture2D))),16,25,80);
		mid=this._ms.addARMarker((Texture2D)(Resources.Load("Marker7", typeof(Texture2D))),16,25,80);
		mid=this._ms.addARMarker((Texture2D)(Resources.Load("Marker8", typeof(Texture2D))),16,25,80);
		mid=this._ms.addARMarker((Texture2D)(Resources.Load("Marker9", typeof(Texture2D))),16,25,80);

		//setup background
		this._bg_panel=GameObject.Find("Plane");
		this._bg_panel.GetComponent<Renderer>().material.mainTexture=cameraTexture;
		this._ms.setARBackgroundTransform(this._bg_panel.transform);
		
		//setup camera projection
		this._ms.setARCameraProjection(this.GetComponent<Camera>());
		//GameObject.Find("Cube").GetComponent<Renderer>().material.mainTexture=new Texture2D(64,64);
		//GameObject.Find("Cube").GetComponent<Renderer>().material.mainTexture=new Texture2D(64,64);

		//pick up
		this._ss.start();


		//vong
		gameUI = GameObject.Find ("GameUI").GetComponent<GameUI> ();

	}

	// Update is called once per frame
	void Update () {

		if (gameUI._time < 0) {
			//Debug.Log (gameUI._time);
		
			droneClient.Land ();

			//Application.LoadLevel("Main");
		}
		

		convertCameraData ();

				if(Input.GetKeyDown(KeyCode.Z))
				{
					droneClient.Takeoff();
				}
				if(Input.GetKeyDown(KeyCode.X))
				{
					droneClient.Land();
				}
		
				roll=Input.GetAxis("Horizontal");
				pitch=-Input.GetAxis("Vertical");
				yaw=Input.GetAxis("Horizontall");
				gaz=Input.GetAxis("Verticall");

		droneClient.Progress(AR.Drone.Client.Command.FlightMode.Progressive, pitch: pitch, roll: roll, gaz: gaz, yaw: yaw); 




	
		// determine wifi strength 
		determineWifiStrength ();


		//Update marker system by ss
		this._ss.update();
		this._ms.update(this._ss);
		//update Gameobject transform

		if(this._ms.isExistMarker(0)){
			this._ms.setMarkerTransform(0,GameObject.Find("MarkerObject").transform);
			//update cube texture
			//this._ms.getMarkerPlaneImage(mid,this._ss,-40,-40,80,80,(Texture2D)(GameObject.Find("Cube").GetComponent<Renderer>().material.mainTexture));
		}else{
			// hide Game object
			GameObject.Find("MarkerObject").transform.localPosition=new UnityEngine.Vector3(0,0,-1000);
		}
//		if(this._ms.isExistMarker(1)){
//			this._ms.setMarkerTransform(1,GameObject.Find("MarkerObject1").transform);
//			//update cube texture
//			//this._ms.getMarkerPlaneImage(mid,this._ss,-40,-40,80,80,(Texture2D)(GameObject.Find("Cube").GetComponent<Renderer>().material.mainTexture));
//		}else{
//			// hide Game object
//			GameObject.Find("MarkerObject1").transform.localPosition=new UnityEngine.Vector3(0,0,-100);
//		}


	}

	// Called if the gameobject is destroyed
	void OnDestroy(){
		droneClient.Land();
		droneClient.Stop();
		droneClient.Dispose ();
		videoPacketDecoderWorker.Stop ();
		videoPacketDecoderWorker.Dispose();
	}

	/// <summary>
	/// Log the unhandled exception.
	/// </summary>
	/// <param name="arg1">Arg1.</param>
	/// <param name="arg2">Arg2.</param>
	void HandleUnhandledException (object arg1, System.Exception arg2)
	{
		Debug.Log (arg2); 
	}

	/// <summary>
	/// Switchs the drone camera.
	/// </summary>
	/// <param name="Type">Video channel type.</param>
	private void switchDroneCamera(AR.Drone.Client.Configuration.VideoChannelType Type){
		var configuration = new AR.Drone.Client.Configuration.Settings();
		configuration.Video.Channel = Type;
		droneClient.Send(configuration);
	}

	/// <summary>
	/// Converts the camera data to a color array and applies it to the texture.
	/// </summary>
	public void convertCameraData(){
		int r = 0;
		int g = 0;
		int b = 0;
		int total = 0;
		var colorArray = new Color32[data.Length/3];
		for(var i = 0; i < data.Length; i+=3)
		{
			colorArray[i/3] = new Color32(data[i + 2], data[i + 1], data[i + 0], 1);
			r += data[i + 2];
			g += data[i + 1];
			b += data[i + 0];
			total++;
		}
		r /= total;
		g /= total;
		b /= total;
		cameraTexture.SetPixels32(colorArray);
		cameraTexture.Apply();
	}



	/// <summary>
	/// Determines what happens if a video packet is acquired.
	/// </summary>
	/// <param name="packet">Packet.</param>
	private void OnVideoPacketAcquired(VideoPacket packet)
	{
		if (videoPacketDecoderWorker.IsAlive)
			videoPacketDecoderWorker.EnqueuePacket(packet);
	}

	/// <summary>
	/// Determines what happens if a video packet is decoded.
	/// </summary>
	/// <param name="frame">Frame.</param>
	private void OnVideoPacketDecoded(VideoFrame frame)
	{
		data = frame.Data;
	}



	/// <summary>
	/// Determine the wifi strength.
	/// </summary>
	private void determineWifiStrength(){
		int signalQuality = 0;
		foreach (WlanClient.WlanInterface wlanIface in client.Interfaces)
		{
			try {
				signalQuality = (int)wlanIface.CurrentConnection.wlanAssociationAttributes.wlanSignalQuality;
			}
			catch (System.Exception e ){
				Debug.Log ("No Wifi Connection");
			}
		}
	}


}
