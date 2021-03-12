using UnityEngine;

#if !UNITY_WEBGL
namespace Pashmak.Core.CU._UnityEngine._Microphone
{
    /// <summary>
    /// Working with device microphones.
    /// </summary>
    public class CU_Microphone : CU_Component
    {
        // variable________________________________________________________________
        /// <summary>
        /// If microphone wasn't detected, this clip will be assigned to the audioSource.
        /// </summary>
        [SerializeField] private AudioClip m_defaultClip = null;
        /// <summary>
        /// This variable get the recorded clip from microphone.
        /// </summary>
        [SerializeField] private AudioSource m_audioSource;
        /// <summary>
        /// The name of the device.
        /// </summary>
        [SerializeField] private string m_deviceName = "Built -in Microphone";
        /// <summary>
        /// Indicates whether the recording should continue recording if lengthSec is reached,
        /// and wrap around and record from the beginning of the AudioClip.
        /// </summary>
        [SerializeField] private bool m_loop = false;
        /// <summary>
        /// Is the length of the AudioClip produced by the recording.
        /// </summary>
        [SerializeField] private int m_lengthSec = 2;
        /// <summary>
        /// The sample rate of the AudioClip produced by the recording.
        /// </summary>
        [SerializeField] private int m_frequency = 44100;


        // property________________________________________________________________
        public AudioClip DefaultClip { get => m_defaultClip; set => m_defaultClip = value; }
        public AudioSource AudioSource { get => m_audioSource; set => m_audioSource = value; }
        public string DeviceName { get => m_deviceName; set => m_deviceName = value; }
        public bool Loop { get => m_loop; set => m_loop = value; }
        public int LengthSec { get => m_lengthSec; set => m_lengthSec = value; }
        public int Frequency { get => m_frequency; set => m_frequency = value; }


        // monoBehaviour___________________________________________________________
        private void Awake()
        {
            // Check AudioSource component.
            if (AudioSource == null)
                AudioSource = GetComponent<AudioSource>();
            if (AudioSource == null)
                Debug.LogError("There is no AudioSource to get recorded clip on it.");
        }
        private void Update()
        {
            if (!IsActive) return;
            if (AudioSource.clip)
            {
                print("length: " + AudioSource.clip.length);
                print("samples: " + AudioSource.clip.samples);
                print("frequency: " + AudioSource.clip.frequency);
            }
        }


        // function________________________________________________________________
        /// <summary>
        /// Start Recording with device.
        /// </summary>
        public void StartRecord()
        {
            if (!IsActive) return;
            AudioSource.clip = Microphone.Start(DeviceName, Loop, LengthSec, Frequency);
            if (AudioSource.clip == null)
            {
                AudioSource.clip = DefaultClip;
            }
        }
        /// <summary>
        /// Stops recording.
        /// </summary>
        public void EndRecord()
        {
            Microphone.End(DeviceName);
        }
        /// <summary>
        /// Stops recording.
        /// </summary>
        /// <param name="deviceName">Name of device.</param>
        public void EndRecord(string deviceName)
        {
            Microphone.End(deviceName);
        }
    }
}
#endif