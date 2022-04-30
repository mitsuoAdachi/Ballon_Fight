using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VirtualFlootingObject : MonoBehaviour
{
    public float moveTime,moveRange;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOMoveY(transform.position.y-moveRange,moveTime).SetEase(Ease.Linear).SetLoops(-1,LoopType.Yoyo).SetLink(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
