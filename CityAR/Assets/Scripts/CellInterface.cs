using UnityEngine;
using System.Collections;
using Boo.Lang;
using TMPro;

public class CellInterface : MonoBehaviour
{

    public enum InterfaceState
    {
        Hide, Grey, White
    }
    public enum TextState
    {
        Grey, White, Changes
    }

    public TextState CurrentTextState;
    public TextMeshPro[] StatusText;
    public TextMeshPro CellName;
    private CellLogic cell;
    public GameObject[] Images;
    public Material WhiteMat;
    public Material GreyMat;

    //Animate Rise of numbers
    public GameObject FinanceRise;
    public GameObject SocialRise;
    public GameObject EnvironmentRise;
    public TextMeshPro FinanceRiseText;
    public TextMeshPro SocialRiseText;
    public TextMeshPro EnvironmentRiseText;
    private float finCurVal;
    private float socCurVal;
    private float envCurVal;
    private int FinRiseVal;
    private int SocRiseVal;
    private int EnvRiseVal;
    private bool animate;
    private float animateT = 5;


    void Start()
    {
        cell = GetComponent<CellLogic>();
        FinanceRise.SetActive(false);
        SocialRise.SetActive(false);
        EnvironmentRise.SetActive(false);
        FinanceRiseText.text = "";
        SocialRiseText.text = "";
        EnvironmentRiseText.text = "";
    }

    void Update()
    {
        if (animate)
        {
            FinanceRiseText.rectTransform.anchoredPosition3D = new Vector3(FinanceRiseText.rectTransform.anchoredPosition3D.x, FinanceRise.transform.position.y * 2 + 3f, FinanceRiseText.rectTransform.anchoredPosition3D.z);
            SocialRiseText.rectTransform.anchoredPosition3D = new Vector3(SocialRiseText.rectTransform.anchoredPosition3D.x, SocialRise.transform.position.y * 2 + 3f, SocialRiseText.rectTransform.anchoredPosition3D.z);
            EnvironmentRiseText.rectTransform.anchoredPosition3D = new Vector3(EnvironmentRiseText.rectTransform.anchoredPosition3D.x, EnvironmentRise.transform.position.y * 2 + 3f, EnvironmentRiseText.rectTransform.anchoredPosition3D.z);
            AnimateText();
        }
    }

    public void HighlightCell(int fin, int soc, int env)
    {
        StopAllCoroutines();
        StartCoroutine(ExpandDisplay(fin, soc, env));
    }

