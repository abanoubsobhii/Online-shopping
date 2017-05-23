using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entites
{
   public class Cart
    {
        List<CartLine> LineCollection = new List<CartLine>();
        public void AddItem(Book book,int quantity=1)
        {
            CartLine line = LineCollection.Where(b => b.Book.ISBN == book.ISBN)
                            .FirstOrDefault();
            if (line == null)
            {

                LineCollection.Add(new CartLine { Book = book, Quantity = quantity });
            }
            else
                line.Quantity += quantity;

        }
        public decimal ComputeTotalValue()
        {
            return LineCollection.Sum(cl => cl.Book.price * cl.Quantity);            
        }
        public void Clear()
        {
            LineCollection.Clear();
        }
        public IEnumerable<CartLine> Lines
        {
            get { return LineCollection; }
        }

        public void RemoveLine(Book book)
        {
            LineCollection.RemoveAll(b=>b.Book.ISBN==book.ISBN);

        }
    }
    public class CartLine
    {
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}
