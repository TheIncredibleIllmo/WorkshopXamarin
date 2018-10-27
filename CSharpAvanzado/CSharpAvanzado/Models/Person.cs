﻿using System;
namespace CSharpAvanzado.Models
{
    internal class Person
    {
        private string _name;
        private int _height;
        private int _weight;

        private Gender _gender;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }

        public int Weight
        {
            get
            {
                return _weight;
            }
            set
            {
                _weight = value;
            }
        }

        public Gender Gender { get; set; }

        public Person(string name, int height, int weight, Gender gender)
        {
            _name = name;
            _height = height;
            _weight = weight;
            _gender = gender;
        }

        public override string ToString()
        {
            return $"{Name} {Height}";
        }
    }

    internal enum Gender
    {
        Male,
        Female
    }
}
