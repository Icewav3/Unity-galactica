using System.Collections;
using System.Collections.Generic;
using Content;
using Content.Blocks;
using Unity.VisualScripting;
using UnityEngine;

namespace Mechanics.TurretControllers
{
    public class KineticTurretController : TurretControllerBase
    {
        public AudioClip fire;
        private AudioSource _audioSource;

        protected override void Start()
        {
            base.Start();
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = fire;
        }

        protected override void FireWeapon()
        {
            //play clip, do not layer them
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
            //implement your other firing logic here (if any)
        }
    }
}