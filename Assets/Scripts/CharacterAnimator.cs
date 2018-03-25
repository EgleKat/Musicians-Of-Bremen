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

    private Animator animator;
    private AnimatorOverrideController animatorOverrideController;

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
    public float frameRate;

    private void Awake()
    {
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

            ObjectReferenceKeyframe[] spriteKeyFrames = new ObjectReferenceKeyframe[framesPerDirection*2 - 2];
            int frame = 0;
            for (int spriteNum = 0; spriteNum < framesPerDirection; spriteNum++)
            {
                spriteKeyFrames[frame] = new ObjectReferenceKeyframe
                {
                    time = frame*0.25f,
                    value = allSprites[spriteOffset + directionIndex.Value * spritesPerRow + spriteNum]
                };
                frame++;
            }
            for (int spriteNum = framesPerDirection - 2; spriteNum > 0; spriteNum--)
            {
                // Play in reverse
                spriteKeyFrames[frame] = new ObjectReferenceKeyframe
                {
                    time = frame * 0.25f,
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
