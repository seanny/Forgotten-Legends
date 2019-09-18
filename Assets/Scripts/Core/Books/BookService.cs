using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Core.Services;

namespace Core.Books
{
    public class BookService : MonoBehaviour, IService
    {
        private Image m_BookImage;
        private Texture2D m_BookSprite;
        
        private TextMeshProUGUI m_BookText;

        private string[] m_BookPageTexts;

        public bool isReading { get; private set; }
        private Book m_Book;
        private int m_BookPage;

        public void OnStart()
        {
            
        }

        private void Start()
        {
            ServiceLocator.AddService(this);
            
            m_BookImage = GameObject.FindWithTag("BookImage").GetComponent<Image>();
            m_BookText = m_BookImage.GetComponentInChildren<TextMeshProUGUI>();
            
            m_BookSprite = Utility.ImageUtils.LoadPNG(Path.Combine(Application.streamingAssetsPath, "Textures", "Misc", "Page.png"));
            
            m_BookImage.sprite = Sprite.Create(m_BookSprite, new Rect(0, 0, m_BookSprite.width, m_BookSprite.height), new Vector2(0.5f, 0.5f));
            m_BookImage.gameObject.SetActive(false);
            isReading = false;
        }

        public void OnEnd()
        {
            StopReading();
        }

        public void ReadBook(Book book)
        {
            m_Book = book;
            m_BookPageTexts = m_Book.bookText;
            m_BookPage = 0;
            isReading = true;
            m_BookImage.gameObject.SetActive(true);
            ReadPage(0);
        }

        private void StopReading()
        {
            m_Book = null;
            m_BookPageTexts = null;
            m_BookImage.gameObject.SetActive(false);
            isReading = false;
        }
        
        private void ReadPage(int page)
        {
            m_BookText.text = FormatText(m_BookPageTexts[page]);
            m_BookPage = page;
        }

        private void Update()
        {
            if (isReading)
            {
                if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.Mouse0))
                {
                    m_BookPage--;
                    if (m_BookPage < 0)
                    {
                        m_BookPage = 0;
                    }
                    ReadPage(m_BookPage);
                }
                else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.Mouse1))
                {
                    m_BookPage++;
                    if (m_BookPage > m_Book.bookText.Length - 1)
                    {
                        m_BookPage = m_Book.bookText.Length - 1;
                    }
                    ReadPage(m_BookPage);
                }
                else if (Input.GetKeyUp(KeyCode.Escape))
                {
                    StopReading();
                }
            }
        }

        /// <summary>
        /// Format Text into something human readable. 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string FormatText(string text)
        {
            string formattedText = text;
            formattedText = formattedText.Replace("~br~", Environment.NewLine);
            formattedText = formattedText.Replace("~bold~", "<b>");
            formattedText = formattedText.Replace("~endbold~", "</b>");
            formattedText = formattedText.Replace("~italic~", "<i>");
            formattedText = formattedText.Replace("~enditalic~", "</i>");
            formattedText = formattedText.Replace("~strike~", "<s>");
            formattedText = formattedText.Replace("~endstrike~", "</s>");
            formattedText = formattedText.Replace("~line~", "<u>");
            formattedText = formattedText.Replace("~endline~", "</u>");
            formattedText = formattedText.Replace("~alignleft~", "<align=\"left\">");
            formattedText = formattedText.Replace("~alignright~", "<align=\"right\">");
            formattedText = formattedText.Replace("~aligncentre~", "<align=\"center\">");
            formattedText = formattedText.Replace("~endalign~", "</align>");
            formattedText = formattedText.Replace("~enditalic~", "</i>");
            formattedText = formattedText.Replace("~hide~", "<mark=#000000FF>");
            formattedText = formattedText.Replace("~endhide~", "</mark>");
            return formattedText;
        }
    }
}