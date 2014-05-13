using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm2
{
    public class Library
    {
        public string name;
        public bool hasVisited;
        public ObservableCollection<Book> books;
        public Library()
        {
            //gets a random name
            name = new List<string>() { "Library of Congress", "King's Library", "local library", "National Library", "Elmwood Libaray", "Evergreen Library", "Silvercreek Library", "Library of Ramsey", "Libray of Magic" }[Program.random.Next(0, 10)];
            hasVisited = false;
            books = new ObservableCollection<Book>();
            //adds three random books to the Library
            for (int i = 0; i < 3; i++)
                books.Add(Program.main.mainBookList[Program.random.Next(0, Program.main.mainBookList.Count)]);
        }
        public void Interact(Book book)
        {
            //if the player hasn't been there
            if (!hasVisited)
            {
                //if the player hasn't read the book
                if (book.Read())
                    hasVisited = true;
            }
        }
    }
    public class Book
    {
        public string name, desc;
        public Ability ability;
        public Book(string Name, string Desc, Ability _ability)
        {
            name = Name;
            desc = Desc;
            ability = _ability;
        }
        public override string ToString()
        {
            return name;
        }
        public bool Read()
        {
            if (Program.main.player.LearnAbility(ability))
                return true;
            else
                return false;
        }
    }
}
