using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class PagesOfMakersUI : MonoBehaviour
{
    /*
    --page
    **categoryname
    maker1
    maker2
    maker3
    maker4 


    goal format ->

    categoryname1       maker1
                        maker2
                        maker3
                        maker4
     
    categoryname2       maker1
                        maker2
                        maker3
                        maker4

     */




    [SerializeField]
    [TextArea(3,150)]
    public string MakersText;
    public int PageIndex = 0;

    public Text CategoriesTextUi;
    public Text MakersTextUi;

    private List<PageOfMakersData> pagesData;

    private float LastPagedTime;
    public float PageTimeRate = 2f;

    public KeyCode CloseKey = KeyCode.Escape;

    void Start()
    {
        LastPagedTime = Time.time;
        UpdateText();
    }

    
    void Update()
    {

        if (Input.GetKeyDown(CloseKey)) this.transform.gameObject.SetActive(false);
        if (pagesData!=null && pagesData.Count > 1)
        {

            if (Time.time > LastPagedTime + PageTimeRate)
            {
                LastPagedTime = Time.time;
                PageIndex++;

                Debug.Log($"tick {PageIndex}");
            }

            if (PageIndex < 0)
            {
                PageIndex = pagesData.Count-1;
            }

            if (PageIndex > pagesData.Count - 1)
            {
                PageIndex = 0;
            }
        }

        DisplayCurrentPage();
    }

    public void UpdateText()
    {
        string newPageSign = "--";
        string newCategorySign = "**";
        
        this.pagesData = new List<PageOfMakersData>();
        CategoryOfMakersData category = null;
        PageOfMakersData page = null;


        string[] lines = MakersText.Split('\n'); /// TODO: System.Environment.NewLine

        foreach (string line in lines)
        {
            if (line.Trim().StartsWith(newPageSign))
            {
                page = new PageOfMakersData();
                this.pagesData.Add(page);
                continue;
            }

            if (line.Trim().StartsWith(newCategorySign))
            {
                category = new CategoryOfMakersData();
                category.CategoryName = line.Remove(0,newCategorySign.Length);
                if (page == null)
                {
                    page = new PageOfMakersData();
                    this.pagesData.Add(page);
                }
                page.Add(category);
                continue;
            }

            category.MakersText+=line + '\n'.ToString();
        }
    }

    [ExecuteAlways]
    public void DisplayCurrentPage()
    {
        if (this.pagesData!=null && this.pagesData.Count > 0)
        {
            var pageData = this.pagesData[PageIndex];
            CategoriesTextUi.text = pageData.GetCategoryNamesFormatted();
            MakersTextUi.text = pageData.GetMakersNamesFormatted();
        }
        else
        {
            UpdateText();
        }

    }
}

public class PageOfMakersData:List<CategoryOfMakersData>
{

    public string PageTitle = ""; // TODO: shoulb used if filled, not used yet

    public string GetCategoryNamesFormatted()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in this)
        {
            sb.Append(item.CategoryName);
            string[] makers = item.MakersText.Split('\n');

            foreach (var maker in makers)
            {
                sb.AppendLine();
            }
        }
        return sb.ToString();
    }

    public string GetMakersNamesFormatted()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in this)
        {
            sb.AppendLine(item.MakersText);
        }
        return sb.ToString();
    }

}

public class CategoryOfMakersData 
{
    public string CategoryName = "";

    /// <summary>
    /// text block of lines, line of names
    /// </summary>
    public string MakersText = "";  
}

