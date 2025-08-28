using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;

public class VoiceChat : MonoBehaviour {

	public bool Enabled=false;
    public PhotonView photonView;
    public NetworkLauncher Netlauncher;
    int FREQUENCY = 8000;
	bool sending=true;
	AudioClip sendingClip;
	bool notRecording=true;
	int lastSample = 0;

	void FixedUpdate()
	{

//		if (ConnectedViaWifi())
		// If there is a connection
		if ((Netlauncher.host || Netlauncher.client) && Enabled)
		{
			if (notRecording)
			{
				notRecording = false;
				sendingClip = Microphone.Start(null, true, 100, FREQUENCY);
				sending = true;
			}
			else if(sending)
			{
				int pos = Microphone.GetPosition(null);
				int diff = pos-lastSample;
				
				if (diff > 0)
				{
					float[] samples = new float[diff * sendingClip.channels];
					sendingClip.GetData (samples, lastSample);
					byte[] ba = ToByteArray (samples);
                    photonView.RPC("Send", RpcTarget.Others, ba, sendingClip.channels);
					//Debug.Log(Microphone.GetPosition(null).ToString());
				}
				lastSample = pos;
			}
		}
	}

    [PunRPC]
    public void Send(byte[] ba, int chan) {
		if (Enabled) {
			float[] f = ToFloatArray (ba);
			GetComponent<AudioSource> ().clip = AudioClip.Create ("", f.Length, chan, FREQUENCY, false);
			GetComponent<AudioSource> ().clip.SetData (f, 0);
			if (!GetComponent<AudioSource> ().isPlaying)
				GetComponent<AudioSource> ().Play ();
		}
		
	}
	// Used to convert the audio clip float array to bytes
	public byte[] ToByteArray(float[] floatArray) {
		int len = floatArray.Length * 4;
		byte[] byteArray = new byte[len];
		int pos = 0;
		foreach (float f in floatArray) {
			byte[] data = System.BitConverter.GetBytes(f);
			System.Array.Copy(data, 0, byteArray, pos, 4);
			pos += 4;
		}
		return byteArray;
	}
	// Used to convert the byte array to float array for the audio clip
	public float[] ToFloatArray(byte[] byteArray) {
		int len = byteArray.Length / 4;
		float[] floatArray = new float[len];
		for (int i = 0; i < byteArray.Length; i+=4) {
			floatArray[i/4] = System.BitConverter.ToSingle(byteArray, i);
		}
		return floatArray;
	}
	
}

