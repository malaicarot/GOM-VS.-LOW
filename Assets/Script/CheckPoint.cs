using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] GameObject[] dragonEyes;


    public void GlowingEyes()
    {
        foreach (GameObject eye in dragonEyes)
        {
            Color color = Color.yellow;
            Material material = eye.GetComponent<Renderer>().material;
            material.SetColor("_EmissiveColor", color * 50);
        }
    }
}
