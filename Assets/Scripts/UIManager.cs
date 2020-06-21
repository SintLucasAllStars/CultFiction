using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject inventory;

    private void Update()
    {
        if (inventory == null)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenInventory();
            Cursor.lockState = inventory.activeInHierarchy ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = inventory.activeInHierarchy;
        }
    }

    private void OpenInventory()
    {
        inventory.SetActive(!inventory.activeInHierarchy);
    }

    public void OpenScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}