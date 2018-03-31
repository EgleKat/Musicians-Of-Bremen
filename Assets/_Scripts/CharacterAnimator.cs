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

    public Vector3[] directionOrder = {
        Vector3.down,
        Vector3.left,
        Vector3.right,
        Vector3.up,
    };

    private Animator animator;
    private AnimatorOverrideController animatorOverrideController;

    private SpriteRenderer spriteRendererComponent;

    private Dictionary<Vector3, int> directionClips;
    private Dictionary<Vector3, Sprite> directionIdleSprites;

    private Sprite[] allSprites;

    private Vector3 currentDirection = Vector3.right;
    public float frameRate;

    private void Awake()
    {
        directionClips = new Dictionary<Vector3, int>
        {
            {directionOrder[0], 0},
            {directionOrder[1], 1},
            {directionOrder[2], 2},
            {directionOrder[3], 3},
        };

        directionIdleSprites = new Dictionary<Vector3, Sprite>
        {
            {directionOrder[0], null},
            {directionOrder[1], null},
            {directionOrder[2], null},
            {directionOrder[3], null},
        };

        allSprites = Resources.LoadAll<Sprite>(spriteSheetName);

        animator = gameObject.GetComponent<Animator>();
        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = animatorOverrideController;

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
            if (currentDirection != command.vec.Value || !animator.enabled)
            {
                if (command.vec.Value == Vector3.zero)
                {
                    OnStopMoveAnimationCommand(command);
                    return;
                }
                animator.enabled = true;
                currentDirection = command.vec.Value;
                animator.Play(directionClips[currentDirection].ToString());
            }

        }
    }

    private void OnStopMoveAnimationCommand(object moveCommand)
    {
        MoveCommand command = (MoveCommand)moveCommand;
        if (command.gameObjectToMove == gameObject.name)
        {
            animator.enabled = false;
            spriteRendererComponent.sprite = directionIdleSprites[currentDirection];
        }
    }

    private void CreateAnimationClips()
    {
        foreach (KeyValuePair<Vector3, int> directionIndex in directionClips)
        {
            AnimationClip directionClip = new AnimationClip
            {
                frameRate = frameRate,
            };

            AnimationClipSettings animationClipSettings = AnimationUtility.GetAnimationClipSettings(directionClip);
            animationClipSettings.loopTime = true;
            AnimationUtility.SetAnimationClipSettings(directionClip, animationClipSettings);

            EditorCurveBinding spriteBinding = new EditorCurveBinding
            {
                type = typeof(SpriteRenderer),
                path = "",
                propertyName = "m_Sprite"
            };

            ObjectReferenceKeyframe[] spriteKeyFrames = new ObjectReferenceKeyframe[framesPerDirection * 2 - 2];
            int frame = 0;
            for (int spriteNum = 0; spriteNum < framesPerDirection; spriteNum++)
            {
                spriteKeyFrames[frame] = new ObjectReferenceKeyframe
                {
                    time = (float)frame / frameRate,
                    value = allSprites[spriteOffset + directionIndex.Value * spritesPerRow + spriteNum]
                };
                frame++;
            }
            for (int spriteNum = framesPerDirection - 2; spriteNum > 0; spriteNum--)
            {
                // Play in reverse
                spriteKeyFrames[frame] = new ObjectReferenceKeyframe
                {
                    time = (float)frame / frameRate,
                    value = allSprites[spriteOffset + directionIndex.Value * spritesPerRow + spriteNum]
                };
                frame++;
            }
            AnimationUtility.SetObjectReferenceCurve(directionClip, spriteBinding, spriteKeyFrames);

            animatorOverrideController[directionIndex.Value.ToString()] = directionClip;
            directionIdleSprites[directionIndex.Key] = allSprites[spriteOffset + directionIndex.Value * spritesPerRow + idleFrameOffset];
        }
    }
}
