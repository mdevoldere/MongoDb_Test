using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MongoDB_Test
{
    class Person
    {
        public ObjectId Id { get; set; }

        
        //public ObjectId myPrimaryKey;

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public List<string> Interests { get; set; }


        public Person()
        {

        }
    }
}
