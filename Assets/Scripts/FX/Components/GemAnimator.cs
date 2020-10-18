using DG.Tweening;
using UnityEngine;

public class GemAnimator : MonoBehaviour
{
    public Sequence AnimateSwap(Transform gemA, Transform gemB)
    {
        var sequence = DOTween.Sequence();

        var gemAPosition = gemA.position;
        sequence.Append(gemA.DOMove(gemB.transform.position, .2f));
        sequence.Join(gemB.DOMove(gemAPosition, .2f));

        return sequence;
    }

    public void AnimateSelectionState(bool selectionState)
    {

    }
}
