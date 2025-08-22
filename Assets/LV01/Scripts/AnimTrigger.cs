using UnityEngine;

public class AnimTrigger : MonoBehaviour
{

    Player player;
    void Awake()
    {
        player = FindAnyObjectByType<Player>();
    }

    public void CallTrig()
    {
        player.CallAnimTrig();
    }
}
