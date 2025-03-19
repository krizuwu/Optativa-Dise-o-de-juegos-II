using UnityEngine;

public class url : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void openurl(string url)
    {
        Application.OpenURL(url);
    }
}

