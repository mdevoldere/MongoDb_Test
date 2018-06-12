using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Pour utiliser MongoDB dans un projet VisualStudio 2017:
// Outils -> Gestionnaire de package NuGet -> Gérer les packages NuGet pour la solution...
// Dans la fenêtre, cliquez sur "Parcourir" puis installer les packages suivants:
// MongoDB.Driver
// MongoDB.Driver.Core (devrait être installé automatiquement avec MongoDB.Driver)
// MongoDB.Bson (devrait être installé automatiquement avec MongoDB.Driver)
//
// Une fois les packages installés, sélectionnez le package "MongoDB.Driver" puis cochez la case correspondant à votre projet pour effectuer la liaison.

// Import de l'espace de noms MongoDB
using MongoDB.Driver;
using MongoDB.Driver.Core;

namespace MongoDB_Test
{
    class Program
    {
        static Person staticPerson = new Person()
        {
            FirstName = "Mike",
            LastName = "Dev",
            Age = 37,
            Interests = new List<string>() { "AstroBiology", "Networks" }
        };

        static List<Person> staticPeople = new List<Person>()
        {
            new Person() { FirstName = "Anne", LastName = "Onyme", Age = 37, Interests = new List<string>() { "Nothing" } },
            new Person() { FirstName = "Handy", LastName = "Capet", Age = 18, Interests = new List<string>() { "Bolognese", "Game of Thrones" } },
            new Person() { FirstName = "Marc", LastName = "Cram", Age = 43, Interests = new List<string>() { "Money" } },
            new Person() { FirstName = "Henri", LastName = "Cram", Age = 21, Interests = new List<string>() { "Money" } }
        };

        // Création d'une instance du client MongoDB.
        static MongoClient client = new MongoClient("mongodb://localhost:27017");

        // Chargement de la base de données "people".
        // Si la base de données n'existe pas, elle est automatiquement créée.
        static IMongoDatabase db = client.GetDatabase("people");

        static void Main(string[] args)
        {
            db.DropCollection("persons"); // Vidage de la collection

            // Chargement de la collection "persons" depuis la base de données "people".
            // Si la collection n'xiste pas, elle est automatiquement créée.
            IMongoCollection<Person> collection = db.GetCollection<Person>("persons");

            // Insertion d'un élement
            collection.InsertOne(staticPerson);

            // Insertion de plusieurs élements
            collection.InsertMany(staticPeople);
            
            // Recherche d'éléments correspondant à un filtre (requête Linq)
            List<Person> result = collection.Find(x => x.FirstName == "Mike").ToList();

            // Parcours de la liste de résultat

            Console.WriteLine(result.Count().ToString() + " personnes trouvées pour le filtre \"x => x.FirstName == Mike\"");

            foreach (Person p in result)
            {
                Console.WriteLine(p.FirstName + " " + p.LastName + " (" + p.Age.ToString() + ")");
            }

            Console.ReadLine();

            // Récupération de tous les éléments
            List<Person> resultAll = collection.Find(x => true).ToList();

            Console.WriteLine(resultAll.Count().ToString() + " personnes trouvées pour le filtre \"x => true\"");


            foreach (Person p in resultAll)
            {
                Console.WriteLine(p.Id + ": " +  p.FirstName + " " + p.LastName + " (" + p.Age.ToString() + ")");
            }

            Console.ReadLine();

            // Récupération d'UN élément 
            Person person = collection.Find(x => x.LastName == "Cram").ToList().FirstOrDefault();

           
            // Définition d'un filtre de recherche
            var filter = Builders<Person>.Filter.Eq(x => x.LastName, "Cram");

            // Définition des attributs à mettre à jour
            var update = Builders<Person>.Update.Set(x => x.LastName, "Barbelivien");

            //Mise à jour de la collection (Uniquement le 1er élément correspondant au filtre)
            collection.UpdateOne(filter, update);

            // Mise à jour de la collection (Tous les éléments correspondant au filtre)
           // collection.UpdateMany(filter, update);

            // Récupération de tous les éléments
            resultAll = collection.Find(x => true).ToList();

            Console.WriteLine(resultAll.Count().ToString() + " personnes trouvées pour le filtre \"x => true\"");


            foreach (Person p in resultAll)
            {
                Console.WriteLine(p.Id + ": " + p.FirstName + " " + p.LastName + " (" + p.Age.ToString() + ")");
            }

            Console.ReadLine();

            // On vide la collection
            db.DropCollection("persons");

        }


        static void Test()
        {
            
        }
    }
}
