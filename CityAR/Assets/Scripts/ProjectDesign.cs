using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectDesign : MonoBehaviour {

    public InputField TitleInput;
    public Text FinanceText;
    public Text EnvironmentText;
    public Text SocialText;
    public Text RatingText;
    public Text BudgetText;
    public Text ComponentsText;
    public GameObject FinanceImage;
    public GameObject EnvironmentImage;
    public GameObject SocialImage;
    public GameObject RatingImage;
    public GameObject BudgetImage;
    public GameObject AddValueImage;
    public GameObject SubtractValueImage;
    public GameObject ComponentImage;
    public string Title;
    public int Environment;
    public int Social;
    public int Finance;
    public int Budget;
    public int Rating;
    private int Value = 5;
    private int BudgetValue = 500;
    private int RatingValue = 1;
    public int MaxComponents = 5;

	void Start () {
	}
	
	void Update () {
        EnvironmentText.text = "" + FormatText(Environment);
        FinanceText.text = "" + FormatText(Finance);
        SocialText.text = "" + FormatText(Social);
        RatingText.text = "" + FormatText(Rating);
        BudgetText.text = "" + FormatText(Budget);
        Title = TitleInput.text;
        ComponentsText.text = "" + MaxComponents;
    }   
    public void AddValue(Draggable.DraggableType type)
    {
        if (MaxComponents == 0)
            return;
        StartCoroutine(AnimateIcon(AddValueImage, .7f, 1f));
        StartCoroutine(AnimateIcon(ComponentImage, .7f, 1f));
        switch (type)
        {
            case Draggable.DraggableType.Environment:
                Environment += Value;
                StartCoroutine(AnimateIcon(EnvironmentImage, .7f, .5f));
                break;
            case Draggable.DraggableType.Finance:
                Finance += Value;
                StartCoroutine(AnimateIcon(FinanceImage, .7f, .5f));
                break;
            case Draggable.DraggableType.Social:
                Social += Value;
                StartCoroutine(AnimateIcon(SocialImage, .7f, .5f));
                break;
            case Draggable.DraggableType.Rating:
                Rating += RatingValue;
                StartCoroutine(AnimateIcon(RatingImage, .7f, .5f));
                break;
            case Draggable.DraggableType.Budget:
                Budget += BudgetValue;
                StartCoroutine(AnimateIcon(BudgetImage, .7f, .5f));
                break;
        }
        MaxComponents -= 1;
    }

    public void SubtractValue(Draggable.DraggableType type)
    {
        if (MaxComponents == 5)
            return;
        StartCoroutine(AnimateIcon(SubtractValueImage, .7f, 1f));
        StartCoroutine(AnimateIcon(ComponentImage, 1.2f, 1f));
        switch (type)
        {
            case Draggable.DraggableType.Environment:
                Environment -= Value;
                StartCoroutine(AnimateIcon(EnvironmentImage, .7f, .5f));
                break;
            case Draggable.DraggableType.Finance:
                Finance -= Value;
                StartCoroutine(AnimateIcon(FinanceImage, .7f, .5f));
                break;
            case Draggable.DraggableType.Social:
                Social -= Value;
                StartCoroutine(AnimateIcon(SocialImage, .7f, .5f));
                break;
            case Draggable.DraggableType.Rating:
                Rating -= RatingValue;
                StartCoroutine(AnimateIcon(RatingImage, .7f, .5f));
                break;
            case Draggable.DraggableType.Budget:
                Budget -= BudgetValue;
                StartCoroutine(AnimateIcon(BudgetImage, .7f, .5f));
                break;
        }
        MaxComponents += 1;
    }
    string FormatText(int num)
    {
        string text = "";
        if (num > 0)
            text = "+" + num;
        if (num < 0)
            text = "" + num;
        if (num == 0)
            text = "" + num;
        return text;
    }

    IEnumerator AnimateIcon(GameObject icon, float start, float finish)
    {
        iTween.ScaleTo(icon, iTween.Hash("x", start, "y", start, "time", .2f));
        yield return new WaitForSeconds(.2f);
        iTween.ScaleTo(icon, iTween.Hash("x", finish, "y", finish, "time", .2f));
    }

    public void Reset()
    {
        Finance = 0;
        Social = 0;
        Environment = 0;
        Budget = 0;
        Rating = 0;
        MaxComponents = 5;
    }
}
