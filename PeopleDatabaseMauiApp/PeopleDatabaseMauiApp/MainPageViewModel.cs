using PeopleDatabaseClassLibrary;
using PeopleDatabaseClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleDatabaseMauiApp
{
    internal class MainPageViewModel : BindableObject
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }

        private Command addPersonCommand;
        public Command AddPersonCommand
        {
            get
            {
                if (addPersonCommand == null)
                    addPersonCommand = new Command(() =>
                    {
                        peopleRepository.AddNewPerson(Name, Surname, Age);
                    });
                return addPersonCommand;
            }
        }

        public ObservableCollection<Person> People { get; set; }

        private Command readPeopleCommand;
        public Command ReadPeopleCommand
        {
            get
            {
                if (readPeopleCommand == null)
                    readPeopleCommand = new Command(() =>
                    {
                        List<Person> people = peopleRepository.GetPeople();
                        People.Clear();
                        foreach (Person person in people)
                            People.Add(person);
                    });
                return readPeopleCommand;
            }
        }

        private PeopleRepository peopleRepository;
        public MainPageViewModel()
        {
            peopleRepository = new PeopleRepository();
            People = new ObservableCollection<Person>();
        }
    }
}
