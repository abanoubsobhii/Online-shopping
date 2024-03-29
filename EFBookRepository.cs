﻿using BookStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain.Entites;

namespace BookStore.Domain.Concrete
{
    public class EFBookRepository : IBookRepository
    {
        EFDbContext context = new EFDbContext();
        public IEnumerable<Book> Books
        {
            get
            {
                return context.Books;
            }
        }

        public Book DeleteBook(int isbn)
        {
            Book dbEntity = context.Books.Find(isbn);
            if (dbEntity != null)
            {
                context.Books.Remove(dbEntity);
                context.SaveChanges();
            }
            return dbEntity;
        }

        public void SaveBook(Book book)
        {
            Book dbEntity = context.Books.Find(book.ISBN);
            if (dbEntity == null)
            {
           
              context.Books.Add(book);
            }
            else
            {
                dbEntity.Title = book.Title;
                dbEntity.Specialization = book.Specialization;
                dbEntity.price = book.price;
                dbEntity.Description = book.Description;
            }
            context.SaveChanges();
        }
    }
}
