using UnityEngine;
using DG.Tweening;

public class CoinMovement : MonoBehaviour
{
    public float rotationSpeed = 180f;           
    public float positionWaveAmplitude = 0.3f;  
    public float positionWaveDuration = 1f;      
    public float waveDelay = 0.1f;          

    private void Start()
    {
        AnimateCoins();
    }

    private void AnimateCoins()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform coin = transform.GetChild(i);

            //Apply delayed effect for the wave effect
            float initialDelay = i * waveDelay;

            coin.DOLocalRotate(new Vector3(0, 360, 0), 1 / rotationSpeed, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Incremental)  
                .SetDelay(initialDelay);

            //Up and down movement
            coin.DOLocalMoveY(coin.localPosition.y + positionWaveAmplitude, positionWaveDuration)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo)
                .SetDelay(initialDelay);
        }
    }
}
