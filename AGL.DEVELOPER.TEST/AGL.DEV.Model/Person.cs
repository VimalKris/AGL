using System.Collections.Generic;

namespace AGL.DEV.Model
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public List<Pet> Pets { get; set; }

        public Person()
        {
            Pets = new List<Pet>();
        }
    }

    public enum Gender { Male, Female }
}
