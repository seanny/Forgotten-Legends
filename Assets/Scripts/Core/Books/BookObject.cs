using Core.Services;
using UnityEngine;

namespace Core.Books
{
    public class BookObject : Interactable.Interactable
    {
        public Book book;

        public override void Interact()
        {
            base.Interact();
            ServiceLocator.GetService<BookService>().ReadBook(book);
        }
    }
}