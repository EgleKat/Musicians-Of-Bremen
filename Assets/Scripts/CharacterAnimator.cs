using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{

    public string spriteSheetName;
    public int spriteOffset;
    public int spritesPerRow;
    public int framesPerDirection;
    public int idleFrameOffset;

    private Animation animationComponent;
    private SpriteRenderer spriteRendererComponent;

    private Dictionary<Vector3, int> directionClips = new Dictionary<Vector3, int>
    {
        {Vector3.down, 0},
        {Vector3.left, 1},
        {Vector3.right, 2},
        {Vector3.up, 3},
    };
    private Dictionary<Vector3, Sprite> directionIdleSprites = new Dictionary<Vector3, Sprite>
    {
        {Vector3.down, null},
        {Vector3.left, null},
        {Vector3.right, null},
        {Vector3.up, null},
    };

    private Sprite[] allSprites;

    private Vector3 currentDirection = Vector3.right;

    private void Awake()
    {
        allSprites = Resources.LoadAll<Sprite>(spriteSheetName);
        animationComponent = gameObject.AddComponent<Animation>();
        spriteRendererComponent = GetComponent<SpriteRenderer>();
        EventManager.AddListener(EventType.MoveAnimation, OnMoveAnimationCommand);
        EventManager.AddListener(EventType.StopMoveAnimation, OnStopMoveAnimationCommand);
        CreateAnimationClips();
    }

    private void OnMoveAnimationCommand(object moveCommand)
    {
        MoveCommand command = (MoveCommand)moveCommand;
        if (command.gameObjectToMove == gameObject.name)
        {
            currentDirection = command.vec.Value;
            animationComponent.Play(directionClips[currentDirection].ToString());
        }
    }

    private void OnStopMoveAnimationCommand(object moveCommand)
    {
        MoveCommand command = (MoveCommand)moveCommand;
        if (command.gameObjectToMove == gameObject.name)
        {
            animationComponent.Stop();
            spriteRendererComponent.sprite = directionIdleSprites[currentDirection];
        }
    }

    private void CreateAnimationClips()
    {
        foreach (KeyValuePair<Vector3, int> directionIndex in directionClips)
        {
            AnimationClip directionClip = new AnimationClip
            {
                wrapMode = WrapMode.PingPong,
                legacy = true,
                frameRate = framesPerDirection
            };
            EditorCurveBinding spriteBinding = new EditorCurveBinding
            {
                type = typeof(SpriteRenderer),
                path = "",
                propertyName = "m_Sprite"
            };

            ObjectReferenceKeyframe[] spriteKeyFrames = new ObjectReferenceKeyframe[framesPerDirection];
            for (int frame = 0; frame < framesPerDirection; frame++)
            {
                spriteKeyFrames[frame] = new ObjectReferenceKeyframe
                {
                    time = frame,
                    value = allSprites[spriteOffset + directionIndex.Value * spritesPerRow + frame]
                };
            }
            AnimationUtility.SetObjectReferenceCurve(directionClip, spriteBinding, spriteKeyFrames);

            animationComponent.AddClip(directionClip, directionIndex.Value.ToString());
            directionIdleSprites[directionIndex.Key] = allSprites[spriteOffset + directionIndex.Value * spritesPerRow + idleFrameOffset];
        }
    }
}
