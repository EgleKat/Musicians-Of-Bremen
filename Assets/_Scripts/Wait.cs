using UnityEngine;
using System.Threading.Tasks;
using System.Collections;

public class Wait : MonoBehaviour
{
    private static Wait wait;

    public static Wait Instance
    {
        get
        {
            if (!wait)
            {
                wait = FindObjectOfType(typeof(Wait)) as Wait;

                if (!wait)
                {
                    Debug.LogError("There needs to be one active Wait script on a GameObject in your scene.");
                }
            }

            return wait;
        }
    }

    public static Task<bool> ForSeconds(float seconds)
    {
        return Instance._ForSeconds(seconds);
    }

    private Task<bool> _ForSeconds(float seconds)
    {
        TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
        StartCoroutine(_ForSecondsCoroutine(seconds, taskCompletionSource));
        return taskCompletionSource.Task;
    }
    private IEnumerator _ForSecondsCoroutine(float seconds, TaskCompletionSource<bool> taskCompletionSource)
    {
        yield return new WaitForSeconds(seconds);
        taskCompletionSource.SetResult(true);
    }
}
