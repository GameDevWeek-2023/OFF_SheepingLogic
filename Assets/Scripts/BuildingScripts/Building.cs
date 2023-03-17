using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

    public (int, int) grid_position;

    public float building_height =0;

    public int building_cost =0;

    public int powerConsumption =0;

    public AudioClip build_completed_clip;

    public bool hasAudioLoop=false;

    public bool destrucible=true;

    public virtual void PlayBuildSound()
    {
        GetComponent<AudioSource>().PlayOneShot(build_completed_clip);
        if (hasAudioLoop) GetComponent<AudioSource>().PlayScheduled(AudioSettings.dspTime + build_completed_clip.length + 0.1f); 
    }

}
