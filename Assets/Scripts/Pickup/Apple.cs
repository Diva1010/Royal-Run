using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Apple : Pickup
{
    [SerializeField] float adjustChangeMoveSpeed = 3f;
    LevelGenerator levelGenerator;

    public void Init(LevelGenerator levelGenerator) {
        this.levelGenerator = levelGenerator;
    }

    protected override void OnPickup()
    {
        levelGenerator.ChangeChunkMoveSpeed(adjustChangeMoveSpeed);
    }
}

