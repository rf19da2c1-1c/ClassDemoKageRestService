using System;
using System.Collections.Generic;
using System.Text;

namespace ClassDemoKageLib.model
{
    public class Kage
    {
        private string _name;
        private double _price;
        private int _noOfPieces;
        private int _id;

        // HUSK default kostruktør
        public Kage()
        {
            _id = -1;
        }

        public Kage(string name, double price, int noOfPieces, int id)
        {
            _name = name;
            _price = price;
            _noOfPieces = noOfPieces;
            _id = id;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public double Price
        {
            get => _price;
            set => _price = value;
        }

        public int NoOfPieces
        {
            get => _noOfPieces;
            set => _noOfPieces = value;
        }

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {_id}, nameof(Name): {_name}, {nameof(Price)}: {_price}, {nameof(NoOfPieces)}: {_noOfPieces}";
        }

    }
}
