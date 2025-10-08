using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Selections : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public string heroName;

    public Animator animator;

    public GameObject character;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = transform.parent.GetComponent<Animator>();

        if (heroName == "speedHero")
        {
            character = Resources.Load<GameObject>("prefabs/Player/Player");
        }

        if (heroName == "powerHero")
        {
            character = Resources.Load<GameObject>("prefabs/Player/Player2");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.instance.SetPlayerCharacter(character);

        SceneManager.LoadScene("Level1");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetBool(heroName, true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool(heroName, false);
    }
}