using UnityEngine;

namespace _0.Game.Scripts
{
   public class AudioManager : MonoBehaviour
    {
        public static AudioManager ins;

        [Header("Music")]
        public AudioClip musicClip;

        [Header("Button SFX")]
        public AudioClip clickClip;
        public AudioClip wrong;
        public AudioClip right;
        public AudioClip collider;
        public AudioClip jump;
        public AudioClip win;
        public AudioClip lose;
        private AudioSource _musicSrc;
        private AudioSource _sfxSrc;
        private AudioSource _sfxCollider;

        private void Awake()
        {
            if (ins != null && ins != this) { Destroy(gameObject); return; }
            ins = this;
            DontDestroyOnLoad(gameObject);

            _musicSrc = gameObject.AddComponent<AudioSource>();
            _musicSrc.loop = true;
            
            _sfxSrc = gameObject.AddComponent<AudioSource>();
            _sfxCollider = gameObject.AddComponent<AudioSource>();
            _sfxSrc.loop = false;
            _sfxCollider.loop = false;
            
            SetSfxVolume(PlayerData.SfxVolume);
            SetMusicVolume(PlayerData.MusicVolume);
        }

        private void Start()
        {
            if (musicClip != null) PlayMusic(musicClip, true);
        }

        // ===== Music =====
        public void PlayMusic(AudioClip clip, bool loop = true)
        {
            if (clip == null) return;
            _musicSrc.clip = clip;
            _musicSrc.loop = loop;
            _musicSrc.volume = PlayerData.MusicVolume;
            _musicSrc.Play();
        }

        public void StopMusic() => _musicSrc.Stop();
        public void SetMusicVolume(float v)
        {
            _musicSrc.volume = PlayerData.MusicVolume;
        }

        // ===== SFX =====
        public void PlaySfx(AudioClip clip)
        {
            if (clip == null) return;
            if(_sfxSrc.isPlaying) return; 
            _sfxSrc.PlayOneShot(clip, PlayerData.SfxVolume);
        }

        public void SetSfxVolume(float v)
        {
            _sfxSrc.volume = PlayerData.SfxVolume;
            _sfxCollider.volume = PlayerData.SfxVolume;
        }

        // Nút phổ biến
        public void PlayButtonClick() => PlaySfx(clickClip);
        public void PlayWrong() => PlaySfx(wrong);
        public void PlayRight() => PlaySfx(right);

        public void PlayCollider()
        {
            _sfxCollider.PlayOneShot(collider, PlayerData.SfxVolume);
        }
        public void PlayJump() => PlaySfx(jump);
        public void PlayWin() => PlaySfx(win);
        public void PlayLose() => PlaySfx(lose);
    }
}