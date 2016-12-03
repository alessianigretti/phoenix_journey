using UnityEngine;
using System.Collections;

public class PlayerEntrance : MonoBehaviour
{
    public GameObject mesh;
    public GameObject player;

    void Start()
    {
       StartCoroutine("WaitForAnimation");
    }

    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        mesh.SetActive(false);
        player.SetActive(true);
    }
}
