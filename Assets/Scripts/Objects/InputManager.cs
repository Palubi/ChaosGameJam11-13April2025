using UnityEngine;

public class InputManager : MonoBehaviour
{
    //Singleton
    public static InputManager Instance { get; private set; } = null;

    //ControllsMapping
    private ControllsMapping controllsMapping = null;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            controllsMapping = new ControllsMapping();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        controllsMapping.Enable();
    }

    public ControllsMapping GetControllsMapping()
    {
        return controllsMapping;
    }
}