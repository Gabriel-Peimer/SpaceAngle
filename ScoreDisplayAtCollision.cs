using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplayAtCollision : MonoBehaviour
{
    public Text scoreAtCollision;
    public RandomGeneratingObstacles generator;

    public void TextUpdate()
    {
        scoreAtCollision.text = Convert.ToString(generator.score);
    }
}
