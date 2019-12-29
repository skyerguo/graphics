using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [RequireComponent(typeof(AudioSource))]
public class MusicShowDemo : MonoBehaviour{
	 private AudioSource audioSource;
	 private float[] samples = new float[128]; //存放频谱数据
	 //private LineRenderer lineRender; //频谱画线

	 public GameObject cubePrefab; //预制体

	 private Transform[] cubeTransforms; //生成与频谱等量的预制体组

	 private Vector3 middleCubePosition; //临时保存与频谱计算的位置

	 private Transform middleTran;
	// Use this for initialization
	void Start ()
	{ //初始化
		GameObject CubeTmp; //生成预制体
		audioSource = GetComponent<AudioSource>(); //获取声源
		//lineRender = GetComponent<LineRenderer>();
		//lineRender.positionCount = samples.Length; //线的长度与频谱个数也保持一致
		cubeTransforms = new Transform[samples.Length]; //创建了与频谱个数等量的预制体组
		
		transform.position = new Vector3(-samples.Length*0.5f,
			transform.position.y, transform.position.z); //设置初始位置x,y,z

		for (int i = 0; i < samples.Length; i++)
		{ //对每个预制体进行设置
			CubeTmp = Instantiate(cubePrefab, new Vector3(transform.position.x + i + i,
				transform.position.y, transform.position.z), Quaternion.identity);	//参数：object,vector3,quaternion
																					//复制实例化对象，在x轴上排开；identity表示无旋转，完全对齐
			CubeTmp.transform.parent = transform; //设置transform父对象
			cubeTransforms[i] = CubeTmp.transform; //保存每个预制体的初始transform
		}
	}
	
	// Update is called once per frame
	void Update ()
	{ //每一帧刷新时调用
		audioSource.GetSpectrumData(samples, 0, FFTWindow.BlackmanHarris); //获得音频的频谱数据，存到samples中
		
		for(int i = 0; i < samples.Length; i++)
		{ //对每一个预制体进行更新
			middleCubePosition.Set(cubeTransforms[i].position.x,
				Mathf.Clamp(samples[i] * (50 + i * i * 0.5f), 0, 100),
				cubeTransforms[i].position.z); //设置临时计算出的Vector3的x、y、z；
												//x,z不变，y根据频谱变化，
												//clamp()限制value值在min~max之间
			//lineRender.SetPosition(i, middleCubePosition - Vector3.up); //设置Line位置，将index为i处的y值设置为 预制体-1
			
			if (cubeTransforms[i].localScale.y < middleCubePosition.y)
			{ //新的位置在上一帧位置上方，取新的位置
				//cubeTransforms[i].position = middleCubePosition;
				//cubeTransforms[i].localScale += new Vector3(0, Mathf.Clamp(samples[i] * (50 + i * i * 0.5f),0,50), 0);
				cubeTransforms[i].localScale = new Vector3(1, Mathf.Clamp(samples[i] * (50 + i * i * 0.5f),0,100), 1);
				//cubeTransforms[i].Translate(0, Mathf.Clamp(samples[i] * (50 + i * i * 0.5f),0,100), 0);
			}
			else if (cubeTransforms[i].localScale.y > middleCubePosition.y)
			{ //新的位置在上一帧位置的下方，当前位置的y-0.5
				//cubeTransforms[i].position -= new Vector3(0, 0.5f, 0);
				//cubeTransforms[i].localScale += new Vector3(0, -(Mathf.Clamp(samples[i] * (50 + i * i * 0.5f),0,50)), 0);
				cubeTransforms[i].localScale = new Vector3(1, cubeTransforms[i].localScale.y*0.8f, 1);
				//Debug.Log(cubeTransforms[i].localScale.y);
				//Debug.Log("Test");
				//cubeTransforms[i].Translate(0, -Mathf.Clamp(samples[i] * (50 + i * i * 0.5f),0,100), 0);
			}

			//middleTran.localScale += new Vector3(0, Mathf.Clamp(samples[i] * (50 + i * i * 0.5f), 0);
		}
	}
}
