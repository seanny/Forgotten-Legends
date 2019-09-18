using System;

namespace Core.Books
{
    [Serializable]
    public class Book
    {
        /// <summary>
        /// Book Name
        /// </summary>
        public string bookName;

        /// <summary>
        /// Book Type
        /// 1: Book
        /// 2: Scroll
        /// 3: Note
        /// </summary>
        public int bookType;
        
        /// <summary>
        /// Book Author
        /// </summary>
        public string bookAuthor;

        /// <summary>
        /// Book Publish Date
        /// </summary>
        public string bookDate;

        /// <summary>
        /// Book Text
        /// </summary>
        public string[] bookText;
    }
}
