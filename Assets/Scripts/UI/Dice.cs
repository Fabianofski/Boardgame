using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public AudioSource Audio;

    [Header("Input / Parameters")]
    public int maxEyes;
    public float timebtwDice;
    public int Iterations;
    private float IterationsIndex;
    public bool DiceRolled;

    [Header("Animation")]
    public LeanTweenType easeType;
    public AnimationCurve curve;
    private float time;

    [Header("Display UI")]
    private Image image;
    public Sprite[] DiceSprites;
    private GameController gameController;

    [Header("Output")]
    public int SumEyes;

    void Start()
    {
        image = GetComponent<Image>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    public void ButtonRoll()
    {
        if (DiceRolled) return;

        Audio.Play();
        IterationsIndex = 0;
        StartCoroutine(RollDice());
        DiceRolled = true;
        Animate();
    }

    IEnumerator RollDice()
    {
        IterationsIndex++;
        yield return new WaitForSeconds(timebtwDice);
        SumEyes = Random.Range(1, maxEyes + 1);

        if (IterationsIndex <= Iterations)
            StartCoroutine(RollDice());
        else
            StartCoroutine(StartMoving());

        OutputDice();
    }

    IEnumerator StartMoving()
    {
        yield return new WaitForSeconds(1f);
        gameController.StartMove(SumEyes);
    }

    void OutputDice()
    {
        image.sprite = DiceSprites[SumEyes - 1];
    }

    void Animate()
    {
        time = (Iterations + 3) * timebtwDice;
        
        LeanTween.rotateZ(gameObject, -180, time/2).setEase(curve);
        LeanTween.moveLocalY(gameObject, -30, time * 3/4).setEase(easeType);
        LeanTween.moveLocalY(gameObject, -65, time / 4).setEase(easeType).setDelay(time/2).setOnComplete(Reset);
    }

    void Reset()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
        
    }
}
