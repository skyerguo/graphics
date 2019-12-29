using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class RealImage : MonoBehaviour
{
	// Use this for initialization
	void Start () {
		AddCamera();
		AddLight();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	//添加主相机
	void AddCamera()
	{
		if (Camera.main == null)
		{	//场景中添加main camera
			GameObject cameraObj = new GameObject()
			{
				name = "Main Camera", tag = "MainCamera"
			};
			cameraObj.AddComponent<Camera>();
			cameraObj.transform.position = new Vector3(-7.288312f, 8.399572f, 12.43243f); //设置相机位置
			cameraObj.transform.eulerAngles = new Vector3(30.424f, 151.982f, 0);
			Camera.main.fieldOfView = 48;
		}
	}

	void AddLight()
	{
		//添加平行光
		GameObject lightObj = new GameObject("Directional Liaght");
		Light light = lightObj.AddComponent<Light>();
		light.type = LightType.Directional;
		light.shadows = LightShadows.Soft;
		lightObj.transform.position = new Vector3(5.9f, -0.93f, -3.8f);
		lightObj.transform.eulerAngles = new Vector3(46, -58, -21);
	}
}
