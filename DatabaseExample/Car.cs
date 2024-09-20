using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseExample
{
    public class Car
    {
        public int Id { get; set; } 
        public string Model { get; set; }
        public string Brand { get; set; }
        public double Price { get; set; }
        public DateTime YearOfManufacture { get; set; }
        public bool IsElectric { get; set; } // Indikerer om bilen er elektrisk

        public Car(int id, string model, string brand, double price, DateTime yearOfManufacture, bool isElectric)
        {
            Id = id;
            Model = model;
            Brand = brand;
            Price = price;
            YearOfManufacture = yearOfManufacture;
            IsElectric = isElectric;
        }


        public Car(string model, string brand, double price, DateTime yearOfManufacture, bool isElectric)
        {
          
            Model = model;
            Brand = brand;
            Price = price;
            YearOfManufacture = yearOfManufacture;
            IsElectric = isElectric;
        }


        public override string ToString()
        {
            return $"Id: {Id}, Brand: {Brand}, Model: {Model}, Price: {Price:C}, Year: {YearOfManufacture.Year}, Electric: {IsElectric}";
        }
    }
}