    IEnumerator ExpandDisplay(int finVal, int socVal, int envVal)
    {
        yield return new WaitForSeconds(1f);
        animateT = 5f;
        float elapsedTime = 0;

        animate = true;
        if (CurrentTextState != TextState.Changes)
        {
            ChangeCellDisplay(InterfaceState.Hide);
            ChangeCellText(TextState.Grey);
        }
        Images[0].SetActive(true);
        Images[1].SetActive(true);
        Images[2].SetActive(true);
        FinanceRise.SetActive(true);
        SocialRise.SetActive(true);
        EnvironmentRise.SetActive(true);

        iTween.ScaleTo(FinanceRise, iTween.Hash("y", cell.FinanceRate / 5f, "time", animateT, "easeType", iTween.EaseType.linear));
        iTween.ScaleTo(SocialRise, iTween.Hash("y", cell.SocialRate / 5f, "time", animateT, "easeType", iTween.EaseType.linear));
        iTween.ScaleTo(EnvironmentRise, iTween.Hash("y", cell.EnvironmentRate / 5f, "time", animateT, "easeType", iTween.EaseType.linear));
        iTween.MoveTo(FinanceRise, iTween.Hash("y", cell.FinanceRate / 5f + .5, "time", animateT, "easeType", iTween.EaseType.linear));
        iTween.MoveTo(SocialRise, iTween.Hash("y", cell.SocialRate / 5f + .5, "time", animateT, "easeType", iTween.EaseType.linear));
        iTween.MoveTo(EnvironmentRise, iTween.Hash("y", cell.EnvironmentRate / 5f + .5, "time", animateT, "easeType", iTween.EaseType.linear));

        FinRiseVal = finVal;
        SocRiseVal = socVal;
        EnvRiseVal = envVal;
        while (elapsedTime <= animateT)
        {
            finCurVal = Mathf.Lerp(0, finVal, elapsedTime / animateT);
            socCurVal = Mathf.Lerp(0, socVal, elapsedTime / animateT);
            envCurVal = Mathf.Lerp(0, envVal, elapsedTime / animateT);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(animateT);
        iTween.ScaleTo(FinanceRise, iTween.Hash("y", 0, "time", animateT));
        iTween.ScaleTo(SocialRise, iTween.Hash("y", 0, "time", animateT));
        iTween.ScaleTo(EnvironmentRise, iTween.Hash("y", 0, "time", animateT));
        iTween.MoveTo(FinanceRise, iTween.Hash("y", .5, "time", animateT));
        iTween.MoveTo(SocialRise, iTween.Hash("y", .5, "time", animateT));
        iTween.MoveTo(EnvironmentRise, iTween.Hash("y", .5, "time", animateT));
        if (CurrentTextState != TextState.Changes)
        {
            ChangeCellDisplay(InterfaceState.Grey);
            ChangeCellText(TextState.Grey);
        }
        yield return new WaitForSeconds(5f);

        animate = false;
        FinanceRise.SetActive(false);
        SocialRise.SetActive(false);
        EnvironmentRise.SetActive(false);
        FinanceRiseText.text = "";
        SocialRiseText.text = "";
        EnvironmentRiseText.text = "";
    }

    void AnimateText()
    {
        if (FinRiseVal > 0)
            FinanceRiseText.text = "<color=white>" + (cell.FinanceRate - FinRiseVal) + "</color>" + "<color=green><b>+" + Mathf.RoundToInt(finCurVal) + "</color></b>";
        if (SocRiseVal > 0)
            SocialRiseText.text = "<color=white>" + (cell.SocialRate - SocRiseVal) + "</color>" + "<color=green><b>+" + Mathf.RoundToInt(socCurVal) + "</color></b>";
        if (EnvRiseVal > 0)
            EnvironmentRiseText.text = "<color=white>" + (cell.EnvironmentRate - EnvRiseVal) + "</color>" + "<color=green><b>+" + Mathf.RoundToInt(envCurVal) + "</color></b>";
        if (FinRiseVal < 0)
            FinanceRiseText.text = "<color=white>" + (cell.FinanceRate - FinRiseVal) + "</color>" + "<color=red><b>" + Mathf.RoundToInt(finCurVal) + "</color>";
        if (SocRiseVal < 0)
            SocialRiseText.text = "<color=white>" + (cell.SocialRate - SocRiseVal) + "</color>" + "<color=red><b>" + Mathf.RoundToInt(socCurVal) + "</color>";
        if (EnvRiseVal < 0)
            EnvironmentRiseText.text = "<color=white>" + (cell.EnvironmentRate - EnvRiseVal) + "</color>" + "<color=red><b>" + Mathf.RoundToInt(envCurVal) + "</color>";
        if (FinRiseVal == 0)
            FinanceRiseText.text = "<color=white>" + cell.FinanceRate + "</color>";
        if (SocRiseVal == 0)
            SocialRiseText.text = "<color=white>" + cell.SocialRate + "</color>";
        if (EnvRiseVal == 0)
            EnvironmentRiseText.text = "<color=white>" + cell.EnvironmentRate + "</color>";
    }

    public void ChangeCellText(TextState state)
    {
        CurrentTextState = state;
        switch (state)
        {
            case TextState.Changes:
                int fin = ProjectManager.Instance.GetFinanceInt(ProjectManager.Instance.CurrentDummy.Id_CSV);
                int soc = ProjectManager.Instance.GetSocialInt(ProjectManager.Instance.CurrentDummy.Id_CSV);
                int env = ProjectManager.Instance.GetEnvironmentInt(ProjectManager.Instance.CurrentDummy.Id_CSV);
                CellName.text = "<color=white>Area " + cell.CellId + "</color>";
                if (fin >= 0)
                    StatusText[0].text = "<color=white>" + cell.FinanceRate + "</color>" + " <color=green><b>+" + fin + "</color></b>";
                if (soc >= 0)
                    StatusText[1].text = "<color=white>" + cell.SocialRate + "</color>" + " <color=green><b>+" + soc + "</color></b>";
                if (env >= 0)
                    StatusText[2].text = "<color=white>" + cell.EnvironmentRate + "</color>" + " <color=green><b>+" + env + "</color></b>";
                if (fin < 0)
                    StatusText[0].text = "<color=white>" + cell.FinanceRate + "</color>" + " <color=red><b>" + fin + "</color>";
                if (soc < 0)
                    StatusText[1].text = "<color=white>" + cell.SocialRate + "</color>" + " <color=red><b>" + soc + "</color>";
                if (env < 0)
                    StatusText[2].text = "<color=white>" + cell.EnvironmentRate + "</color>" + " <color=red><b>" + env + "</color>";
                break;
            case TextState.Grey:
                CellName.text = "<color=#c0c0c0ff>Area " + cell.CellId + "</color>";
                StatusText[0].text = "<color=#c0c0c0ff>" + cell.FinanceRate + "</color>";
                StatusText[1].text = "<color=#c0c0c0ff>" + cell.SocialRate + "</color>";
                StatusText[2].text = "<color=#c0c0c0ff>" + cell.EnvironmentRate + "</color>";
                break;
            case TextState.White:
                CellName.text = "<color=white>Area " + cell.CellId + "</color>";
                StatusText[0].text = "<color=white>" + cell.FinanceRate + "</color>";
                StatusText[1].text = "<color=white>" + cell.SocialRate + "</color>";
                StatusText[2].text = "<color=white>" + cell.EnvironmentRate + "</color>";
                break;
        }
    }

    public void ChangeCellDisplay(InterfaceState state)
    {
        switch (state)
        {
            //ENABLE TEXT AND ICONS ONLY FOR PLAYER ROLE
            case InterfaceState.Hide:
                CellName.gameObject.SetActive(false);
                Images[0].SetActive(false);
                Images[1].SetActive(false);
                Images[2].SetActive(false);
                StatusText[0].gameObject.SetActive(false);
                StatusText[1].gameObject.SetActive(false);
                StatusText[2].gameObject.SetActive(false);
                break;
            case InterfaceState.Grey:
                CellName.gameObject.SetActive(true);
                Images[0].GetComponent<SpriteRenderer>().color = Color.grey;
                Images[1].GetComponent<SpriteRenderer>().color = Color.grey;
                Images[2].GetComponent<SpriteRenderer>().color = Color.grey;

                switch (LocalManager.Instance.RoleType)
                {
                    case Vars.Player1:
                        Images[0].SetActive(true);
                        Images[1].SetActive(false);
                        Images[2].SetActive(false);
                        StatusText[0].gameObject.SetActive(true);
                        StatusText[1].gameObject.SetActive(false);
                        StatusText[2].gameObject.SetActive(false);

                        break;
                    case Vars.Player2:
                        Images[0].SetActive(false);
                        Images[1].SetActive(true);
                        Images[2].SetActive(false);
                        StatusText[0].gameObject.SetActive(false);
                        StatusText[1].gameObject.SetActive(true);
                        StatusText[2].gameObject.SetActive(false);
                        break;
                    case Vars.Player3:
                        Images[0].SetActive(false);
                        Images[1].SetActive(false);
                        Images[2].SetActive(true);
                        StatusText[0].gameObject.SetActive(false);
                        StatusText[1].gameObject.SetActive(false);
                        StatusText[2].gameObject.SetActive(true);
                        break;
                }
                break;
            case InterfaceState.White:
                //ENABLE ALL TEXT AND ICONS
                CellName.gameObject.SetActive(true);
                Images[0].GetComponent<SpriteRenderer>().color = Color.white;
                Images[1].GetComponent<SpriteRenderer>().color = Color.white;
                Images[2].GetComponent<SpriteRenderer>().color = Color.white;
                switch (LocalManager.Instance.RoleType)
                {
                    case Vars.Player1:
                        Images[0].SetActive(true);
                        Images[1].SetActive(true);
                        Images[2].SetActive(true);
                        StatusText[0].gameObject.SetActive(true);
                        StatusText[1].gameObject.SetActive(true);
                        StatusText[2].gameObject.SetActive(true);
                        break;
                    case Vars.Player2:

                        Images[0].SetActive(true);
                        Images[1].SetActive(true);
                        Images[2].SetActive(true);
                        StatusText[0].gameObject.SetActive(true);
                        StatusText[1].gameObject.SetActive(true);
                        StatusText[2].gameObject.SetActive(true);
                        break;
                    case Vars.Player3:
                        Images[0].SetActive(true);
                        Images[1].SetActive(true);
                        Images[2].SetActive(true);
                        StatusText[0].gameObject.SetActive(true);
                        StatusText[1].gameObject.SetActive(true);
                        StatusText[2].gameObject.SetActive(true);
                        break;
                }
                break;
        }
    }
}
