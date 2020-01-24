using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class MapTrigger : MonoBehaviour {

	SerialPort sp = new SerialPort("/dev/tty.usbmodem1411", 9600);
	//each game object is a group of all the sounds from that location
	public GameObject amz;
	public GameObject kl;
	public GameObject temple;
	public GameObject noosa;

	// Use this for initialization
	void Start ()
	{
		sp.Open();
		sp.ReadTimeout = 1000;
	}

	// Update is called once per frame
	void Update ()
	{
			//each line is read from the serial port
			string[] elements = sp.ReadLine().Split('/');
			//if the array is the expected length
			if (elements.Length == 5)
			{
				//store the first value of the array in RFID
			int RFID = int.Parse(elements[0]);
			//call the function mapChange with the value of RFID
			mapChange(RFID);
			}

	}

	void mapChange(int Map)
	{

		//bools to compare if the sound is playing
		bool Map1 = false;
		bool Map2 = false;
		bool Map3 = false;
		bool Map4 = false;
		//if the value is zero, there are no maps present
		if(Map == 0)
		{
			//no sounds playing
			kl.SetActive(false);
			amz.SetActive(false);
			temple.SetActive(false);
			noosa.SetActive(false);

		}

		if(Map == 1 && Map1==false)
		{
			// if RFID reads 1, Kuala Lumpur sounds play
			amz.SetActive(false);
			temple.SetActive(false);
			noosa.SetActive(false);
			kl.SetActive(true);
			//Kuala Lumpur sounds are playing, all others are not
			Map1 = true;
			Map2 = false;
			Map3 = false;
			Map4 = false;

		}

		if(Map == 2 && Map2 == false)
		{
		// if RFID reads 2, Amazon rainforest sounds play
			kl.SetActive(false);
			temple.SetActive(false);
			noosa.SetActive(false);
			amz.SetActive(true);
			//Amazon sounds are playing, all others are not
			Map1 = false;
			Map3 = false;
			Map4 = false;
			Map2 = true;
		}

		if(Map == 3 && Map3 == false)
		{
			// if RFID reads 3, New Delhi sounds play
			kl.SetActive(false);
			amz.SetActive(false);
			noosa.SetActive(false);
			temple.SetActive(true);
			//New Delhi sounds are playing, all others are not
			Map3 = true;
			Map1 = false;
			Map2 = false;
			Map4 = false;
		}

		if(Map == 4 && Map4 ==false)
		{
			// if RFID reads 4, noosa beach sounds play
			noosa.SetActive(true);
			kl.SetActive(false);
			amz.SetActive(false);
			temple.SetActive(false);
		}
	}
}
