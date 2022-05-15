using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] audioSources;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public IEnumerator PlayBGM(int index)
    {
        if (index != 0)
        {
            //Debug.Log("前の曲のボリューム下げる");
            audioSources[index - 1].DOFade(0, 0.75f);
        }

        if (index == 3)
        {
            audioSources[index - 2].DOFade(0, 0.75f);
        }
        yield return new WaitForSeconds(0.45f);

        audioSources[index].Play();

        audioSources[index].DOFade(0.1f, 0.75f);

        if (index != 0) {
            // 前に流れていた曲のボリュームが0になったら
            yield return new WaitUntil(() => audioSources[index - 1].volume == 0);

            audioSources[index - 1].Stop();

        }
    }
}
