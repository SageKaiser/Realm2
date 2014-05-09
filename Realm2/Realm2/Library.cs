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
            name = new List<string>() { "Library of Congress", "King's Library", "local library", "National Library", "Elmwood Libaray", "Evergreen Library", "Silvercreek Library", "Library of Ramsey", "Libray of Magic" }[Program.main.rand.Next(0, 10)];
            hasVisited = false;
            books = new ObservableCollection<Book>();
            for (int i = 0; i < 3; i++)
                books.Add(Program.main.mainBookList[Program.main.rand.Next(0, Program.main.mainBookList.Count)]);
        }
        public void Interact(Book book)
        {
            if (!hasVisited)
            {
                book.Read();
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
        public void Read()
        {
            Program.main.player.LearnAbility(ability);
        }
    }
}
