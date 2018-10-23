using UnityEngine;

public class PointRipple : MonoBehaviour
{
	public Material m_Mat;
	public string m_RippleMaskName = "TransparentFX";
	public Shader m_SdrRippleMask;
	public bool m_EnableMaskObjects = false;
	public bool m_ShowInternalMaps = false;
	public bool m_AntiAliasing = false;
	[System.Serializable] public class Ripple
	{
		public float Frequence = 60f;
		public float Speed = 30f;
		public float Strength = 1f;
		public float WaveWidth = 0.3f;
		[Range(0.1f, 1f)] public float Spread = 0.3f;
	}
	[Header("Ripple Effect")]
	public Ripple m_Ripple;
	
	public class RippleClick
	{
		public float PosX = 0.5f;
		public float PosY = 0.5f;
		public float Progress = 1f;
		public float Strength = 0f;  // ripple2 need
	}
	RippleClick[] m_RippleClick = new RippleClick[3];
	int m_CurrentRippleClick = 0;
	Camera m_RTCam;
	RenderTexture m_RTRippleMask;
    Camera m_Camera;

    Vector3 ripple = Vector3.zero;
	
	void Start ()
	{
		int n = m_RippleClick.Length;
		for (int i = 0; i < n; i++)
			m_RippleClick[i] = new RippleClick();
		
		// prepare ripple mask
		if (m_Camera == null)
			m_Camera = Camera.main;
		m_RTCam = new GameObject().AddComponent<Camera> ();
		m_RTCam.name = "Ripple Mask Camera";
		m_RTCam.transform.parent = m_Camera.gameObject.transform;
        m_RTCam.enabled = false;
		m_RTRippleMask = new RenderTexture (Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
		m_RTRippleMask.name = "Ripple Mask";
	}

    void Update ()
	{
		if (m_AntiAliasing)
			QualitySettings.antiAliasing = 8;
		else
			QualitySettings.antiAliasing = 0;

		if (ripple != Vector3.zero)
		{
			m_RippleClick[m_CurrentRippleClick].PosX = ripple.x;
			m_RippleClick[m_CurrentRippleClick].PosY = ripple.y;
			m_RippleClick[m_CurrentRippleClick].Progress = 0f;
            print(m_RippleClick[m_CurrentRippleClick].PosX + "   " + m_RippleClick[m_CurrentRippleClick].PosY);
			m_CurrentRippleClick++;
            m_CurrentRippleClick = (m_CurrentRippleClick >= 3) ? 0 : m_CurrentRippleClick;
            ripple = Vector3.zero;
		}
			
		// build ripple mask
		if (m_EnableMaskObjects)
		{
			m_RTCam.CopyFrom (m_Camera);
			m_RTCam.clearFlags = CameraClearFlags.Color;
			m_RTCam.backgroundColor = Color.black;
			m_RTCam.targetTexture = m_RTRippleMask;
			m_RTCam.cullingMask = 1 << LayerMask.NameToLayer (m_RippleMaskName);
			m_RTCam.RenderWithShader (m_SdrRippleMask, "");
			m_Mat.EnableKeyword ("SR_MASK");
			m_Mat.SetTexture ("_MaskTex", m_RTRippleMask);
		}
		else
		{
			m_Mat.DisableKeyword ("SR_MASK");
		}
			
		// update click
		int n = m_RippleClick.Length;
		for (int i = 0; i < n; i++)
		{
			m_RippleClick[i].Progress += 0.01f;
			m_RippleClick[i].Strength = Mathf.Max(m_RippleClick[i].Strength, 0f);
		}
		m_Mat.SetFloat ("_Frequence", m_Ripple.Frequence);  
		m_Mat.SetFloat ("_Speed", m_Ripple.Speed);
		m_Mat.SetFloat ("_Strength", m_Ripple.Strength);
		m_Mat.SetFloat ("_WaveWidth", m_Ripple.WaveWidth);
		m_Mat.SetFloat ("_CurWaveDis", m_RippleClick[0].Progress);
		m_Mat.SetFloat ("_Spread", m_Ripple.Spread);
			
		// send click data to ripple shader
		m_Mat.SetVector ("_Ripple3", new Vector4 (m_RippleClick[2].PosX, m_RippleClick[2].PosY, m_RippleClick[2].Progress, 0));
	}
	void OnRenderImage (RenderTexture src, RenderTexture dst)
	{
		Graphics.Blit (src, dst, m_Mat);
	}
	void OnGUI ()
	{
		if (m_ShowInternalMaps)
			GUI.DrawTextureWithTexCoords (new Rect (10, 10, 128, 128), m_RTRippleMask, new Rect (0, 0, 1, 1));
	}

    public void AddRipples(Vector3 position)
    {
        ripple = position;
    }
}
