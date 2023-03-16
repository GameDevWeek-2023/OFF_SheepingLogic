using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{

    public (int, int) grid_position;

    public float building_height;

    public int building_cost;

    public AudioClip build_completed_clip;

    public bool hasAudioLoop=false;

    public bool destrucible=true;

    public virtual void PlayBuildSound()
    {
        GetComponent<AudioSource>().PlayOneShot(build_completed_clip);
        if (hasAudioLoop) GetComponent<AudioSource>().PlayScheduled(AudioSettings.dspTime + build_completed_clip.length + 0.1f); 
    }

}
