using UnityEditor;
using UnityEngine;
using UnityEditor.Animations;

public class CreateCharacterAnimator : EditorWindow
{
    AnimationClip idleClip;
    AnimationClip walkClip;
    string controllerPath = "Assets/CharacterController.controller";

    [MenuItem("Tools/Create Character Animator")]
    static void ShowWindow()
    {
        GetWindow<CreateCharacterAnimator>("Create Animator");
    }

    void OnGUI()
    {
        GUILayout.Label("Create a simple Animator Controller (Idle/Walk)", EditorStyles.boldLabel);
        idleClip = (AnimationClip)EditorGUILayout.ObjectField("Idle Clip", idleClip, typeof(AnimationClip), false);
        walkClip = (AnimationClip)EditorGUILayout.ObjectField("Walk Clip", walkClip, typeof(AnimationClip), false);
        controllerPath = EditorGUILayout.TextField("Controller Path", controllerPath);

        if (GUILayout.Button("Create Animator Controller"))
        {
            CreateController();
        }
    }

    void CreateController()
    {
        if (idleClip == null || walkClip == null)
        {
            if (!EditorUtility.DisplayDialog("Missing Clips", "Please assign both Idle and Walk animation clips.", "OK")) return;
        }

        var controller = AnimatorController.CreateAnimatorControllerAtPath(controllerPath);
        controller.AddParameter("Speed", AnimatorControllerParameterType.Float);

        var sm = controller.layers[0].stateMachine;

        var idleState = sm.AddState("Idle");
        idleState.motion = idleClip;

        var walkState = sm.AddState("Walk");
        walkState.motion = walkClip;

        var toWalk = idleState.AddTransition(walkState);
        toWalk.hasExitTime = false;
        toWalk.duration = 0.12f;
        toWalk.AddCondition(AnimatorConditionMode.Greater, 0.1f, "Speed");

        var toIdle = walkState.AddTransition(idleState);
        toIdle.hasExitTime = false;
        toIdle.duration = 0.08f;
        toIdle.AddCondition(AnimatorConditionMode.Less, 0.1f, "Speed");

        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = controller;
        Debug.Log("Animator Controller created at: " + controllerPath);
    }
}
